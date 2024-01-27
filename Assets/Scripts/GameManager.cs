using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float maxPlayerScore;
    
    [Header("Player 1")]
    public TextMeshProUGUI player1ScoreText;
    
    [Header("Player 2")]
    public TextMeshProUGUI player2ScoreText;

    private ScoreCount scoreCount;

    public MMF_Player winLoseFeedback;

    public Player.PlayerType playerWinner;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreCount = GetComponent<ScoreCount>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckPlayerScoreCount()
    {
        if (scoreCount.Player1Score >= maxPlayerScore)
        {
            playerWinner = Player.PlayerType.BluePlayer;
            winLoseFeedback.PlayFeedbacks();
        }

        if (scoreCount.Player2Score >= maxPlayerScore)
        {
            playerWinner = Player.PlayerType.RedPlayer;
            winLoseFeedback.PlayFeedbacks();
        }
    }
}
