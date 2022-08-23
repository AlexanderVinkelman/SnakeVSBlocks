using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SnakeMovement : MonoBehaviour
{
    public float ForwardSpeed = 5;
    public float Sensitivity = 10;

    public int Length = 4;


    public TextMesh TM;
    public GameObject LengthText;

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

        for (int i = 0; i < Length; i++)
        {
            componentST.AddBodies();
        }

        TM = transform.GetChild(1).GetComponent<TextMesh>();
        TM.text = Length.ToString();

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
        TM.text = Length.ToString();
    }

   public void RemoveTail ()
    {
        Length--;
        if (Length >= 0)
        {
            componentST.RemoveBodies();
        }

        if (Length < 0)
        {
            TM.text = "0";
        }
        else
        {
            TM.text = Length.ToString();
        }
    }

    public void Die ()
    {
        ForwardSpeed = 0;
        Debug.Log("Game over");
        ReloadLevel();
    }

    public void ReachFinish()
    {
        ForwardSpeed = 0;
        Debug.Log("You win");
        LevelNumber++;
        
        if (LevelNumber > 3)
        {
            LevelNumber = 1;
        }
        

        ReloadLevel();
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

    private void FixedUpdate()
    {
        if (Mathf.Abs(sidewaysSpeed) > 4)
        {
            sidewaysSpeed = 4 * Mathf.Sign(sidewaysSpeed);
        }

        componentRB.velocity = new Vector3(sidewaysSpeed * 5, 0, ForwardSpeed);
        sidewaysSpeed = 0;
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
