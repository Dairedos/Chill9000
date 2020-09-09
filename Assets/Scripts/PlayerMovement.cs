using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    
    private PlayerActionControl playerActionControls;

    private PlayerCollider playerCollider;

    private float jumpInput;
    private float movementInput;

    private PlayerJump playerJump = new PlayerJump();
    private PlayerSideMove playerSideMove = new PlayerSideMove();

    private Rigidbody2D rb;

    public void GetMoveEvent(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<float>();

    }

    public void GetJumpEvent(InputAction.CallbackContext context)
    {
        jumpInput = context.ReadValue<float>();
    }

    void Awake() {
        playerActionControls = new PlayerActionControl();
    }

    void OnEnable() {
        playerActionControls.Enable();
    }

    void OnDisable()
    {
        playerActionControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
    playerCollider = GetComponentInChildren<PlayerCollider>();
    rb = this.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {

        playerSideMove.SetPlayersDirection(movementInput);

        playerSideMove.MovementSpeedHandling(movementInput);

        playerJump.JumpHandling(jumpInput, playerCollider.CollidingObjects);

        PlayerVelocityHandling();
        

      // Debug.Log("Player Velocity: " + rb.velocity);
      // Debug.Log("Movement input: " + movementInput);
        
    }
    
    //handles player velocity
    private void PlayerVelocityHandling() {

        Vector2 desiredVelocity = new Vector2(playerSideMove.OutputMovementValue, (rb.velocity.y + playerJump.OutputJumpValue));
        rb.velocity = desiredVelocity;
    }
    
}
