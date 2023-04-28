using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] InputActionReference movement;
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerObj;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private Animator animator;

    // Start is called before the first frame update
    void Start() 
    { 
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movementInput = movement.action.ReadValue<Vector2>();
        float horizontalInput = movementInput.x;
        float verticalIput = movementInput.y;

        moveDirection = orientation.forward * verticalIput + orientation.right * horizontalInput;
        animator.SetBool("isMoving", moveDirection.magnitude > 0);
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public float AttackCooldown = 1f;
    float timeSinceAttack = 0;

    public void Attack(InputAction.CallbackContext context)
    {
        if (timeSinceAttack < AttackCooldown)
            return;

        animator.SetTrigger("isAttacking");

        timeSinceAttack = 0;
    }

    void Update() => timeSinceAttack += Time.deltaTime;

}
