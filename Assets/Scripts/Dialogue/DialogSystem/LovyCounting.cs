using UnityEngine;

[System.Serializable]
public class LovyCounting
{
    [SerializeField] private int LovyCount;
    [SerializeField] private int MinusLovyCount;

    public int lovyCount => LovyCount;
    public int minusLovyCount => MinusLovyCount;
}
