using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyChase2D : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;
    public float moveSpeed = 3f;

    [Header("Audio (opcional)")]
    public AudioClip chaseClip;
    public float volume = 1f;

    private Rigidbody2D rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Buscar jugador automáticamente si no está asignado
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
                Debug.Log("Jugador detectado automáticamente.");
            }
            else
            {
                Debug.LogWarning("No se encontró ningún objeto con la etiqueta 'Player'.");
            }
        }

        // Buscar AudioSource solo si existe, sin requerirlo
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && chaseClip != null)
        {
            audioSource.clip = chaseClip;
            audioSource.loop = true;
            audioSource.volume = volume;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            if (audioSource != null && !audioSource.isPlaying && chaseClip != null)
            {
                audioSource.Play();
            }

            Debug.Log("Persiguiendo al jugador...");
        }
        else
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Colisión con el jugador. Reiniciando escena...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
