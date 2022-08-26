using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SnakeMovement : MonoBehaviour
{
    public float ForwardSpeed = 5;
    public float Sensitivity = 10;

    public int Length = 4;
    public int Score;

    public GameObject WinScreen;
    public GameObject LoseScreen;

    public Button NewLvl;
    public Button ResLvl;

    //public TextMesh TM;
    //public GameObject LengthText;

    public Text SnakeLenghtText;
    public Text ScoreText;
    public Text WinText;

    private Camera mainCamera;
    private Rigidbody componentRB;
    private SnakeTail componentST;

    private Vector3 touchLastPos;
    private float sidewaysSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        componentRB = GetComponent<Rigidbody>();
        componentST = GetComponent<SnakeTail>();

        NewLvl = GetComponent<Button>();
        ResLvl = GetComponent<Button>();

        Score = 0;
        ScoreText.text = ScorePoints.ToString();

        for (int i = 0; i < Length; i++)
        {
            componentST.AddBodies();
        }

        //TM = transform.GetChild(1).GetComponent<TextMesh>();
        //TM.text = Length.ToString();
        SnakeLenghtText.text = "Длина: " + Length.ToString();

        Debug.Log(LevelNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            sidewaysSpeed = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = (Vector3)mainCamera.ScreenToViewportPoint(Input.mousePosition) - touchLastPos;
            sidewaysSpeed += delta.x * Sensitivity;
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
    }

   public void AddTail ()
    {
        Length++;
        componentST.AddBodies();
        //TM.text = Length.ToString();
        SnakeLenghtText.text = "Длина: " + Length.ToString();
    }

   public void RemoveTail ()
    {
        Length--;

        ScorePoints++;
        ScoreText.text = ScorePoints.ToString();

        if (Length >= 0)
        {
            componentST.RemoveBodies();
        }

        if (Length < 0)
        {
            //TM.text = "0";
            SnakeLenghtText.text = "Длина: 0";
            ScorePoints = 0;
        }
        else
        {
            //TM.text = Length.ToString();
            SnakeLenghtText.text = "Длина: " + Length.ToString();
        }
    }

    public void Die ()
    {
        ForwardSpeed = 0;
        
        LoseScreen.SetActive(true);

        //ReloadLevel();
    }

    public void ReachFinish()
    {
        ForwardSpeed = 0;
        
        WinScreen.SetActive(true);
        WinText.text = "Уровень " + LevelNumber.ToString() + " пройден!";

        LevelNumber++;

        if (LevelNumber > 3)
        {
            LevelNumber = 1;
        }

        /*if (Input.GetButton("NextLvlButton"))
        {
            ReloadLevel();
        }*/

        //
    }

    public int LevelNumber
    {
        get => PlayerPrefs.GetInt(LevelNumberKey, 1);

        private set
        {
            PlayerPrefs.SetInt(LevelNumberKey, value);
            PlayerPrefs.Save();
        }
    }

    private const string LevelNumberKey = "LevelNumber";

    public int ScorePoints
    {
        get => PlayerPrefs.GetInt(ScorePointsKey, 0);

        private set
        {
            PlayerPrefs.SetInt(ScorePointsKey, value);
            PlayerPrefs.Save();
        }
    }

    private const string ScorePointsKey = "ScorePoint";

    private void FixedUpdate()
    {
        if (Mathf.Abs(sidewaysSpeed) > 4)
        {
            sidewaysSpeed = 4 * Mathf.Sign(sidewaysSpeed);
        }

        componentRB.velocity = new Vector3(sidewaysSpeed * 5, 0, ForwardSpeed);
        sidewaysSpeed = 0;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
