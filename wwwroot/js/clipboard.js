// Fonction pour lire le contenu du clipboard
window.clipboardInterop = {
    readText: async function () {
        try {
            const text = await navigator.clipboard.readText();
            return text;
        } catch (err) {
            console.error('Erreur lors de la lecture du clipboard:', err);
            return null;
        }
    }
};
