using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] InputActionReference interaction;
    private IInteractable currentInteractable = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            currentInteractable = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null && other.GetComponent<IInteractable>() == currentInteractable)
        {
            currentInteractable = null;
        }
    }

    public void TryInteract(InputAction.CallbackContext context)
    {
        if (currentInteractable != null)
            currentInteractable.Interact();
    }
}
