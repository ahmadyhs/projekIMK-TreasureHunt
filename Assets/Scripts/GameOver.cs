using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private int previousSceneIndex;

    private void Start()
    {
        // Get the build index of the previous scene
        previousSceneIndex = PlayerPrefs.GetInt("PreviousSceneIndex");
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(previousSceneIndex);
    }
}
