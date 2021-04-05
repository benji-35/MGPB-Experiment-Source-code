using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Text;

public class xmlReader : MonoBehaviour
{
    public TextAsset dictionnary;
    [SerializeField]
    Dropdown languageDrop;
    List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;
    // Start is called before the first frame update
    void Awake()
    {
        reader();
        updateLanguage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateLanguage()
    {
        PlayerPrefs.SetInt("Language", languageDrop.value);
    }

    void reader()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dictionnary.text);
        XmlNodeList languageList = xmlDoc.GetElementsByTagName("language");

        foreach(XmlNode lvalue in languageList)
        {
            XmlNodeList languageContent = lvalue.ChildNodes;
            obj = new Dictionary<string, string>();
            foreach(XmlNode value in languageContent)
                obj.Add(value.Name, value.InnerText);
            languages.Add(obj);
        }
    }

    public List<Dictionary<string, string>> getLanguages()
    {
        return (this.languages);
    }
}
