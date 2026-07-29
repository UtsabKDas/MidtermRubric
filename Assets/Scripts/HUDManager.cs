using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Health")]
    [SerializeField] private Image healthBar;

    [Header("Pause")]
    public bool IsPaused { get; private set; }
    [SerializeField] private GameObject pauseMenu;

    private bool isPaused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int newScore)
    {
        scoreText.SetText("Score: {0:000}", newScore);
    }

    public void UpdateHealthBar(float healthPercentage)
    {
        healthBar.fillAmount = healthPercentage;
    }

    public void UpdateHealthBar(int x, int u)
    {
        healthBar.fillAmount = (float) x / (float) u;
    }

    public void OnPauseInputPressed(InputAction.CallbackContext value)
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
            
        pauseMenu.SetActive(isPaused);
        scoreText.gameObject.SetActive(!isPaused);
        healthBar.gameObject.SetActive(!isPaused);
    }
    public void Unpause()
    {
        isPaused = false;
        scoreText.gameObject.SetActive(true);
        healthBar.gameObject.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
