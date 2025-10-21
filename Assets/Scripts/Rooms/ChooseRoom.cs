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
        public GameObject enemyPrefab;
    }

    public enum RoomType { Left, Right }

    [Header("Buttons")]
    public Button leftButton;
    public Button rightButton;

    [Header("Boss UI Override")]
    public Sprite defaultBossSprite;

    private Room leftRoom;
    private Room rightRoom;

    private EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner = GameObject.FindWithTag("EnemyS").GetComponent<EnemySpawner>();
    }

    public void ShowRoomOptions()
    {
        if (currentRoom) return;
        currentRoom = true;

        List<GameObject> activeEnemies = enemySpawner.GetActiveList();

        if (activeEnemies == null || activeEnemies.Count == 0)
        {
            Debug.LogWarning("No active enemies available for room options.");
            return;
        }

        // pick two random enemies
        int leftIndex = Random.Range(0, activeEnemies.Count);
        int rightIndex;
        do { rightIndex = Random.Range(0, activeEnemies.Count); } while (rightIndex == leftIndex);

        GameObject leftEnemy = activeEnemies[leftIndex];
        GameObject rightEnemy = activeEnemies[rightIndex];

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

        //Set normal enemy sprites first
        leftButton.image.sprite = leftRoom.roomSprite;
        rightButton.image.sprite = rightRoom.roomSprite;

        //Check boss condition and override if needed
        if (enemySpawner.roomsSpawnBoss == 5 ||
            enemySpawner.roomsSpawnBoss == 11 ||
            enemySpawner.roomsSpawnBoss == 17)
        {
            if (defaultBossSprite != null)
            {
                leftButton.image.overrideSprite = defaultBossSprite;
                rightButton.image.overrideSprite = defaultBossSprite;
                Debug.Log("Boss room detected — overriding room button sprites.");
            }
            else
            {
                Debug.LogWarning("defaultBossSprite is not assigned in Inspector.");
            }
        }

        //Assign button clicks
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
        enemySpawner.QueueSpecificEnemy(room.enemyPrefab);
        currentRoom = false;
    }
}