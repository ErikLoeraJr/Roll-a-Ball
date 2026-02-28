using UnityEngine;

public class MushroomPowerUp : MonoBehaviour
{
    // Multiplicador de tamaño
    public float scaleMultiplier = 1.5f;

    // Multiplicador de salto
    public float jumpMultiplier = 1.5f;

    // Multiplicador de velocidad (opcional)
    public float speedMultiplier = 1.2f;

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
            // Aumentar tamaño del jugador
            other.transform.localScale *= scaleMultiplier;

            // Obtener el script PlayerController
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                // Aumentar salto
                player.jumpForce *= jumpMultiplier;

                // Aumentar velocidad
                player.speed *= speedMultiplier;
            }

            // Destruir el hongo
            Destroy(gameObject);
        }
    }
}