using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private int winScore = 50;
    private int score;

    [Header("Coin")]
    [SerializeField] private CoinSpawner coinSpawn;

    [SerializeField] private HUDManager hud;

    private void Start()
    {
        if(hud == null)
        {
            hud = GameManager.Instance.HUDManager;
        }
    }

    public bool HasAchievedWinScore()
    {
        return score > winScore;
    }

    public bool HasNotAchievedWinScore()
    {
        return score < winScore;
    }

    public void AddScore(int amount)
    {
        if (!HasAchievedWinScore())
        {
            score += amount;
            hud.UpdateScore(score);
            Debug.Log(score);
            if (score >= winScore)
            {
                GameManager.Instance.TriggerWin();
            }
        }
    }
}
