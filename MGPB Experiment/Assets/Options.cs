using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    
    public GameObject Menu;
    private bool menueVisible = false;
    
    public GameObject Option;
    bool optionsVisible = false;
    
    //public GameObject OptionGraphics;
    //bool optionsGraphicsVisible = false;
    //
    //public GameObject OptionKeys;
    //bool optionsKeysVisible = false;
    

    public Dropdown resolution;

    private void Start()
    {
        QualitySettings.SetQualityLevel(4);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
            //if (OptionGraphics.activeSelf || OptionKeys.activeSelf)
            //{
            //    //OptionGraphics.SetActive(false);
            //    //OptionKeys.SetActive(false);
            //    Option.SetActive(true); 
            //}
            //else
            //{
                //optionsVisible = !optionsVisible;
                //Option.SetActive(optionsVisible);
            //}
        //}
    }
    
    public void DisplayOptions()
    {
        Option.SetActive(true);
        Menu.SetActive(false);
    }
    
    // public void DisplayGrapics()
    // {
    //     Option.SetActive(true);
    // }
    // 
    // public void DisplayGrapics()
    // {
    //     if (Option.activeSelf)
    //     {
    //         optionsGraphicsVisible = !optionsGraphicsVisible;
    //         OptionGraphics.SetActive(true);
    //         Option.SetActive(false);
    //     }
    // }
    // 
    // public void DisplayKeys()
    // {
    //     if (Option.activeSelf)
    //     {
    //         optionsKeysVisible = !optionsKeysVisible;
    //         OptionKeys.SetActive(true);
    //         Option.SetActive(false);
    //     }
    // }
    // 
    public void SetResolution(int resolution)
    {
        QualitySettings.SetQualityLevel(resolution);
    }
    // 
    public void SetFullscreen(bool isFullscrenn)
    {
        Screen.fullScreen = isFullscrenn;
    }
}
