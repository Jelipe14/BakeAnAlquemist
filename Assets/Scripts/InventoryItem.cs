using UnityEngine;

/// <summary>
/// Representa un tipo de �tem que puede ir al inventario.
/// Se crea como un ScriptableObject para poder asignarse en el inspector.
/// </summary>
[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    [Tooltip("Identificador �nico. Ej.: 'key', 'golden_egg'")]
    public string itemID;

    [Tooltip("Icono que aparecer� en la interfaz")]
    public Sprite icon;
}
