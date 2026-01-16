# Table Extractor - Extension Chrome

Extension Chrome pour extraire les tableaux HTML des pages web.

## Fonctionnalités

- Extraction automatique de tous les tableaux d'une page web
- Prévisualisation des tableaux extraits
- Export en CSV
- Export en JSON
- Copie dans le presse-papiers

## Installation

1. Ouvrez Chrome et allez dans `chrome://extensions/`
2. Activez le "Mode développeur" en haut à droite
3. Cliquez sur "Charger l'extension non empaquetée"
4. Sélectionnez le dossier `table-extractor-extension`

## Note sur les icônes

Les fichiers d'icônes (icon16.png, icon48.png, icon128.png) doivent être créés. Vous pouvez:
- Créer vos propres icônes aux dimensions spécifiées
- Utiliser un générateur d'icônes en ligne
- Temporairement commenter les lignes d'icônes dans manifest.json si vous voulez tester sans icônes

## Utilisation

1. Naviguez vers une page web contenant des tableaux
2. Cliquez sur l'icône de l'extension dans la barre d'outils
3. Cliquez sur "Extraire les tableaux"
4. Visualisez les tableaux extraits
5. Utilisez les boutons d'export pour sauvegarder les données

## Structure des fichiers

- `manifest.json` - Configuration de l'extension
- `popup.html` - Interface utilisateur
- `popup.css` - Styles de l'interface
- `popup.js` - Logique de l'interface
- `content.js` - Script d'extraction des tableaux
- `icon16.png`, `icon48.png`, `icon128.png` - Icônes (à créer)

## Technologies utilisées

- Chrome Extension Manifest V3
- JavaScript vanilla
- HTML5 / CSS3
