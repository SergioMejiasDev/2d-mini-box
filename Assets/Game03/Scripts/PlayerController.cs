using UnityEngine;

/// <summary>
/// Clase con las funciones principales del jugador.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Velocidad de movimiento de la nave del jugador.
    /// </summary>
    readonly float speed = 5;
    /// <summary>
    /// Límite de la pantalla por la derecha en el eje X.
    /// </summary>
    readonly float maxBound = 7;
    /// <summary>
    /// Límite de la pantalla por la izquierda en el eje X.
    /// </summary>
    readonly float minBound = -7;
    /// <summary>
    /// Componente AudioSource de la nave.
    /// </summary>
    [SerializeField] AudioSource shootAudio = null;
    /// <summary>
    /// Posición desde la que se generarán las balas.
    /// </summary>
    [SerializeField] Transform shootPoint = null;
    /// <summary>
    /// Cadencia de disparo de la nave.
    /// </summary>
    readonly float cadency = 1;
    /// <summary>
    /// Momento en el que podremos realizar el siguiente disparo.
    /// Su valor se actualiza cada vez que se realiza un disparo.
    /// </summary>
    float nextFire;

    void Update()
    {
        float h = Input.GetAxisRaw("Player1Horizontal");
        
        // Si el jugador alcanza alguno de los bordes de la pantalla, no podrá seguir moviéndose en esa dirección.

        if (transform.position.x < minBound && h < 0)
        {
            h = 0;
        }

        else if (transform.position.x > maxBound && h > 0)
        {
            h = 0;
        }
        
        transform.Translate(Vector2.right * h * speed * Time.deltaTime);

        // No se podrá realizar un nuevo disparo hasta que no pase un tiempo determinado (cadencia).

        if (Input.GetKey(KeyCode.W) && (Time.time > nextFire))
        {
            nextFire = Time.time + cadency;
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Game3/BulletPlayer");
            if (bullet != null)
            {
                bullet.SetActive(true);
                bullet.transform.position = shootPoint.position;
                bullet.transform.rotation = Quaternion.identity;
            }
            shootAudio.Play();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            GameManager3.manager3.PauseGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si somos alcanzados por una bala enemiga, la nave explotará y perderemos una vida.

        if (other.gameObject.CompareTag("Game3/BulletEnemy"))
        {
            other.gameObject.SetActive(false);
            GameManager3.manager3.LoseHealth(1);
        }
    }
}