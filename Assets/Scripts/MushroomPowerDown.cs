using UnityEngine;

public class MushroomPowerDown : MonoBehaviour
{
    // Multiplicador de tamaño (menor que 1 reduce el tamaño)
    public float scaleMultiplier = 0.7f;

    // Multiplicador de salto (menor que 1 reduce el salto)
    public float jumpMultiplier = 0.7f;

    // Multiplicador de velocidad (menor que 1 reduce la velocidad)
    public float speedMultiplier = 0.8f;

    // Velocidad de rotación del hongo
    public float rotationSpeed = 90f;

    void Update()
    {
        // Hace que el hongo gire constantemente
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica que sea el jugador
        if (other.CompareTag("Player"))
        {
            // Reducir tamaño del jugador
            other.transform.localScale *= scaleMultiplier;

            // Obtener el script PlayerController
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                // Reducir salto
                player.jumpForce *= jumpMultiplier;

                // Reducir velocidad
                player.speed *= speedMultiplier;
            }

            // Destruir el hongo
            Destroy(gameObject);
        }
    }
}