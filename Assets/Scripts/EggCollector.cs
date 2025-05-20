using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EggCollector : MonoBehaviour
{
    private int eggCount = 0;

    [SerializeField] private TextMeshProUGUI eggCounterText; // TextMeshProUGUI en lugar de Text

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Egg"))
        {
            eggCount++;
            UpdateUI();
            Destroy(other.gameObject); // Elimina el huevo
        }
    }

    private void UpdateUI()
    {
        eggCounterText.text = "Huevos: " + eggCount;
    }

    // 👇 Agrega esto:
    public int GetEggCount()
    {
        return eggCount;
    }
}