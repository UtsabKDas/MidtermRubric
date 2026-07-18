using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int winScore = 300;

    private bool isGameOver;

    public bool IsGameOver => isGameOver;

    public int WinScore => winScore;

    private void Awake()
    {
        Instance = this;
    }

    public void TriggerWin()
    {
        if (isGameOver)
        {
            return;
        }
        isGameOver = true;
        Debug.Log("You win");
    }

    public void TriggerLose()
    {
        if (isGameOver)
        {
            return;
        }
        isGameOver = true;
        Debug.Log("You lose");
    }
}
