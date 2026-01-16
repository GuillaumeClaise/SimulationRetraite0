using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using SimulationRetraite0.Models;
using System.Reflection;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using A = DocumentFormat.OpenXml.Drawing;

namespace SimulationRetraite0.Services;

public class PowerPointExportService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PowerPointExportService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public byte[] ExportTableauToPowerPoint(Tableau tableau, List<LigneTableau> lignes)
    {
        // Chemin vers le template
        var templatePath = Path.Combine(_webHostEnvironment.WebRootPath, "templates", "template.pptx");

        if (!File.Exists(templatePath))
        {
            throw new FileNotFoundException($"Le fichier template n'existe pas : {templatePath}");
        }

        // Créer une copie du template en mémoire
        using var templateStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read);
        using var memoryStream = new MemoryStream();
        templateStream.CopyTo(memoryStream);
        memoryStream.Position = 0;

        // Modifier la copie
        using (var presentationDocument = PresentationDocument.Open(memoryStream, true))
        {
            var presentationPart = presentationDocument.PresentationPart;
            if (presentationPart == null)
                throw new InvalidOperationException("Le template PowerPoint ne contient pas de PresentationPart");

            // Récupérer le premier slide
            var slideIdList = presentationPart.Presentation.SlideIdList;
            if (slideIdList == null || !slideIdList.Elements<SlideId>().Any())
                throw new InvalidOperationException("Le template ne contient aucun slide");

            var slideId = slideIdList.Elements<SlideId>().First();
            var slidePart = (SlidePart)presentationPart.GetPartById(slideId.RelationshipId!);

            // Modifier le graphique du slide
            ModifyChartInSlide(slidePart, tableau, lignes);
        }

        return memoryStream.ToArray();
    }

    private void ModifyChartInSlide(SlidePart slidePart, Tableau tableau, List<LigneTableau> lignes)
    {
        // Chercher le premier graphique dans le slide
        var graphicFrame = slidePart.Slide.Descendants<GraphicFrame>().FirstOrDefault();
        if (graphicFrame == null)
            throw new InvalidOperationException("Aucun graphique trouvé dans le slide 1");

        // Récupérer le ChartPart
        var chartPart = slidePart.ChartParts.FirstOrDefault();
        if (chartPart == null)
            throw new InvalidOperationException("Aucun ChartPart trouvé dans le slide");

        // Récupérer l'entête (première ligne avec LtNumeroLigne = 0)
        var entete = lignes.FirstOrDefault(l => l.LtNumeroLigne == 0);
        var dataLines = lignes.Where(l => l.LtNumeroLigne > 0).OrderBy(l => l.LtNumeroLigne).ToList();

        // Modifier les données du graphique
        UpdateChartData(chartPart, dataLines, entete);
    }

    private void UpdateChartData(ChartPart chartPart, List<LigneTableau> lignes, LigneTableau? entete)
    {
        var chartSpace = chartPart.ChartSpace;
        var chart = chartSpace.Elements<C.Chart>().FirstOrDefault();
        if (chart == null)
            throw new InvalidOperationException("Aucun Chart trouvé dans le ChartPart");

        // Récupérer le BarChart
        var plotArea = chart.PlotArea;
        var barChart = plotArea?.Elements<C.BarChart>().FirstOrDefault();
        if (barChart == null)
            throw new InvalidOperationException("Aucun BarChart trouvé dans le graphique");

        // Récupérer les colonnes valides (exclure LtValeur1 qui est l'ID)
        var colonnesValides = GetColonnesValides(entete);
        var colonnesDataOnly = colonnesValides.Where(c => c != "LtValeur1").ToList();

        // Mettre à jour chaque série
        var seriesList = barChart.Elements<C.BarChartSeries>().ToList();

        for (int i = 0; i < Math.Min(seriesList.Count, colonnesDataOnly.Count); i++)
        {
            var series = seriesList[i];
            var colonne = colonnesDataOnly[i];

            UpdateBarChartSeries(series, colonne, lignes, entete);
        }
    }

    private void UpdateBarChartSeries(C.BarChartSeries series, string colonne, List<LigneTableau> lignes, LigneTableau? entete)
    {
        // Récupérer le nom de la série depuis l'entête
        var seriesName = GetLibelleForColonne(colonne, entete) ?? colonne;

        // Mettre à jour le nom de la série
        var seriesText = series.Elements<C.SeriesText>().FirstOrDefault();
        if (seriesText != null)
        {
            var stringCache = seriesText.Elements<C.StringReference>().FirstOrDefault()?.Elements<C.StringCache>().FirstOrDefault();
            if (stringCache != null)
            {
                var pointCount = stringCache.Elements<C.PointCount>().FirstOrDefault();
                if (pointCount != null)
                    pointCount.Val = 1;

                var point = stringCache.Elements<C.StringPoint>().FirstOrDefault();
                if (point != null)
                {
                    var numericValue = point.Elements<C.NumericValue>().FirstOrDefault();
                    if (numericValue != null)
                        numericValue.Text = seriesName;
                }
            }
        }

        // Mettre à jour les catégories (libellés des lignes)
        var categoryAxisData = series.Elements<C.CategoryAxisData>().FirstOrDefault();
        if (categoryAxisData != null)
        {
            var stringReference = categoryAxisData.Elements<C.StringReference>().FirstOrDefault();
            if (stringReference != null)
            {
                var stringCache = stringReference.Elements<C.StringCache>().FirstOrDefault();
                if (stringCache != null)
                {
                    // Mettre à jour le nombre de points
                    var pointCount = stringCache.Elements<C.PointCount>().FirstOrDefault();
                    if (pointCount != null)
                        pointCount.Val = (uint)lignes.Count;

                    // Supprimer les anciens points
                    stringCache.RemoveAllChildren<C.StringPoint>();

                    // Ajouter les nouveaux points (libellés)
                    for (uint idx = 0; idx < lignes.Count; idx++)
                    {
                        var ligne = lignes[(int)idx];
                        stringCache.Append(new C.StringPoint
                        {
                            Index = idx,
                            NumericValue = new C.NumericValue(ligne.LtLibelle ?? "")
                        });
                    }
                }
            }
        }

        // Mettre à jour les valeurs
        var values = series.Elements<C.Values>().FirstOrDefault();
        if (values != null)
        {
            var numberReference = values.Elements<C.NumberReference>().FirstOrDefault();
            if (numberReference != null)
            {
                var numberingCache = numberReference.Elements<C.NumberingCache>().FirstOrDefault();
                if (numberingCache != null)
                {
                    // Mettre à jour le nombre de points
                    var pointCount = numberingCache.Elements<C.PointCount>().FirstOrDefault();
                    if (pointCount != null)
                        pointCount.Val = (uint)lignes.Count;

                    // Supprimer les anciens points
                    numberingCache.RemoveAllChildren<C.NumericPoint>();

                    // Ajouter les nouveaux points (valeurs)
                    for (uint idx = 0; idx < lignes.Count; idx++)
                    {
                        var ligne = lignes[(int)idx];
                        var valueStr = GetValueForColonne(colonne, ligne);
                        var value = CleanAndParseNumericValue(valueStr);

                        numberingCache.Append(new C.NumericPoint
                        {
                            Index = idx,
                            NumericValue = new C.NumericValue(value.ToString("F2", System.Globalization.CultureInfo.InvariantCulture))
                        });
                    }
                }
            }
        }
    }

    private List<string> GetColonnesValides(LigneTableau? entete)
    {
        if (entete == null)
            return new List<string>();

        var colonnes = new List<string>();
        var properties = typeof(LigneTableau).GetProperties()
            .Where(p => p.Name.StartsWith("LtValeur") && p.PropertyType == typeof(string))
            .OrderBy(p => p.Name);

        foreach (var prop in properties)
        {
            var libelle = prop.GetValue(entete) as string;
            if (!string.IsNullOrWhiteSpace(libelle))
            {
                colonnes.Add(prop.Name);
            }
        }

        return colonnes;
    }

    private string? GetLibelleForColonne(string nomColonne, LigneTableau? entete)
    {
        if (entete == null)
            return null;

        var property = typeof(LigneTableau).GetProperty(nomColonne);
        return property?.GetValue(entete) as string;
    }

    private string? GetValueForColonne(string nomColonne, LigneTableau ligne)
    {
        var property = typeof(LigneTableau).GetProperty(nomColonne);
        return property?.GetValue(ligne) as string;
    }

    private double CleanAndParseNumericValue(string? valueStr)
    {
        if (string.IsNullOrWhiteSpace(valueStr))
            return 0;

        var cleaned = new System.Text.StringBuilder();
        bool hasDecimalSeparator = false;

        foreach (char c in valueStr)
        {
            if (char.IsDigit(c))
            {
                cleaned.Append(c);
            }
            else if ((c == ',' || c == '.') && !hasDecimalSeparator)
            {
                cleaned.Append('.');
                hasDecimalSeparator = true;
            }
            else if (c == '-' && cleaned.Length == 0)
            {
                cleaned.Append(c);
            }
        }

        if (cleaned.Length == 0)
            return 0;

        if (double.TryParse(cleaned.ToString(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double result))
            return result;

        return 0;
    }
}
