using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player 1")]
    [SerializeField] private TextMeshProUGUI player1ScoreText;
    [SerializeField] private float player1ScoreCount;

    [Header("Player 2")]
    [SerializeField] private TextMeshProUGUI player2ScoreText;
    [SerializeField] private float player2ScoreCount;

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
