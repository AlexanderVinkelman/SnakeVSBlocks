using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnakeMovement : MonoBehaviour
{
    public float ForwardSpeed = 5;
    public float Sensitivity = 10;

    public int Length = 1;

    //public TextMeshPro PointsText;

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

        //PointsText.SetText(Length.ToString());
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            componentST.AddBodies();
            Length++;
            //PointsText.SetText(Length.ToString());
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Length--;
            
            if (Length >= 0)
            {
                componentST.RemoveBodies();
            }
            else
            {
                Debug.Log("Game over");
            }

            
        }
    }

    public void Die ()
    {
        ForwardSpeed = 0;
        Debug.Log("Game over");
    }
    
    private void FixedUpdate()
    {
        if (Mathf.Abs(sidewaysSpeed) > 4)
        {
            sidewaysSpeed = 4 * Mathf.Sign(sidewaysSpeed);
        }

        componentRB.velocity = new Vector3(sidewaysSpeed * 5, 0, ForwardSpeed);
        sidewaysSpeed = 0;
    }
}
