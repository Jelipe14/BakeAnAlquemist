using UnityEngine;
using DialogueEditor;

/// <summary>
/// NPC que intercambia pan por un objeto de inventario genérico.
/// </summary>
public class BreadTradeNPC : MonoBehaviour
{
    [Header("Trueque")]
    [SerializeField] private int breadRequired = 3;          // Pan necesario
    [SerializeField] private InventoryItem rewardItem;       // Ítem que se otorga

    [Header("Referencias jugador")]
    [SerializeField] private BreadGenerator playerBread;
    [SerializeField] private InventoryUI inventoryUI;

    [Header("Diálogos (Dialogue Editor)")]
    public NPCConversation notEnoughBreadConversation;
    public NPCConversation tradeSuccessConversation;

    [Header("Audio (opcional)")]
    [SerializeField] private AudioClip tradeFailClip;    // Sonido si no hay pan suficiente
    [SerializeField] private AudioClip tradeSuccessClip; // Sonido si el trueque es exitoso
    [SerializeField] private float volume = 1f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // No es obligatorio, pero se usa si existe
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            TryTrade();
    }

    private void TryTrade()
    {
        if (!playerBread.SpendBread(breadRequired))
        {
            // Falta pan
            if (notEnoughBreadConversation != null)
                ConversationManager.Instance.StartConversation(notEnoughBreadConversation);

            if (audioSource != null && tradeFailClip != null)
                audioSource.PlayOneShot(tradeFailClip, volume);

            return;
        }

        // Éxito: añade al inventario y diálogo
        inventoryUI.AddItem(rewardItem.itemID, rewardItem.icon);

        if (tradeSuccessConversation != null)
            ConversationManager.Instance.StartConversation(tradeSuccessConversation);

        if (audioSource != null && tradeSuccessClip != null)
            audioSource.PlayOneShot(tradeSuccessClip, volume);
    }
}
