using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - Player.position;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, Player.position.y + offset.y, Player.position.z + offset.z);
    }
}
