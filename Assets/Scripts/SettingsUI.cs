using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public void SetMusicVolume(float amount)
    {
        AudioManager.Instance.MusicSource.volume = amount;
    }

    public void SetSFXVolume(float amount)
    {
        AudioManager.Instance.SFXSource.volume = amount;
    }

    public void Scream(float blarg, float blarg2)
    {
        Debug.Log("AAAAAHHHHHHH");
    }
}
