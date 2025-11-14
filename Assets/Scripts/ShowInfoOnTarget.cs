using UnityEngine;
using Vuforia;

public class ShowInfoOnTarget : MonoBehaviour
{
    public GameObject infoPanel;   // Panel contenant l'image + texte qui doit s'afficher

    private ObserverBehaviour observer; // Référence vers le composant Vuforia qui gère le tracking de l'image cible

    void Start()
    {
        // Récupère le composant ObserverBehaviour attaché à ce GameObject l'image target 
        observer = GetComponent<ObserverBehaviour>();
        // Vérifie qu'il existe bien
        if (observer != null)
        {
            observer.OnTargetStatusChanged += OnStatusChanged;
        }

        // Panel caché au début
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    // Méthode appelée automatiquement par Vuforia
    // à chaque changement de statut du target (détecté / perdu)
    private void OnStatusChanged(ObserverBehaviour behaviour,
                                 TargetStatus status)
    {
        // Le target est considéré comme visible si son état est :
        // - TRACKED : visible et correctement détecté
        // - EXTENDED_TRACKED : suivi même si pas toujours dans la caméra
        bool isVisible =
            status.Status == Status.TRACKED ||
            status.Status == Status.EXTENDED_TRACKED;
        
        // Affiche ou masque le panel selon la visibilité du target
        if (infoPanel != null)
            infoPanel.SetActive(isVisible);
    }
}
