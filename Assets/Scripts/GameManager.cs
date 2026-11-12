using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    
    void Awake()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        Time.timeScale = 1;
        playerHealth.OnDied += DisplayLosePanel;
        enemySpawner.OnAllWavesCleared += DisplayWinPanel;
    }

    private void DisplayLosePanel()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void DisplayWinPanel()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
        playerController.enabled = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
    }

}
