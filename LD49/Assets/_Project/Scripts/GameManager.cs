using UnityEngine.UI;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Difficulty")]
    public Difficulty GameDifficulty;

    [Header("Score")]
    public Text scoreText;
    public ScorePopup scorePopup;
    public Text scoreTextInDeadMenu;
    public int score = 0;

    [Header("Coins")]
    public GameObject coinParticles;

    [Header("Obstacle")]

    public GameObject explosionParticles;

    [Header("Options")]
    public float soundVolume;
    public float musicVolume;
    public AudioSource musicAudioSource;

    private void Start()
    {
        PlayerMovement.OnPlayerDie += GameEnded;
    }

    private void OnDestroy()
    {
        PlayerMovement.OnPlayerDie -= GameEnded;
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "" + score;
        scoreTextInDeadMenu.text = "" + score;

        scorePopup.AnimateScorePopup();
        SoundManager.Instance.PlaySound(SoundType.Coin, soundVolume);
    }

    public void GetPowerUp()
    {
        SoundManager.Instance.PlaySound(SoundType.PowerUp, soundVolume * 2f);
    }

    public void UpdateVolume(VolumeType vt, float newVolume)
    {
        if (vt == VolumeType.Music)
        {
            musicVolume = newVolume;
            musicAudioSource.volume = musicVolume;
        }
        else
            soundVolume = newVolume;
    }

    public void GameEnded()
    {
        SoundManager.Instance.PlaySound(SoundType.PlayerDead, soundVolume);
        Time.timeScale = 0f;
        CanvasManager.Instance.SwitchCanvas(CanvasType.DeadGameMenu);
    }
}

public enum Difficulty
{
    Easy,
    Normal,
    Difficult
}
