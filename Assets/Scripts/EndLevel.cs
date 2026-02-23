using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private Manager manager;
    [SerializeField] private Collider2D endTrigger;

    [SerializeField] private float displayTime;
    [SerializeField] private Text myText;

    void Start()
    {
        if(SceneManager.GetActiveScene().name != "Start" && SceneManager.GetActiveScene().name != "Win"){
            StartCoroutine(ShowText(SceneManager.GetActiveScene().name, displayTime));
        }
    }

    IEnumerator ShowText(string message, float duration)
    {
        myText.text = message;
        Color color = myText.color;
        color.a = 1f;
        myText.color = color;
        myText.gameObject.SetActive(true);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = 1f - (elapsed / duration);
            myText.color = color;
            yield return null;
        }

        color.a = 0f;
        myText.color = color;
        myText.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            manager.next_scene();
        }
    }
}
