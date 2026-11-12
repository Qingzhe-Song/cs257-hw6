// PlayerMovement.cs
// Maintainer: Qingzhe-Song
// Summary: Converts Move and Look input into relative Rigidbody forces and locks the cursor.

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [InspectorName("Movement Force")]
    [Tooltip("Scales movement input before applying relative force to the Rigidbody.")]
    public float speed;
    [InspectorName("Turning Torque")]
    [Tooltip("Scales horizontal look input before applying torque around the local Y axis.")]
    public float rotationSpeed;

    private Vector2 movementForce;
    private float turningTorque;
    private Rigidbody playerRigidbody;

    public void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerRigidbody = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        // PlayerInput uses the OnMove name to deliver the Move action's value.
        movementForce = value.Get<Vector2>() * speed;
    }

    public void OnLook(InputValue value)
    {
        // Ignore vertical look input because the player turns only around the Y axis.
        turningTorque = value.Get<Vector2>().x * rotationSpeed;
    }

    private void Update()
    {
        playerRigidbody.AddRelativeForce(movementForce.x * Time.deltaTime, 0, movementForce.y * Time.deltaTime);
        playerRigidbody.AddRelativeTorque(0, turningTorque * Time.deltaTime, 0);
    }
}
