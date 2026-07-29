using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private int winScore = 50;
    public int Score { get; set; }

    [Header("Coin")]
    [SerializeField] private CoinSpawner coinSpawn;

    [SerializeField] private HUDManager hud;

    private Dictionary<Coin.CoinType, int> numOfEachCoinType = new Dictionary<Coin.CoinType, int>();

    private void Start()
    {
        numOfEachCoinType.Add(Coin.CoinType.bronze, 0);
        numOfEachCoinType.Add(Coin.CoinType.silver, 0);
        numOfEachCoinType.Add(Coin.CoinType.gold, 0);

        if (hud == null)
        {
            hud = GameManager.Instance.HUDManager;
        }
    }

    public bool HasAchievedWinScore()
    {
        return Score > winScore;
    }

    public bool HasNotAchievedWinScore()
    {
        return Score < winScore;
    }

    public void AddScore(Coin.CoinType coinType, int amount)
    {
        if (!HasAchievedWinScore())
        {
            Score += amount;
            hud.UpdateScore(Score);
            
            numOfEachCoinType[coinType]++;
            Debug.Log(Score);
            if (Score >= winScore)
            {
                GameManager.Instance.TriggerWin();
            }
        }
    }
}
