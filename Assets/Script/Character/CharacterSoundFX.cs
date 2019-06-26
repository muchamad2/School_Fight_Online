using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundFX : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audio_attack_1, audio_attack_2, die_sound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Attack_1()
    {
        audioSource.clip = audio_attack_1;
        audioSource.Play();
    }
    public void Attack_2()
    {
        audioSource.clip = audio_attack_2;
        audioSource.Play();
    }
    public void Die()
    {
        audioSource.clip = die_sound;
        audioSource.Play();
    }

}
