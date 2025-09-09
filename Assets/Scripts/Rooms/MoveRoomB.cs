using System.Collections;
using UnityEngine;

public class MoveRoomB : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;
    public float moveDistance;
    private bool isMoving;

    private Vector3 targetMovePosition;

    [Header("Teleport Room")]
    public Vector3 targetSpawnPosition;
    public Vector3 targetTeleportPosition;
    public float teleportDistanceThreshold;
    private bool teleported;

    BattleSystem battleSystem;
    MoveRoomTest moveRoomTest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        moveRoomTest = GameObject.FindWithTag("MoveRoomTesting").GetComponent<MoveRoomTest>();

        moveRoomTest.move = false;
        isMoving = false;
        teleported = false;

        targetSpawnPosition = new Vector3(0f, 0f, -38f);
        targetTeleportPosition = new Vector3(0f, 0f, 38f);
        teleportDistanceThreshold = 0.01f;
        moveSpeed = 15f;
    }

    private void Update()
    {
        //if (battleSystem.state == BattleState.WON && !isMoving)
        if (moveRoomTest.move && !isMoving)
        {
            moveDistance = 38f;

            targetMovePosition = transform.position + transform.forward * moveDistance;
            isMoving = true;
            moveRoomTest.move = false;
            teleported = false;
        }

        // Handle movement
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetMovePosition, moveSpeed * Time.deltaTime);

            // When reached targetTeleportLocation
            if (Vector3.Distance(transform.position, targetTeleportPosition) <= teleportDistanceThreshold)
            {
                if (!teleported)
                {
                    transform.position = targetSpawnPosition;
                    teleported = true;
                }

                isMoving = false;
            }
            else if (Vector3.Distance(transform.position, targetMovePosition) <= teleportDistanceThreshold)
            {
                isMoving = false;
            }
        }
    }
}