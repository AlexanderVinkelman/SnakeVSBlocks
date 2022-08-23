using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : MonoBehaviour
{
    public int FoodPoints;
    public TextMesh TM;

    private SnakeMovement snake;
    //private SnakeTail tail;


    // Start is called before the first frame update
    void Start()
    {
        FoodPoints = (int) Random.Range(1f, 6f);

        
        TM = transform.GetChild(0).GetComponent<TextMesh>();
        TM.text = FoodPoints.ToString();

        snake = GameObject.Find("Player").GetComponent<SnakeMovement>();
        //tail = GameObject.Find("Player").GetComponent<SnakeTail>();
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        
    }*/

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < FoodPoints; i++)
        {
            snake.AddTail();
        }

        Destroy(gameObject);
    }
}
