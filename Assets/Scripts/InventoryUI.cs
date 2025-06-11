using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Inventario genérico con interfaz dinámica.
/// Instancia un prefab para cada nuevo objeto y actualiza su contador.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    [Header("Prefabs & Layout")]
    [SerializeField] private GameObject itemUIPrefab; // Prefab con Image("Icon") + TextMeshProUGUI("Count")
    [SerializeField] private Transform itemsParent;   // Contenedor horizontal/vertical en el HUD

    // Diccionario interno: itemID -> ranura en la UI
    private readonly Dictionary<string, InventorySlot> inventory = new Dictionary<string, InventorySlot>();

    /// <summary>
    /// Añade una unidad del objeto indicado. Crea la ranura si aún no existe.
    /// </summary>
    public void AddItem(string itemID, Sprite icon)
    {
        if (inventory.TryGetValue(itemID, out InventorySlot slot))
        {
            slot.count++;
            slot.text.text = "x" + slot.count;
        }
        else
        {
            GameObject slotGO = Instantiate(itemUIPrefab, itemsParent);

            Image image = slotGO.transform.Find("Icon").GetComponent<Image>();
            TextMeshProUGUI text = slotGO.transform.Find("Count").GetComponent<TextMeshProUGUI>();

            image.sprite = icon;
            text.text = "x1";

            inventory[itemID] = new InventorySlot
            {
                icon = image,
                text = text,
                count = 1
            };
        }
    }

    /// <summary>
    /// Devuelve la cantidad que el jugador posee de un item.
    /// </summary>
    public int GetItemCount(string itemID)
    {
        return inventory.TryGetValue(itemID, out InventorySlot slot) ? slot.count : 0;
    }

    /// <summary>
    /// Estructura privada que representa cada slot visual.
    /// </summary>
    private class InventorySlot
    {
        public Image icon;
        public TextMeshProUGUI text;
        public int count;
    }
}