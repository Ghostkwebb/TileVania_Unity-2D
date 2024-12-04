using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{

    PlayerMovement player;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundEffectSource;
    public AudioClip background1;
    public AudioClip background2;
    public AudioClip deathSound;


    void Awake()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 0)
        {
            musicSource.clip = background1;
            musicSource.loop = true;
            musicSource.Play();
        }
        else if (currentSceneIndex == 1)
        {
            musicSource.clip = background2;
            musicSource.loop = true;
            musicSource.Play();
        }
    }


    void Update()
    {

    }
}
