using TMPro;
using UnityEngine;

public class MoveRooms : MonoBehaviour
{
    public float roomSpeed;
    public float moveTimer;
    public GameObject rooms;

    public bool test;

    public bool teleport = false;
    public Vector3 targetPosition;

    // BattleSystem battleSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        moveTimer = 0f;
        roomSpeed = 9.5f;
        test = false;
        targetPosition = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (teleport)
        {
            rooms.transform.position = targetPosition;
            teleport = false;
        }

        //if (battleSystem.state == BattleState.WON && moveTimer <= 0f)
        if (test == true && moveTimer <= 0f)
        {
            moveTimer = 3.9999f;
        }

        if (moveTimer > 0f)
        {
            moveTimer -= Time.deltaTime;
            transform.Translate(Vector3.forward * roomSpeed * Time.deltaTime);

            if (moveTimer <= 0f)
            {
                moveTimer = 0f;
                test = false;
            }
        }
    }
}