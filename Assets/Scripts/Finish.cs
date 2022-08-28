using UnityEngine;

public class Finish : MonoBehaviour
{
    private SnakeMovement snake;

    private void Start()
    {
        snake = GameObject.Find("Player").GetComponent<SnakeMovement>();
    }

    
    private void OnTriggerExit(Collider other)
    {
        snake.ReachFinish();
    }
}
