using System.Text.Json.Serialization;

namespace SimulationRetraite0.Models;

/// <summary>
/// Représente un tableau Mako avec ses en-têtes et ses lignes
/// </summary>
public class TableauMako
{
    [JsonPropertyName("headers")]
    public List<string> Headers { get; set; } = new();

    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("rows")]
    public List<List<string>> Rows { get; set; } = new();
}
