using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class autoLangText : MonoBehaviour
{
    [SerializeField]
    private bool autoUpdate;
    [SerializeField]
    private string key;
    Text txt;
    accesLanguage acces;
    private void Start()
    {
        txt = GetComponent<Text>();
        acces = GameObject.Find("languageManager").GetComponent<accesLanguage>();
        updateText();
    }

    void Update()
    {
        if (autoUpdate)
            updateText();
    }

    public void updateText()
    {
        if (acces == null)
            return;
        txt.text = acces.getWordLanguage(key);
    }
}
