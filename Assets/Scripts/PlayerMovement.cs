using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    GameObject playerSprite;

    [SerializeField]
    GameObject emoteSprite;

    private Animator playerSpriteAnimator;
    private Animator emoteSpriteAnimator;

    private PlayerActionControl playerActionControls;

    private PlayerCollider playerCollider;

    private float jumpInput;
    private float movementInput;

    private PlayerJump playerJump = new PlayerJump();
    private PlayerSideMove playerSideMove = new PlayerSideMove();

    private Rigidbody2D rb;

    private RangeValue<float> EmoteTimerValue = new RangeValue<float>(0f, 5f, 0f);

    private const string EMOTE_ACTION_VAR = "DoAction";

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
    playerSpriteAnimator = playerSprite.GetComponent<Animator>();
    emoteSpriteAnimator = emoteSprite.GetComponent<Animator>();

        playerCollider = this.GetComponentInChildren<PlayerCollider>();
    rb = this.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {

        playerSideMove.SetPlayersDirection(movementInput);

        playerSideMove.MovementSpeedHandling(movementInput);

        playerJump.JumpHandling(jumpInput, playerCollider.CollidingObjects);

        PlayerVelocityHandling();

        PlayerMovementAnimator();

        EmoteAnimator();

        // Debug.Log("Player Velocity: " + rb.velocity);
        // Debug.Log("Movement input: " + movementInput);
    }
    
    //handles player velocity
    private void PlayerVelocityHandling() {
        Vector2 desiredVelocity = new Vector2(playerSideMove.OutputMovementValue, (rb.velocity.y + playerJump.OutputJumpValue));
        rb.velocity = desiredVelocity;
    }

    private void EmoteAnimator() {

        bool playerIsMoving = !movementInput.Equals(0f);
        bool playerNotOnGround = !playerJump.IsOnGround;

        if (playerIsMoving || playerNotOnGround)
        {
            EmoteTimerValue.Current = 0;
        }
        else {
            EmoteTimerValue.Current += Time.deltaTime;
        }
        
        if (EmoteTimerValue.Current > EmoteTimerValue.Maximum)
        {
            emoteSpriteAnimator.SetBool(EMOTE_ACTION_VAR, true);
        }
        else {
            emoteSpriteAnimator.SetBool(EMOTE_ACTION_VAR, false);
        }
    }

    private void PlayerMovementAnimator() {

        if (playerJump.IsOnGround)
            playerSpriteAnimator.SetBool("OnGround", true);
        else
            playerSpriteAnimator.SetBool("OnGround", false);

        if (movementInput.Equals(0f))
        {
            playerSpriteAnimator.SetBool("Standstill", true);
            playerSpriteAnimator.SetFloat("X_Axis", 0f);
        }
        else if (movementInput > 0f)
        {
            playerSpriteAnimator.SetFloat("X_Axis", movementInput);
            playerSpriteAnimator.SetBool("Standstill", false);
        }
        else if (movementInput < 0f)
        {
            playerSpriteAnimator.SetFloat("X_Axis", movementInput);
            playerSpriteAnimator.SetBool("Standstill", false);
        }

        if (playerJump.JumpedInAir)
            playerSpriteAnimator.SetBool("JumpedInAir", true);
        else
            playerSpriteAnimator.SetBool("JumpedInAir", false);

    }

    
    
}
