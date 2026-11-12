using UnityEngine;
using TMPro;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    public TMP_Text text;

    void Awake()
    {
        enemySpawner.OnWaveStarted += DisplayWave;
    }

    void DisplayWave(int waveNumber, int waves)
    {
        text.SetText("Wave " + waveNumber + " / " + waves);
    }
}
