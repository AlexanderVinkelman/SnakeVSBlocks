using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Transform SnakeHead;
    public float CircleDiameter;

    private List<Transform> snakeBodies = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();

    
    // Start is called before the first frame update
    void Start()
    {
        positions.Add(SnakeHead.position);

    }

    // Update is called once per frame
    void Update()
    {
        float distance = ((Vector3) SnakeHead.position - positions[0]).magnitude;

        if (distance > CircleDiameter)
        {
            Vector3 direction = ((Vector3)SnakeHead.position - positions[0]).normalized;

            positions.Insert(0, positions[0] + direction * CircleDiameter);
            positions.RemoveAt(positions.Count - 1);

            distance -= CircleDiameter;
        }

        for (int i = 0; i < snakeBodies.Count; i++)
        {
            snakeBodies[i].position = Vector3.Lerp(positions[i + 1], positions[i], distance / CircleDiameter);
        }
    }

    public void AddBodies ()
    {
        Transform body = Instantiate(SnakeHead, positions[positions.Count - 1], Quaternion.identity, transform);
        snakeBodies.Add(body);
        positions.Add(body.position);
    }

    public void RemoveBodies ()
    {
        Destroy(snakeBodies[0].gameObject);
        snakeBodies.RemoveAt(0);
        positions.RemoveAt(1);
    }
}
