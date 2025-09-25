using UnityEngine;
using System.Collections.Generic;
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
        if (currentRoom)
        {
            return;
        }

        currentRoom = true;

        leftRoom = typeOfRoom[0];
        rightRoom =  typeOfRoom[1];

        // Assign sprites
        leftButton.image.sprite = leftRoom.roomSprite;
        rightButton.image.sprite = rightRoom.roomSprite;

        // Assign behavior
        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();
        
        leftButton.onClick.AddListener(() => ApplyRoom(leftRoom));
        rightButton.onClick.AddListener(() => ApplyRoom(rightRoom));
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
            case RoomType.Left:
                Debug.LogWarning("Left Room Chosen");
                break;
            case RoomType.Right:
                Debug.LogWarning("Right Room Chosen");
                break;
        }
    }

}
