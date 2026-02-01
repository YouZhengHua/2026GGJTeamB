using System.Collections;
using UnityEngine;

public class bgmContainer : MonoBehaviour
{
    public AudioSource[] ASs = new AudioSource[7];
    
    public void FadeIn(int index)
    {
        StartCoroutine(fadeInCoroutine(ASs[index]));
    }
    IEnumerator fadeInCoroutine(AudioSource audioSource)
    {
        float V = 0f;
        for (int i = 0; i < 10f; i++)
        {
            audioSource.volume += 0.15f;
            yield return new WaitForSeconds(0.1f);

        }
    }
    public void FadeOut(int index)
    {
        StartCoroutine(fadeOutCoroutine(ASs[index]));
    }
    IEnumerator fadeOutCoroutine(AudioSource audioSource)
    {
        for (int i = 0; i < 10f; i++)
        {
            audioSource.volume -= 0.15f;
            yield return new WaitForSeconds(0.1f);

        }
    }
}
