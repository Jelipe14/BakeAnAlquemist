using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Genera pan cada X segundos. El jugador lo puede recoger pulsando el botón sobre la casa.
/// El contador de pan se muestra en el HUD y puede gastarse mediante SpendBread().
/// </summary>
public class BreadGenerator : MonoBehaviour
{
    [Header("World UI")]
    [SerializeField] private Canvas breadCanvas;     // Canvas en World Space encima de la panadería
    [SerializeField] private Button breadButton;     // Botón para recoger el pan

    [Header("HUD UI")]
    [SerializeField] private GameObject breadCounterGroup;   // Panel con icono + texto
    [SerializeField] private TextMeshProUGUI breadCountText; // Texto estilo "x0"

    [Header("Config")]
    [SerializeField] private float generationInterval = 10f; // Tiempo entre panes

    [Header("Audio (opcional)")]
    [SerializeField] private AudioClip breadReadyClip;   // Sonido al aparecer pan
    [SerializeField] private AudioClip breadCollectClip; // Sonido al recoger pan
    [SerializeField] private float volume = 1f;

    private int breadCount;
    private AudioSource audioSource;

    private void Start()
    {
        breadCanvas.gameObject.SetActive(false);
        breadCounterGroup.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(GenerateBread());
    }

    private IEnumerator GenerateBread()
    {
        while (true)
        {
            yield return new WaitForSeconds(generationInterval);
            ShowBreadPanel();

            if (audioSource != null && breadReadyClip != null)
                audioSource.PlayOneShot(breadReadyClip, volume);
        }
    }

    private void ShowBreadPanel()
    {
        breadCanvas.gameObject.SetActive(true);
        breadButton.onClick.RemoveAllListeners();
        breadButton.onClick.AddListener(CollectBread);
    }

    private void CollectBread()
    {
        breadCount++;

        if (breadCount == 1)
            breadCounterGroup.SetActive(true);

        UpdateBreadUI();
        breadCanvas.gameObject.SetActive(false);

        if (audioSource != null && breadCollectClip != null)
            audioSource.PlayOneShot(breadCollectClip, volume);
    }

    private void UpdateBreadUI()
    {
        breadCountText.text = "x" + breadCount;
    }

    public int GetBreadCount() => breadCount;

    /// <summary>
    /// Intenta gastar la cantidad indicada de pan.
    /// Devuelve true si hay suficiente y se descuenta; false en caso contrario.
    /// </summary>
    public bool SpendBread(int amount)
    {
        if (breadCount < amount) return false;

        breadCount -= amount;
        UpdateBreadUI();

        if (breadCount == 0)
            breadCounterGroup.SetActive(false);

        return true;
    }
}
