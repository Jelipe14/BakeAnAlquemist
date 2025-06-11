using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryChecker : MonoBehaviour
{
    [Header("Inventario")]
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private string[] requiredItemIDs;

    [Header("Opciones de Victoria")]
    [SerializeField] private bool useCanvas = true;
    [SerializeField] private GameObject victoryCanvas;
    [SerializeField] private string finalSceneName = "Final";
    [SerializeField] private string mainMenuSceneName = "MainMenu"; // <- Nueva opci�n

    private bool victoryAchieved = false;

    public void CheckVictory()
    {
        if (victoryAchieved) return;

        foreach (string itemID in requiredItemIDs)
        {
            if (inventoryUI.GetItemCount(itemID) == 0)
                return; // Falta al menos un �tem
        }

        victoryAchieved = true;

        if (useCanvas)
        {
            if (victoryCanvas != null)
                victoryCanvas.SetActive(true);
            else
                Debug.LogWarning("VictoryCanvas no asignado.");
        }
        else
        {
            SceneManager.LoadScene(finalSceneName);
        }
    }

    // Llamado por el bot�n en el Canvas
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
