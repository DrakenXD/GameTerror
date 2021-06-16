using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    public KeyCode Foward { get; set;}
    public KeyCode Backward { get; set;}
    public KeyCode Right { get; set;}
    public KeyCode Left { get; set;}
    public KeyCode FlashLight { get; set;}


    private void Awake()
    {
        Foward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpkey", "W"));

        Backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardkey", "S"));

        Right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightkey", "D"));

        Left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftkey", "A"));

        FlashLight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("flashlightkey", "Mouse0"));

        
    }
}
