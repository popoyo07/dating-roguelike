using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseRoom : MonoBehaviour
{
    [Header("Bools")]
    //public bool openRoomPop; // Tracks if the room selection popup should be open
    public bool chosenRoom;  // Tracks if the player has chosen a room
    public bool currentRoom; // Ensures room selection is only active once at a time

    [System.Serializable]
    public class Room
    {
        public string roomName;       // Name of the room (for identification)
        public Sprite roomSprite;     // Sprite to display for the room button
        public RoomType roomType;     // Enum type indicating Left or Right room
        public GameObject enemyPrefab; // Enemy prefab associated with the room
    }

    public enum RoomType { Left, Right } // Simple enum for room positioning

    [Header("Buttons")]
    public Button leftButton;  // Button for left room choice
    public Button rightButton; // Button for right room choice

    [Header("Boss UI Override")]
    public Sprite defaultBossSprite; // Sprite to override normal enemies when a boss room appears

    private Room leftRoom;  // Holds data for the left room option
    private Room rightRoom; // Holds data for the right room option

    private EnemySpawner enemySpawner; // Reference to EnemySpawner in the scene

    void Start()
    {
        // Find the EnemySpawner object by tag and get its component
        enemySpawner = GameObject.FindWithTag("EnemyS").GetComponent<EnemySpawner>();
    }

    public void ShowRoomOptions()
    {
        if (currentRoom) return; // Prevent opening room selection multiple times
        currentRoom = true;      // Mark that the room selection is active

        // Get the current list of active enemies
        List<GameObject> activeEnemies = enemySpawner.GetActiveList();

        if (activeEnemies == null || activeEnemies.Count == 0)
        {
            Debug.LogWarning("No active enemies available for room options.");
            return; // Exit if no enemies available
        }

        // Pick two random enemies from the active list
        int leftIndex = Random.Range(0, activeEnemies.Count);
        int rightIndex;
        do { rightIndex = Random.Range(0, activeEnemies.Count); } while (rightIndex == leftIndex);

        GameObject leftEnemy = activeEnemies[leftIndex];
        GameObject rightEnemy = activeEnemies[rightIndex];

        // Create Room objects for left and right buttons
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

        // Set normal enemy sprites for buttons first
        if (enemySpawner.roomsSpawnBoss != 5 || enemySpawner.roomsSpawnBoss != 11 || enemySpawner.roomsSpawnBoss != 17)
        {
            leftButton.image.sprite = leftRoom.roomSprite;
            rightButton.image.sprite = rightRoom.roomSprite;
        }

        // Check if the current room should be a boss room
        if (enemySpawner.roomsSpawnBoss == 6 ||
            enemySpawner.roomsSpawnBoss == 12 ||
            enemySpawner.roomsSpawnBoss == 18)
        {
            if (defaultBossSprite != null)
            {
                // Override button sprites with boss sprite
                leftButton.image.sprite = defaultBossSprite;
                rightButton.image.sprite = defaultBossSprite;
                Debug.Log("Boss room detected — overriding room button sprites.");
            }
            else
            {
                Debug.LogWarning("defaultBossSprite is not assigned in Inspector.");
            }
        }

        // Remove previous listeners to avoid multiple triggers
        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();

        // Assign click events to apply the chosen room
        leftButton.onClick.AddListener(() => ApplyRoom(leftRoom));
        rightButton.onClick.AddListener(() => ApplyRoom(rightRoom));

        Debug.Log("Room options displayed.");
    }
  

    void ApplyRoom(Room room)
    {
        if (!currentRoom) return; // Ensure a room is currently active before applying

        chosenRoom = true;  // Mark that the player has chosen a room

        enemySpawner.QueueSpecificEnemy(room.enemyPrefab); // Queue the chosen enemy to spawn
        currentRoom = false; // Reset the room selection state

        
    }
}