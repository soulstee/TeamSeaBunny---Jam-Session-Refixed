using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public static class Data{
    public static Dictionary<int, KeyCode> keys = new Dictionary<int, KeyCode>();

    public static void DefaultBinds(){
        keys[0] = KeyCode.A;
        keys[1] = KeyCode.S;
        keys[2] = KeyCode.D;
        keys[3] = KeyCode.J;
        keys[4] = KeyCode.K;
        keys[5] = KeyCode.L;
    }
}
