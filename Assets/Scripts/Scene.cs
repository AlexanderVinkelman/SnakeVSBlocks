using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void NextScene (int _sceneNumber)
    {
        SceneManager.LoadScene(_sceneNumber);
    }
}
