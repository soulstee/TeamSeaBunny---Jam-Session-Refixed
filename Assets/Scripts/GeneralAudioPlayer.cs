using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GeneralAudioPlayer : MonoBehaviour
{
    private AudioSource source;

    void Awake(){
        DontDestroyOnLoad(this.gameObject);
        source = GetComponent<AudioSource>();
        source.Play();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Debug.Log(scene.name);
        if(scene.name.IndexOf("Rhythm") == 0){
            source.Pause();
        }else{
            source.UnPause();
        }
    }
}
