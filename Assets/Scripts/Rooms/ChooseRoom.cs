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
    public bool firstPick;
    private bool roomSelectOnce;
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
    public List<Room> roomRewards;

    [Header("Buttons")]
    public Button button1;
    public Button button2;

    private Room chestRoom;
    private Room enemyRoom;

    private BattleSystem battleSystem;
    private EnemySpawner enemySpawner;
    void Start()
    {
        battleSystem = GameObject.FindWithTag("BSystem").GetComponent<BattleSystem>();
        enemySpawner = GameObject.FindWithTag("EnemyS").GetComponent<EnemySpawner>();

        firstPick = true;
    }

    void Update()
    {
        if (battleSystem.state == BattleState.WON && !chosenRoom && !roomSelectOnce)
        {
            ShowRoomOptions();
            roomSelectOnce = true;
        }
    }

    public void ShowRoomOptions()
    {
        if (currentRoom)
        {
            return;
        }

        currentRoom = true;

        // Assign sprites
        button1.image.sprite = enemyRoom.roomSprite;
        button2.image.sprite = chestRoom.roomSprite;

        // Assign behavior
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();

        button1.onClick.AddListener(() => ApplyRoom(enemyRoom));
        button2.onClick.AddListener(() => ApplyRoom(chestRoom));
    }
    
    void ApplyRoom(Room room)
    {
        if (!currentRoom)
        {
            return;
        }

        chosenRoom = true;
        openRoomPop = false;
        firstPick = false;
        roomSelectOnce = false;

        switch (room.roomType)
        {
            case RoomType.Enemy:
                Debug.LogWarning("Enemy Room Chosen");
                enemySpawner.SetNextEnemy(enemyPrefab);
                break;
            case RoomType.Chest:
                Debug.LogWarning("Chest Room Chosen");
                chestInstance = Instantiate(chest, new Vector3(0f, 1.3f, -6.73f), Quaternion.identity);
                break;
        }
    }
}
