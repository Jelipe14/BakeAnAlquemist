using UnityEngine;
using DialogueEditor;

/// <summary>
/// NPC que pide panes al jugador (aunque el contador est� en una panader�a)
/// y a�ade una recompensa a la UI del inventario.
/// </summary>
public class BreadQuestNPC : MonoBehaviour
{
    [Header("Misi�n")]
    [SerializeField] private int breadRequired = 6;

    [Header("Referencias")]
    [SerializeField] private BreadGenerator breadSource;   // Objeto con BreadGenerator (la panader�a)
    [SerializeField] private InventoryUI inventoryUI;      // Script del HUD

    [Header("Recompensa")]
    [SerializeField] private InventoryItem rewardItem;     // �tem que aparecer� en la UI

    [Header("Di�logos")]
    public NPCConversation needBreadConversation;
    public NPCConversation breadCollectedConversation;

    [Header("Audio (opcional)")]
    [SerializeField] private AudioClip questFailClip;      // Sonido si falta pan
    [SerializeField] private AudioClip questSuccessClip;   // Sonido al completar la misi�n
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
            // Di�logo de necesidad
            if (needBreadConversation != null)
                ConversationManager.Instance.StartConversation(needBreadConversation);

            // Audio de fallo
            PlaySound(questFailClip);
            return;
        }

        // Resta el pan
        breadSource.SpendBread(breadRequired);

        // A�ade la recompensa
        if (rewardItem != null && inventoryUI != null)
            inventoryUI.AddItem(rewardItem.itemID, rewardItem.icon);
        else
            Debug.LogWarning($"{name}: falta asignar rewardItem o inventoryUI");

        // Marca como completada
        questCompleted = true;

        // Di�logo de �xito
        if (breadCollectedConversation != null)
            ConversationManager.Instance.StartConversation(breadCollectedConversation);

        // Audio de �xito
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
