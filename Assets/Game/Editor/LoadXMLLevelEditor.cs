using System;
using System.Xml;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;


public class LoadXMLLevelEditor : EditorWindow
{
    
	string packName ="PACK-";
	string levelName = "LEVEL-";
   

    bool encriptar=false;
    
    [MenuItem ("Memeko/Load XML Level")]
    static void Init () {

        LoadXMLLevelEditor window = (LoadXMLLevelEditor)EditorWindow.GetWindow(typeof(LoadXMLLevelEditor));
		window.title="Memeko XML Level Loader";
    }


    private void LoadLevel(string PackName, string LevelName)
    {
        TextAsset xml = (TextAsset)Resources.Load(PackName);


        string xmlContent = "";
        if (xml!=null)
           xmlContent = xml.text;
        else
        {
            return;
        }

        XDocument doc = XDocument.Parse(xmlContent);
        XElement node = doc.XPathSelectElement("pack/level[@name='" + LevelName + "']");
        if (node != null)
        {
           
            var parent = new GameObject(LevelName);

            var elements = node.Elements();

            foreach (XElement element in elements)
            {
                //Debug.Log(element.Attribute("name").Value);
                string name = element.Attribute("name").Value;
                string position = element.Attribute("position").Value;
                string rotation = element.Attribute("rotation").Value;
                string scale = element.Attribute("scale").Value;
                string tempType = element.Attribute("type").Value;
                LevelObjectType objectType = (LevelObjectType)Enum.Parse(typeof(LevelObjectType), tempType, true);
                try
                {
                    GameObject obj = null;
                    var res = Resources.Load(name) as GameObject;
                    obj = (GameObject)Instantiate(res);

                    string[] p = position.Split(new char[] { ',' });
                    string[] r = rotation.Split(new char[] { ',' });
                    string[] s = scale.Split(new char[] { ',' });

                    obj.transform.position = new Vector3(float.Parse(p[0]), float.Parse(p[1]), float.Parse(p[2]));
                    obj.transform.localScale = new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
                    obj.transform.localEulerAngles = new Vector3(float.Parse(r[0]), float.Parse(r[1]), float.Parse(r[2]));

                    if (objectType == LevelObjectType.Text)
                    {
                        obj.GetComponent<TextMesh>().text = element.Attribute("text").Value;
                    }

                    if (objectType == LevelObjectType.Prefab || objectType == LevelObjectType.Text)
                        obj.transform.parent = parent.transform;

                }
                catch
                {
                    Debug.Log("Error al instanciar GameObject");
                }

            }


        }
        else
        {
            Debug.Log("No encontro");
        }

    }

    private void OnGUI()
    {
        GUILayout.Label("Level XML Loader", EditorStyles.boldLabel);
     
        packName = EditorGUILayout.TextField("Pack Name", packName);
        levelName = EditorGUILayout.TextField("Level Name", levelName);
       
        if (GUILayout.Button("Load Level Current Scene"))
        {
           LoadLevel(packName,levelName);
                    

        }
    }
}
