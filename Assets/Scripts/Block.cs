using UnityEngine;

public class Block : MonoBehaviour
{
    public int BlockPoints;
    public int GradientPoints;

    public MeshRenderer MR;
    public Material blockMat;

    public TextMesh TM;

    private SnakeMovement _snake;

    [SerializeField] private float maxPoints;
    
    void Start()
    {
        BlockPoints = (int)Random.Range(1f, maxPoints);
        GradientPoints = BlockPoints;

        TM = transform.GetChild(0).GetComponent<TextMesh>();
        TM.text = BlockPoints.ToString();

        _snake = GameObject.Find("Player").GetComponent<SnakeMovement>();

        MR = GetComponent<MeshRenderer>();
        MR.material.SetFloat("Vector1_98ad2ead854b468885666a98fcbfb38c", BlockPoints/(maxPoints - 1));
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < BlockPoints; i++)
        {
            _snake.RemoveTail();
            
            if (_snake.Length >= 0)
            {
                GradientPoints--;
            }
            else
            {
                _snake.Die();
                return;
            }

            if (GradientPoints == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
