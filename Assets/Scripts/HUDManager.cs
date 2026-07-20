using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

    public void Pause(InputAction.CallbackContext value)
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        scoreText.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
    }
    public void Unpause(InputAction.CallbackContext value)
    {
        scoreText.gameObject.SetActive(true);
        healthBar.gameObject.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
