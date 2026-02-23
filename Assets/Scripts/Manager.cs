using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    Scene activeScene;

    public void next_scene()
    {
        activeScene = SceneManager.GetActiveScene();
        switch (activeScene.name){
            case "Start":
                SceneManager.LoadScene("Level1", LoadSceneMode.Single);
                break;
            case "Level1":
                SceneManager.LoadScene("Level2", LoadSceneMode.Single);
                break;
            case "Level2":
                SceneManager.LoadScene("Level3", LoadSceneMode.Single);
                break;
            case "Level3":
                SceneManager.LoadScene("Win", LoadSceneMode.Single);
                break;
            case "Win":
                SceneManager.LoadScene("Start", LoadSceneMode.Single);
                break;
        }
    }
}
