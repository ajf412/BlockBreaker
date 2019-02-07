using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    // Config Parameters
    [Range(0.1f, 5f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 5;
    [SerializeField] TextMeshProUGUI scoreText;

    // State Variables
    [SerializeField] int currentScore = 0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        RenderScore();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        Debug.Log("AddToScore Start");
        currentScore += pointsPerBlockDestroyed;
        Debug.Log("AddToScore Mid.");
        RenderScore();
        Debug.Log("AddToScore End");
    }

    private void RenderScore()
    {
        Debug.Log("RenderScore Start");
        scoreText.text = currentScore.ToString();
        Debug.Log("RenderScore End");
    }
}
