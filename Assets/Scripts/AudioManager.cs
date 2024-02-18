using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioSource> audioSources;
    
    private static AudioManager instance;
    
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        
        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(gameObject);
    }

    public AudioSource GetSound(string tagName)
    {
        return Instance.audioSources.FirstOrDefault(s => s.CompareTag(tagName));
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Intro")
        {
            var sound = AudioManager.Instance.GetSound("TitleSong");
            sound.enabled = true;
            sound.Play();
        }
        
        if (scene.name == "Level")
        {
            var sound = AudioManager.Instance.GetSound("TitleSong");
            sound.Stop();
        }
    }
}
