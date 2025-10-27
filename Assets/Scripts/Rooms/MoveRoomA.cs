using System.Collections;
using UnityEngine;

public class MoveRoomA : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;       // Speed at which the room moves
    public float moveDistance;    // Distance the room should move forward
    private bool isMoving;        // Tracks whether the room is currently moving

    private Vector3 targetMovePosition; // The position the room is moving towards

    [Header("Teleport Room")]
    public Vector3 targetSpawnPosition;       // Position to teleport to after reaching the teleport point
    public Vector3 targetTeleportPosition;    // Position that triggers the teleport
    public float teleportDistanceThreshold;   // Distance threshold to determine when teleport occurs
    public bool teleported;                   // Tracks if teleport has already occurred

    BattleSystem battleSystem;                // Reference to the BattleSystem script

    // Start is called once before the first execution of Update
    void Start()
    {
        // Find the BattleSystem object by tag and get its component
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();

        battleSystem.moveA = false; // Initialize BattleSystem flag
        isMoving = false;           // Room is not moving initially
        teleported = false;         // Teleport has not occurred yet

        // Set initial positions and movement values
        targetSpawnPosition = new Vector3(0f, 0f, -38f);
        targetTeleportPosition = new Vector3(0f, 0f, 38f);
        teleportDistanceThreshold = 0.01f;
        moveSpeed = 15f;
    }

    private void Update()
    {
        // Check if battle is won and room is not already moving
        if (battleSystem.state == BattleState.WON && !isMoving)
        {
            moveDistance = 38f; // Set how far the room should move
            Debug.Log(isMoving); // Debug log to check movement state
            targetMovePosition = transform.position + transform.forward * moveDistance; // Calculate target position
            isMoving = true; // Start moving
            battleSystem.moveA = false; // Reset BattleSystem flag
            teleported = false; // Reset teleport status
        }
    }

    private void FixedUpdate()
    {
        // Handle movement
        if (isMoving)
        {
            // Move the room smoothly towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetMovePosition, moveSpeed * Time.deltaTime);

            // Check if the room reached the teleport trigger position
            if (Vector3.Distance(transform.position, targetTeleportPosition) <= teleportDistanceThreshold)
            {
                if (!teleported)
                {
                    // Teleport the room to the spawn position
                    transform.position = targetSpawnPosition;
                    teleported = true; // Mark that teleport has occurred
                }

                isMoving = false; // Stop movement after teleport
            }
            // Check if the room reached the final move target position
            else if (Vector3.Distance(transform.position, targetMovePosition) <= teleportDistanceThreshold)
            {
                isMoving = false; // Stop movement
            }
        }
    }
}
