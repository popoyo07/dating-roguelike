using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChooseRoom : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject chest;
    private GameObject chestInstance;

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

       // public GameObject enemyPrefab;
    }

    public enum RoomType { Chest, Enemy }

    [Header("Rooms")]
    public List<Room> typeOfRoom;

    [Header("Buttons")]
    public Button enemyButton;
    public Button chestButton;

    private Room chestRoom;
    private Room enemyRoom;

    private BattleSystem battleSystem;
    private EnemySpawner enemySpawner;
    void Start()
    {
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        enemySpawner = GameObject.FindWithTag("EnemyS").GetComponent<EnemySpawner>();
    }

    public void ShowRoomOptions()
    {
        if (chestInstance != null)
        {
            Destroy(chestInstance);
        }

        if (currentRoom)
        {
            return;
        }

        currentRoom = true;

        enemyRoom =  typeOfRoom[0];
        chestRoom = typeOfRoom[1];

        // Assign sprites
        enemyButton.image.sprite = enemyRoom.roomSprite;
        chestButton.image.sprite = chestRoom.roomSprite;

        // Assign behavior
        enemyButton.onClick.RemoveAllListeners();
        chestButton.onClick.RemoveAllListeners();

        enemyButton.onClick.AddListener(() => ApplyRoom(enemyRoom));
        chestButton.onClick.AddListener(() => ApplyRoom(chestRoom));
    }
    
    void ApplyRoom(Room room)
    {
        if (!currentRoom)
        {
            return;
        }

        chosenRoom = true;

        switch (room.roomType)
        {
            case RoomType.Enemy:
                Debug.LogWarning("Enemy Room Chosen");
                //enemySpawner.SetNextEnemy(enemyPrefab);
                break;
            case RoomType.Chest:
                Debug.LogWarning("Chest Room Chosen");
                chestInstance = Instantiate(chest, new Vector3(0f, 1.3f, -6.73f), Quaternion.identity);
                break;
        }
    }

}
