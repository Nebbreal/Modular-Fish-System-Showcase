using System;
using System.Linq;
using UnityEngine;

public class FishingRodSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private PressureDependantAudioClip[] audioClips;

    private void Start()
    {
        audioClips = audioClips.OrderByDescending(audioClip => audioClip.pressureThreshold).ToArray();
    }

    public void PlaySound(float pressure, float lastPressure)
    {
        float pressureDifference = Mathf.Abs(pressure - lastPressure);

        foreach (var audioClip in audioClips)
        {
            if (pressureDifference > audioClip.pressureThreshold)
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();

                audioSource.resource = audioClip.clip;
                return;
            }
        }
        audioSource.Stop();
    }
}

[Serializable]
public class PressureDependantAudioClip
{
    public AudioClip clip;
    public float pressureThreshold;
}