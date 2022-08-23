using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBlock : MonoBehaviour
{
    public int BlockPoints;
    public int GradientPoints;

    public MeshRenderer MR;
    public Material blockMat;

    public TextMesh TM;

    private SnakeMovement snake;
    //private SnakeTail tail;

    [SerializeField] private float maxPoints;

    // Start is called before the first frame update
    void Start()
    {
        BlockPoints = (int)Random.Range(1f, 3f);
        GradientPoints = BlockPoints;

        TM = transform.GetChild(0).GetComponent<TextMesh>();
        TM.text = BlockPoints.ToString();

        snake = GameObject.Find("Player").GetComponent<SnakeMovement>();
        //tail = GameObject.Find("Player").GetComponent<SnakeTail>();

        MR = GetComponent<MeshRenderer>();
        MR.material.SetFloat("Vector1_98ad2ead854b468885666a98fcbfb38c", BlockPoints / (maxPoints - 1));
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < BlockPoints; i++)
        {
            snake.RemoveTail();

            if (snake.Length >= 0)
            {
                GradientPoints--;
            }
            else
            {
                snake.Die();
                return;
            }

            if (GradientPoints == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
