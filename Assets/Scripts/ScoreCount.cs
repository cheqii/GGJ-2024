using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    [Header("Player 1")]
    [SerializeField] private float player1Score;
    
    [Header("Player 2")]
    [SerializeField] private float player2Score;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreasePlayer1Score(int score)
    {
        player1Score += score;
        GameManager.Instance.player1ScoreText.text = player1Score.ToString();
    }

    public void IncreasePlayer2Score(int score)
    {
        player2Score += score;
        GameManager.Instance.player2ScoreText.text = player2Score.ToString();
    }
}
