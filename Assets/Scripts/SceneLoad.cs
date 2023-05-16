using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public SceneReference targetScene;

    public void LoadScene()
    {
        SceneManager.LoadScene(targetScene);
    }
}
