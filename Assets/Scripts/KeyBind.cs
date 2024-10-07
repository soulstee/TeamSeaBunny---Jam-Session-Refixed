using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBind : MonoBehaviour
{
    public TMP_Dropdown drop;
    public GameObject custom;

    public KeyButton[] buttons;

    private void Start(){
        SetAllBinds();
        SetAllText();
    }

    public void ButtonSelect(int num){
        buttons[num].ChangingBind();
    }

    private void Update(){
        foreach(KeyButton b in buttons){
            if(b.changing){
                if(Input.anyKeyDown){
                    string c = Input.inputString;
                    b.SetBind(c);
                }
            }
        }
    }

    public void SetAllBinds(){
        if(drop.value == 0){
            Data.BindFind("Horizontal");
            SetAllText();
        }else if(drop.value == 1){
            Data.BindFind("Vertical");
            SetAllText();
        }else if(drop.value == 2){
            Data.BindFind("SixShooter");
            SetAllText();
        }else if(drop.value == 3){
            custom.SetActive(true);
        }
    }

    private void SetAllText(){
        for(int i = 0; i < buttons.Length; i++){
            buttons[i].onGraphicText.text = Data.keys[i].ToString();
            if(Data.keys[i].ToString().IndexOf("Alpha") == 0){
                buttons[i].onGraphicText.text = Data.keys[i].ToString().Substring(5);
            }
        }
    }

}

[System.Serializable]
public class KeyButton{

    public int ID;
    public Button button;
    private string textWords = "";
    public TextMeshProUGUI text;
    public TextMeshProUGUI onGraphicText;
    public bool changing = false;

    public void ChangingBind(){
        changing = true;
        textWords = "Press Any Key...";
        text.text = textWords;
    }

    public void SetBind(string key){
        textWords = key.ToUpper();
        text.text = textWords;
        onGraphicText.text = textWords;
        ChangeBind(key);
        changing = false;
    } 

    public void ChangeBind(string k){
        Data.keys[ID] = (KeyCode)System.Enum.Parse(typeof(KeyCode),k.ToLower(), true);
    }
}

public static class Data{
    public static Dictionary<int, KeyCode> keys = new Dictionary<int, KeyCode>();

    public static bool set = false;

    public static void BindFind(string bindname){
        switch(bindname){
            case "Horizontal":
                Horizontal();
                break;
            case "Vertical":
                Vertical();
                break;
            case "SixShooter":
                SixShooter();
                break;
        }
    }

    public static void Horizontal(){
        set = true;
        keys[0] = KeyCode.A;
        keys[1] = KeyCode.J;
        keys[2] = KeyCode.S;
        keys[3] = KeyCode.K;
        keys[4] = KeyCode.D;
        keys[5] = KeyCode.L;
    }

    public static void Vertical(){
        set = true;
        keys[0] = KeyCode.Q;
        keys[1] = KeyCode.E;
        keys[2] = KeyCode.A;
        keys[3] = KeyCode.D;
        keys[4] = KeyCode.Z;
        keys[5] = KeyCode.C;
    }

    public static void SixShooter(){
        set = true;
        keys[0] = KeyCode.Alpha1;
        keys[1] = KeyCode.Alpha2;
        keys[2] = KeyCode.Q;
        keys[3] = KeyCode.W;
        keys[4] = KeyCode.A;
        keys[5] = KeyCode.S;
    }
}
