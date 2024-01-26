using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player 1")]
    public TextMeshProUGUI player1ScoreText;

    [Header("Player 2")]
    public TextMeshProUGUI player2ScoreText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPlayerBully()
    {
        
    }
}
