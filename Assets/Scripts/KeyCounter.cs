using UnityEngine;
using TMPro;

public class KeyCounter : MonoBehaviour
{
    [SerializeField] private GameObject keyGroup;              // Panel que contiene icono + texto
    [SerializeField] private TextMeshProUGUI keyCountText;     // Texto tipo "x0"

    private int keyCount = 0;

    private void Start()
    {
        keyGroup.SetActive(false); // Oculta el grupo al inicio
    }

    public void AddKey()
    {
        keyCount++;

        // Mostrar en pantalla al recoger la primera
        if (keyCount == 1)
            keyGroup.SetActive(true);

        UpdateUI();
    }

    private void UpdateUI()
    {
        keyCountText.text = "x" + keyCount;
    }

    public int GetKeyCount()
    {
        return keyCount;
    }
}

