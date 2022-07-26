using UnityEngine;

/// <summary>
/// Clase que controla el movimiento de las palas.
/// </summary>
public class Paddle : MonoBehaviour
{
    /// <summary>
    /// Velocidad a la que se mueve la pala.
    /// </summary>
    [Header("Movement")]
    readonly float speed = 3.5f;
    
    /// <summary>
    /// Componente Rigidbody2D de la pala.
    /// </summary>
    [Header("Components")]
    [SerializeField] Rigidbody2D rb = null;
    /// <summary>
    /// Componente AudioSource de la pala.
    /// </summary>
    [SerializeField] AudioSource audioSource = null;

    void Update()
    {
        float v = Input.GetAxisRaw("Player1Vertical");

        if (Input.GetButtonDown("Cancel"))
        {
            GameManager2.manager.PauseGame();
        }

        rb.velocity = new Vector2(rb.velocity.x, v * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si la pala golpea la pelota, se reproducirá un sonido.

        if (collision.gameObject.CompareTag("Game2/Ball"))
        {
            audioSource.Play();
        }
    }

    /// <summary>
    /// Función que resetea la posición de la pala y la devuelve al centro de su campo.
    /// </summary>
    public void ResetPosition()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(-5.75f, 0);
    }
}