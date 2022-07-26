using UnityEngine;

/// <summary>
/// Clase encargada del movimiento del coche.
/// </summary>
public class CarMovement : MonoBehaviour
{
    /// <summary>
    /// La fuerza con la que acelera el coche.
    /// </summary>
    [Header("Movement")]
    readonly float force = 5.0f;
    /// <summary>
    /// La velocidad de rotación del coche.
    /// </summary>
    readonly float rotationSpeed = 100.0f;
    /// <summary>
    /// El siguiente punto de control por el que debe pasar el coche (del 1 al 6).
    /// </summary>
    int nextPoint = 1;

    /// <summary>
    /// Componente Rigidbody2D del coche.
    /// </summary>
    [Header("Components")]
    [SerializeField] Rigidbody2D rb = null;
    /// <summary>
    /// Componente AudioSource del coche (sonido de choque).
    /// </summary>
    [SerializeField] AudioSource hitAudio = null;
    /// <summary>
    /// El componente con las funciones del cronómetro.
    /// </summary>
    [SerializeField] Timer11 timer = null;

    void OnEnable()
    {
        nextPoint = 1;
    }

    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");

        rb.AddForce(transform.up * force * v, ForceMode2D.Force);
    }

    private void Update()
    {
        float h = -Input.GetAxis("Horizontal");

        if (rb.velocity.magnitude > 0.1f)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime * h);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            GameManager11.manager.PauseGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el coche choca con algo, se reproduce el sonido de choque.

        hitAudio.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Point1" && nextPoint == 1)
        {
            nextPoint = 2;
        }

        else if (collision.gameObject.name == "Point2" && nextPoint == 2)
        {
            nextPoint = 3;
        }

        else if (collision.gameObject.name == "Point3" && nextPoint == 3)
        {
            nextPoint = 4;
        }

        else if (collision.gameObject.name == "Point4" && nextPoint == 4)
        {
            nextPoint = 5;
        }

        else if (collision.gameObject.name == "Point5" && nextPoint == 5)
        {
            nextPoint = 6;
        }

        else if (collision.gameObject.name == "Point6" && nextPoint == 6)
        {
            nextPoint = 1;

            timer.ResetTimer();
        }
    }
}