using UnityEngine;

public class Food : MonoBehaviour
{
    public int FoodPoints;
    public TextMesh TM;

    private SnakeMovement snake;

    void Start()
    {
        FoodPoints = (int) Random.Range(1f, 6f);
                
        TM = transform.GetChild(0).GetComponent<TextMesh>();
        TM.text = FoodPoints.ToString();

        snake = GameObject.Find("Player").GetComponent<SnakeMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < FoodPoints; i++)
        {
            snake.AddTail();
        }
        
        Destroy(gameObject);
    }
}
