using UnityEngine;

public class MoveRoomTest : MonoBehaviour
{
    public bool moveA;
    public bool moveB;

    public bool moveC;

    private void Update()
    {
        if(moveC == true)
        {
            moveA = true;
            moveB = true;
            moveC = false;
        }
    }
}
