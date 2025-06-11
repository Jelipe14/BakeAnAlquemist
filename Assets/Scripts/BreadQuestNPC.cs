using UnityEngine;
using DialogueEditor;

/// <summary>
/// NPC que pide panes al jugador (aunque el contador esté en una panadería)
/// y añade una recompensa a la UI del inventario.
/// </summary>
public class BreadQuestNPC : MonoBehaviour
{
    [Header("Misión")]
    [SerializeField] private int breadRequired = 6;

    [Header("Referencias")]
    [SerializeField] private BreadGenerator breadSource;   // Objeto con BreadGenerator (la panadería)
    [SerializeField] private InventoryUI inventoryUI;      // Script del HUD

    [Header("Recompensa")]
    [SerializeField] private InventoryItem rewardItem;     // Ítem que aparecerá en la UI

    [Header("Diálogos")]
    public NPCConversation needBreadConversation;
    public NPCConversation breadCollectedConversation;

    [Header("Audio (opcional)")]
    [SerializeField] private AudioClip questFailClip;      // Sonido si falta pan
    [SerializeField] private AudioClip questSuccessClip;   // Sonido al completar la misión
    [SerializeField] private float volume = 1f;

    private AudioSource audioSource;
    private bool questCompleted;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            TryCompleteQuest();
    }

    private void TryCompleteQuest()
    {
        if (questCompleted) return;

        if (breadSource.GetBreadCount() < breadRequired)
        {
            // Diálogo de necesidad
            if (needBreadConversation != null)
                ConversationManager.Instance.StartConversation(needBreadConversation);

            // Audio de fallo
            PlaySound(questFailClip);
            return;
        }

        // Resta el pan
        breadSource.SpendBread(breadRequired);

        // Añade la recompensa
        if (rewardItem != null && inventoryUI != null)
            inventoryUI.AddItem(rewardItem.itemID, rewardItem.icon);
        else
            Debug.LogWarning($"{name}: falta asignar rewardItem o inventoryUI");

        // Marca como completada
        questCompleted = true;

        // Diálogo de éxito
        if (breadCollectedConversation != null)
            ConversationManager.Instance.StartConversation(breadCollectedConversation);

        // Audio de éxito
        PlaySound(questSuccessClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        }
    }
}
