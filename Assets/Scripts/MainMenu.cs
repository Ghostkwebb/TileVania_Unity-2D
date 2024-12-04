using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        FindAnyObjectByType<ScenePersist>().resetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
