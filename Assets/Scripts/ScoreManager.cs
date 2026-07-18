using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int winScore = 50;
    private int score;

    [SerializeField] private CoinSpawner coinSpawn;

    public bool HasAchievedWinScore()
    {
        return score > winScore;
    }

    public void AddScore(int amount)
    {
        if (!HasAchievedWinScore())
        {
            score += amount;
            Debug.Log(score);
            if (score >= winScore)
            {
                GameManager.Instance.TriggerWin();
            }
        }
    }
}
