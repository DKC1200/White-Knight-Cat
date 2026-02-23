using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMain : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private InputProcessor inputProcessor;
    [SerializeField] private PlayerInteract playerInteract;

    public PlayerMove getMove()
    {
        return playerMove;
    }

    public InputProcessor getInput()
    {
        return inputProcessor;
    }

    public PlayerInteract getInteract()
    {
        return playerInteract;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public int getEnemyCount()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
