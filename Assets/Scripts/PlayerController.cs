using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private InputAction moveAction;
    private Vector2 moveInput;
    private Rigidbody2D rigidBody;

    private float playerHalfSizeX;
    private float playerHalfSizeY;
    private float horizontalLimit;
    private float verticalLimit;

    // Only read Player GameObject components - best used in `Awake()`
    void Awake() {
        moveAction = InputSystem.actions.FindAction("Move");
        rigidBody = GetComponent<Rigidbody2D>();

        // Using extents because we need half size of Player GameObject
        Bounds bounds = GetComponent<SpriteRenderer>().bounds;
        playerHalfSizeX = bounds.extents.x;
        playerHalfSizeY = bounds.extents.y;
    }

    // Player GameObject interacts with other GameObjects - best used in `Start()`
    void Start() {
        // Assumes the camera sits at (0,0) and the window isn't resized during play
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        horizontalLimit = cameraWidth / 2 - playerHalfSizeX;
        verticalLimit = cameraHeight / 2 - playerHalfSizeY;
    }

    // Read inputs on every frame
    // `FixedUpdate()` can run 0 or several times per frame, so one-frame events (presses) can be missed or double-counted
    void Update() {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    // Adjust physics objects here because we want consistent movement
    // If we did that in `Update()` the objects could move erratically or stutter with variations in frame rate
    void FixedUpdate() {
        Vector2 velocity = moveInput * moveSpeed;
        Vector2 playerPosition = rigidBody.position; // copy the Player position - write back after changing

        // Physics move the body by its velocity AFTER `FixedUpdate()`, so clamping the
        // position alone lets the player be drawn one step past the edge
        // Stop any velocity that points out of the arena
        if (playerPosition.x >= horizontalLimit && velocity.x > 0) {
            velocity.x = 0;
        }

        if (playerPosition.x <= -horizontalLimit && velocity.x < 0) {
            velocity.x = 0;
        }

        if (playerPosition.y >= verticalLimit && velocity.y > 0) {
            velocity.y = 0;
        }

        if (playerPosition.y <= -verticalLimit && velocity.y < 0) {
            velocity.y = 0;
        }

        rigidBody.linearVelocity = velocity;

        // The velocity check above only stop movement once the Player is past the limit,
        // so the Player can overshoot by one step on arrival. Clamping the position pulls it back
        playerPosition.x = Mathf.Clamp(playerPosition.x, -horizontalLimit, horizontalLimit);
        playerPosition.y = Mathf.Clamp(playerPosition.y, -verticalLimit, verticalLimit);
        
        // Write the Player position back inside its Component
        rigidBody.position = playerPosition;
    }
}
