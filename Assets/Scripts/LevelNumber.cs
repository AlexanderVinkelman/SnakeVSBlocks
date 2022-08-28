using UnityEngine;
using UnityEngine.UI;

public class LevelNumber : MonoBehaviour
{
    public Text LeverNumberText;
    public Text SnakeLenghtText;
    SnakeMovement SM;

    void Start()
    {
        SM = GameObject.Find("Player").GetComponent<SnakeMovement>();

        LeverNumberText.text = (SM.LevelNumber).ToString();
        SnakeLenghtText.text = "Длина: " + (SM.Length).ToString();
    }
}
