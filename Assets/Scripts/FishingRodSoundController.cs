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
        var audioClip = audioClips.First(clip => clip.pressureThreshold < pressureDifference);

        if (audioClip == null)
        {
            audioSource.Stop();
            return;
        }

        if (!audioSource.isPlaying)
            audioSource.Play();

        audioSource.resource = audioClip.clip;
    }
}