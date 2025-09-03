using UnityEngine;

public class Gzmos : MonoBehaviour
{
    public Vector3 size = Vector3.one;
    [Header("Set cube color")]
    [ColorUsage(true, true)]
    public Color colorUsage;

    private void OnDrawGizmos()
    {
     
        Gizmos.color = colorUsage; // set gizmos color

        Gizmos.DrawWireCube(transform.position, size);
    }
}
