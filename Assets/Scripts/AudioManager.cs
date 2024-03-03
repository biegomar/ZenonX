using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The central audio manager.
/// </summary>
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
            var gameSong = AudioManager.Instance.GetSound("GameSong");
            gameSong.Stop();
            
            var sound = AudioManager.Instance.GetSound("TitleSong");
            sound.enabled = true;
            sound.Play();
        }
        
        if (scene.name == "Level")
        {
            var sound = AudioManager.Instance.GetSound("TitleSong");
            sound.Stop();
            
            var gameSong = AudioManager.Instance.GetSound("GameSong");
            gameSong.enabled = true;
            gameSong.Play();
        }
    }
}
