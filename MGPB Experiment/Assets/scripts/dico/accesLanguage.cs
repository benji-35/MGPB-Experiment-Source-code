using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(xmlReader))]
public class accesLanguage : MonoBehaviour
{
    xmlReader reader;
    List<Dictionary<string, string>> Languages = new List<Dictionary<string, string>>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        reader = GetComponent<xmlReader>();
        Languages = reader.getLanguages();
    }

    public string getWordLanguage(string key)
    {
        string res = null;
        int id = PlayerPrefs.GetInt("Language");
        if (id >= Languages.Count)
            return (res);
        Languages[id].TryGetValue(key, out res);
        return (res);
    }
}
