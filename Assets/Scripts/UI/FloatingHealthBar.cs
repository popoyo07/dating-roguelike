using UnityEngine;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    // Update is called once per frame

    private void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
        target = transform.parent;
    }

    void Update()
    {
        transform.parent.rotation = camera.transform.rotation;
        transform.position = target.position;
    }
}
