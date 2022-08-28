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
    public GameObject PlayScreen;

    public Text SnakeLenghtText;
    public Text ScoreText;
    public Text WinText;

    public AudioClip CollectSound;
    public AudioClip CollisionSound;

    private AudioSource _audio;
        
    private Camera _mainCamera;
    private Rigidbody _componentRB;
    private SnakeTail _componentST;
    
    private Vector3 _touchLastPos;
    private float _sidewaysSpeed;

    void Start()
    {
        _mainCamera = Camera.main;
        _componentRB = GetComponent<Rigidbody>();
        _componentST = GetComponent<SnakeTail>();
        _audio = GetComponent<AudioSource>();

        Score = 0;
        ScoreText.text = ScorePoints.ToString();

        for (int i = 0; i < Length; i++)
        {
            _componentST.AddBodies();
        }

        SnakeLenghtText.text = "Души : " + Length.ToString();

        Debug.Log(LevelNumber);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _touchLastPos = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _sidewaysSpeed = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = (Vector3)_mainCamera.ScreenToViewportPoint(Input.mousePosition) - _touchLastPos;
            _sidewaysSpeed += delta.x * Sensitivity;
            _touchLastPos = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
    }

   public void AddTail ()
    {
        Length++;
        _componentST.AddBodies();

        _audio.PlayOneShot(CollectSound);
        
        SnakeLenghtText.text = "Души: " + Length.ToString();
    }

   public void RemoveTail ()
    {
        Length--;

        _audio.PlayOneShot(CollisionSound);
        
        ScorePoints++;
        ScoreText.text = ScorePoints.ToString();

        if (Length >= 0)
        {
            _componentST.RemoveBodies();
        }

        if (Length < 0)
        {
            SnakeLenghtText.text = "Души: 0";
            ScorePoints = 0;
        }
        else
        {
            SnakeLenghtText.text = "Души: " + Length.ToString();
        }
    }

    public void Die ()
    {
        ForwardSpeed = 0;
        PlayScreen.SetActive(false);
        LoseScreen.SetActive(true);
    }

    public void ReachFinish()
    {
        ForwardSpeed = 0;
        PlayScreen.SetActive(false);
        WinScreen.SetActive(true);
        WinText.text = "Уровень " + LevelNumber.ToString() + " пройден!";

        LevelNumber++;

        if (LevelNumber > 3)
        {
            LevelNumber = 1;
        }
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
        if (Mathf.Abs(_sidewaysSpeed) > 4)
        {
            _sidewaysSpeed = 4 * Mathf.Sign(_sidewaysSpeed);
        }

        _componentRB.velocity = new Vector3(_sidewaysSpeed * 5, 0, ForwardSpeed);
        _sidewaysSpeed = 0;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
