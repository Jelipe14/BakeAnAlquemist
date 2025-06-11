using UnityEngine;

/// <summary>
/// Representa un tipo de ítem que puede ir al inventario.
/// Se crea como un ScriptableObject para poder asignarse en el inspector.
/// </summary>
[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    [Tooltip("Identificador único. Ej.: 'key', 'golden_egg'")]
    public string itemID;

    [Tooltip("Icono que aparecerá en la interfaz")]
    public Sprite icon;
}
