using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;
    public AudioSource hitmarket;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        hitmarket = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        hitmarket.PlayOneShot(sound);
    }
}
