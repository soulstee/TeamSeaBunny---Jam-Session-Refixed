using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overworld : MonoBehaviour
{
    public static Overworld instance;
    public static bool daytime = true;

    public Image background;
    public GameObject[] characters = new GameObject[3];
    public Sprite[] backgrounds;

    private void Awake(){
        instance = this;
        daytime = !GeneralAudioPlayer.finishedDaytime;

        if(daytime){
            background.sprite = backgrounds[0];
            characters[0].SetActive(true);
        }else if(!daytime){
            background.sprite = backgrounds[1];
            characters[1].SetActive(true);
            characters[2].SetActive(true);

            //GeneralAudioPlayer.instance.
        }
    }
}
