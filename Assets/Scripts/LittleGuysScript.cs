using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LittleGuysScript : MonoBehaviour
{
    public Sprite[] guy1;
    public Sprite[] guy2;

    public SpriteRenderer guy1Image;
    public SpriteRenderer guy2Image;

    public void UpdateGuy(int id){
        guy1Image.sprite = guy1[id];
        guy2Image.sprite = guy2[id];
    }
}
