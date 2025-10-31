using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject pinPrefab;
    public float pinSpeed = 8f;

    private PlayerMovement playerMovement;
    private Vector2 shootDirection = Vector2.up; // Default to shooting up

    void Start()
    {
        // Get reference to PlayerMovement to access movement direction
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Update shoot direction based on movement input
        UpdateShootDirection();

        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Fire when player presses Space or Ctrl
        if (keyboard.spaceKey.wasPressedThisFrame || keyboard.leftCtrlKey.wasPressedThisFrame)
        {
            ShootPin();
        }
    }

    void UpdateShootDirection()
    {
        // Get movement input from PlayerMovement script
        if (playerMovement != null)
        {
            Vector2 moveInput = playerMovement.GetMoveInput();

            // Only update direction if player is actually moving
            if (moveInput.magnitude > 0.1f)
            {
                shootDirection = moveInput.normalized;
            }
        }
    }

    void ShootPin()
    {
        // Spawn the pin
        GameObject pin = Instantiate(pinPrefab, transform.position, Quaternion.identity);

        // Set pin direction based on current movement direction
        var move = pin.GetComponent<PinMovement>();
        if (move != null)
        {
            move.SetDirection(shootDirection);
            move.speed = pinSpeed;
        }
    }
}