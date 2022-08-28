using UnityEngine;
using Random = System.Random;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] FoodPrefabs;

    public GameObject[] BlockPrefabs;
    public GameObject FirstBlockPrefab;

    public Transform FinishLine;

    public float DistanceBetweenLines;
    public int LineCount;

    public SnakeMovement SM;
    public Block block;

    private void Awake()
    {
        int levelIndex = SM.LevelNumber;

        Random random = new Random(levelIndex);

        for (int i = 3; i < LineCount; i++)
        {
            int blockIndex = RandomRange(random, 0, BlockPrefabs.Length);
            
            if (i == 3)
            {
                GameObject linePrefab = FirstBlockPrefab;
                GameObject line = Instantiate(linePrefab, transform);
                line.transform.position = CalculateLinePosition(i);
            }

            if (i > 3)
            {
                GameObject linePrefab = BlockPrefabs[blockIndex];
                GameObject line = Instantiate(linePrefab, transform);
                line.transform.position = CalculateLinePosition(i);
                // CalculatePositionX
            }

            FinishLine.position = CalculateLinePosition(LineCount);
        }

        for (int i = 0; i < LineCount; i++)
        {
            int foodIndex = RandomRange(random, 0, FoodPrefabs.Length);

            GameObject linePrefab = FoodPrefabs[foodIndex];
            GameObject line = Instantiate(linePrefab, transform);
            line.transform.position = CalculateFoodLinePosition(i);
        }
    }

    private int RandomRange (Random random, int min, int maxExlusive)
    {
        int number = random.Next();
        int length = maxExlusive - min;
        number %= length;

        return min + number;
    }

    private float RandomRange (Random random, float min, float max)
    {
        float t = (float)random.NextDouble();
        return Mathf.Lerp(min, max, t);
    }

    private Vector3 CalculateLinePosition (int lineIndex)
    {
        return new Vector3(0, 0, DistanceBetweenLines * lineIndex); 
    }

    private Vector3 CalculateFoodLinePosition(int lineIndex)
    {
        return new Vector3(0, 0, DistanceBetweenLines * lineIndex + 5f);
    }
}
