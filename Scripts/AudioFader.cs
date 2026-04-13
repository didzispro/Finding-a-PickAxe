using System.Collections;
using UnityEngine;

public class AudioFader : MonoBehaviour
{
    public AudioSource track;
    public float fadeDuration = 2f; // Takes 2 seconds to fade

    public void StartFadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = track.volume;

        while (track.volume > 0)
        {
            // Smoothly reduce volume based on time
            track.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null; 
        }

        track.Stop();
        track.volume = startVolume; // Reset volume for next time
    }
}