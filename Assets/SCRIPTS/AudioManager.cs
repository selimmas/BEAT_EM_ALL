using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip punchClip;
    [SerializeField] AudioSource ambientSound, battleSound;
    [SerializeField] float fadeDuration = 2f;
    [SerializeField] AnimationCurve animCurve;

    public void ChangedeMusique()
    {
        battleSound.Play();
        StartCoroutine(BasculeEntreDeuxAudioSource(ambientSound, battleSound));
        Debug.Log("Son");
    }

    IEnumerator BasculeEntreDeuxAudioSource(AudioSource ambient, AudioSource battle)
    {
        float volume1 = ambient.volume;
        float volume2 = battle.volume;
        float temps = 0;

        while (temps < fadeDuration)
        {
            ambient.volume = Mathf.Lerp(volume1, 0, temps / fadeDuration);
            battle.volume = Mathf.Lerp(volume2, 1, temps / fadeDuration);
            temps = temps + Time.deltaTime;
            yield return null;
        }


        ambient.volume = 0f;
        battle.volume = 1f;


        

    }
}
