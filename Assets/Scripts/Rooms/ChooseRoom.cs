using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseRoom : MonoBehaviour
{
    [Header("Bools")]
    public bool openRoomPop;
    public bool chosenRoom;
    public bool currentRoom;

    [System.Serializable]
    public class Room
    {
        public string roomName;
        public Sprite roomSprite;
        public RoomType roomType;
        public GameObject enemyPrefab; // linked to enemy from activeList
    }

    public enum RoomType { Left, Right }

    [Header("Rooms")]
    public List<Room> typeOfRoom;

    [Header("Buttons")]
    public Button leftButton;
    public Button rightButton;

    private Room leftRoom;
    private Room rightRoom;

    private BattleSystem battleSystem;
    private EnemySpawner enemySpawner;

    void Start()
    {
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        enemySpawner = GameObject.FindWithTag("EnemyS").GetComponent<EnemySpawner>();
    }

    public void ShowRoomOptions()
    {
        if (currentRoom) return;
        currentRoom = true;

        // Get active enemy list from spawner
        List<GameObject> activeEnemies = enemySpawner.GetActiveList();

        if (activeEnemies == null || activeEnemies.Count == 0)
        {
            Debug.LogWarning("No active enemies available for room options.");
            return;
        }

        // Pick two different enemies for left/right options
        int leftIndex = Random.Range(0, activeEnemies.Count);
        int rightIndex;
        do { rightIndex = Random.Range(0, activeEnemies.Count); } while (rightIndex == leftIndex);

        GameObject leftEnemy = activeEnemies[leftIndex];
        GameObject rightEnemy = activeEnemies[rightIndex];

        // Create room data
        leftRoom = new Room
        {
            roomName = "Left Room",
            roomSprite = leftEnemy.GetComponentInChildren<SpriteRenderer>().sprite,
            roomType = RoomType.Left,
            enemyPrefab = leftEnemy
        };

        rightRoom = new Room
        {
            roomName = "Right Room",
            roomSprite = rightEnemy.GetComponentInChildren<SpriteRenderer>().sprite,
            roomType = RoomType.Right,
            enemyPrefab = rightEnemy
        };

        // Set button visuals
        leftButton.image.sprite = leftRoom.roomSprite;
        rightButton.image.sprite = rightRoom.roomSprite;

        // Set click behavior
        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();

        leftButton.onClick.AddListener(() => ApplyRoom(leftRoom));
        rightButton.onClick.AddListener(() => ApplyRoom(rightRoom));

        Debug.Log("Room options displayed.");
    }

    void ApplyRoom(Room room)
    {
        if (!currentRoom) return;

        chosenRoom = true;
        Debug.Log($"Room chosen: {room.roomName}");

        // Queue the chosen enemy for the next delayed spawn
        enemySpawner.QueueSpecificEnemy(room.enemyPrefab);
        currentRoom = false;
    }
}