using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EggCollector : MonoBehaviour
{
    private int eggCount = 0;

    [Header("UI")]
    [SerializeField] private GameObject eggCounterGroup;       // Contador visual (ícono + texto)
    [SerializeField] private TextMeshProUGUI eggCountText;     // Solo el número (ej: x3)
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Meta")]
    [SerializeField] private int eggGoal = 6;

    [Header("Audio")]
    [SerializeField] private AudioClip collectSound;           // Sonido al recoger un huevo
    [SerializeField] private float volume = 1f;

    private AudioSource audioSource;

    private void Start()
    {
        eggCounterGroup.SetActive(false);  // Oculta el contador al inicio
        messagePanel.SetActive(false);     // Oculta el mensaje al inicio

        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Egg"))
        {
            eggCount++;

            // Activar contador visual al recoger el primer huevo
            if (eggCount == 1)
            {
                Debug.Log("Primer huevo recogido. Activando contador.");
                eggCounterGroup.SetActive(true);
            }

            UpdateUI();
            PlaySound();
            Destroy(other.gameObject);

            if (eggCount >= eggGoal)
            {
                Debug.Log("Todos los huevos recogidos. Iniciando mensaje final.");
                StartCoroutine(ShowCompletionMessage());
            }
        }
    }

    private void UpdateUI()
    {
        eggCountText.text = "x" + eggCount;
    }

    private IEnumerator ShowCompletionMessage()
    {
        Debug.Log("Ocultando contador...");
        eggCounterGroup.SetActive(false);

        messageText.text = "¡Has recolectado todos los huevos!";
        messagePanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        messagePanel.SetActive(false);
    }

    private void PlaySound()
    {
        if (collectSound == null) return;

        if (audioSource != null)
        {
            audioSource.PlayOneShot(collectSound, volume);
        }
        else
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
        }
    }

    public int GetEggCount()
    {
        return eggCount;
    }
}
