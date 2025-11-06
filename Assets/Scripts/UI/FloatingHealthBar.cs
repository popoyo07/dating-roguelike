using UnityEngine;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    // Update is called once per frame

    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        target = transform.parent;
    }

    void Update()
    {
        transform.parent.rotation = cam.transform.rotation;
        transform.position = target.position;
    }
}
