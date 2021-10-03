using UnityEngine.UI;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Difficulty")]
    public Difficulty GameDifficulty;

    [Header("Score")]
    public Text scoreText;
    public int score = 0;

    [Header("Coins")]
    public GameObject coinParticles;

    [Header("Options")]
    public float soundVolume;
    public float musicVolume;
    public AudioSource musicAudioSource;

    public void UpdateScore()
    {
        score++;
        scoreText.text = "" + score;

        SoundManager.Instance.PlaySound(SoundType.Coin, soundVolume);
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
}

public enum Difficulty
{
    Easy,
    Normal,
    Difficult
}
