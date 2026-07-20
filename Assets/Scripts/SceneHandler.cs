using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneHandler : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);    
    }
    public void LoadNewScene(int newSceneIndex)
    {
        SceneManager.LoadScene(newSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
