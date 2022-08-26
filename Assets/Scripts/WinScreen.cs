using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public Button NextLevelButton;
    public SnakeMovement SM;

    void Start()
    {
        SM = GameObject.Find("Player").GetComponent<SnakeMovement>();

        NextLevelButton = GetComponent<Button>();
        NextLevelButton.onClick.AddListener(NextLevel);
    }

    void NextLevel ()
    {
        SM.ReloadLevel();
    }
}
