using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCam : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] InputActionReference movement;

    // Update is called once per frame
    void Update()
    {
        Vector2 movementInput = movement.action.ReadValue<Vector2>();
        float horizontalInput = movementInput.x;
        float verticalIput = movementInput.y;

        Vector3 viewDir = player.position - new Vector3(Camera.main.transform.position.x, player.position.y, Camera.main.transform.position.z);
        orientation.forward = viewDir.normalized;

        Vector3 inputDir = orientation.forward * verticalIput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            player.forward = Vector3.Slerp(player.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
