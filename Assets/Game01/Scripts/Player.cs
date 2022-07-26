using UnityEngine;

/// <summary>
/// Clase que se encarga del movimiento del jugador.
/// </summary>
public class Player : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Velocidad del jugador.
    /// </summary>
    [Header("Movement")]
    readonly float speed = 4;
    /// <summary>
    /// Fuerza del salto del jugador.
    /// </summary>
    readonly float jump = 9.5f;
    /// <summary>
    /// Capa asignada al suelo donde se puede saltar.
    /// </summary>
    [SerializeField] LayerMask groundMask = 0;
    
    /// <summary>
    /// Componente Rigidbody2D del jugador.
    /// </summary>
    [Header("Components")]
    [SerializeField] Rigidbody2D rb = null;
    /// <summary>
    /// Componente Animator del jugador.
    /// </summary>
    [SerializeField] Animator anim = null;
    /// <summary>
    /// Componente SpriteRenderer del jugador.
    /// </summary>
    [SerializeField] SpriteRenderer sr = null;
    /// <summary>
    /// Componente AudioSource del jugador (sonido de salto).
    /// </summary>
    [SerializeField] AudioSource audioSource = null;

    /// <summary>
    /// AudioSource cuyo sonido indicará que nos ha alcanzado un enemigo.
    /// </summary>
    [Header("Sounds")]
    [SerializeField] AudioSource hurtSound = null;
    #endregion

    private void OnEnable()
    {
        transform.position = new Vector2(-6.3f, -5.4f); // Posición donde se genera el jugador al iniciar la partida.
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Player1Horizontal"); // Usamos "GetAxisRaw" para que los valores solo puedan ser -1, 0 y 1.

        Movement(h);

        Animation(h);

        Jump();

        if (Input.GetButtonDown("Cancel"))
        {
            GameManager1.manager.PauseGame();
        }
    }

    /// <summary>
    /// Función que mueve al jugador en la dirección deseada.
    /// </summary>
    /// <param name="h">Dirección de movimiento del jugador. Positivo si se mueve a la derecha, negativo si se mueve a la izquierda.</param>
    public void Movement(float h)
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime * h);

        if (h > 0) // Se mueve a la derecha.
        {
            transform.localScale = new Vector2(0.85f, 0.85f);
        }
        else if (h < 0) // Se mueve a la izquierda.
        {
            transform.localScale = new Vector2(-0.85f, 0.85f);
        }
    }

    /// <summary>
    /// Función que activa las animaciones del jugador.
    /// </summary>
    /// <param name="h">Dirección de movimiento del jugador. Positivo si se mueve a la derecha, negativo si se mueve a la izquierda.</param>
    public void Animation(float h)
    {
        anim.SetBool("IsWalking", h != 0 && IsGrounded()); // Si se está moviendo mientras toca el suelo, está caminando.
        anim.SetBool("IsJumping", !IsGrounded()); // Si no está tocando el suelo, se mueva o no, está saltando.
    }

    /// <summary>
    /// Función que hace saltar al jugador.
    /// </summary>
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded()) // Es necesario estar tocando el suelo para poder saltar.
        {
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            audioSource.Play();
        }
    }

    /// <summary>
    /// Función que indica si el jugador está tocando el suelo o no.
    /// </summary>
    /// <returns>Verdadero si el jugador está en el suelo, falso si no lo está.</returns>
    bool IsGrounded()
    {
        // Mediante un Raycast vertical, miramos si justo debajo del jugador está el suelo.

        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, sr.bounds.extents.y + 0.01f, 0), Vector2.down, 0.1f, groundMask);

        return hit;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Si tocamos a un enemigo, perdemos la partida.

        if ((other.gameObject.CompareTag("Game1/Enemy")) || (other.gameObject.CompareTag("Game1/Missile")))
        {
            gameObject.SetActive(false);
            GameManager1.manager.GameOver();

            hurtSound.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si tocamos una moneda, esta desaparece y aumenta nuestra puntuación.

        if (other.gameObject.CompareTag("Game1/Coin"))
        {
            other.gameObject.SetActive(false);
            GameManager1.manager.UpdateScore();
        }
    }
}