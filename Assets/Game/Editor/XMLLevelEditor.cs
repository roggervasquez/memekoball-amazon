using System.Xml;
using UnityEngine;
using UnityEditor;
using System.IO;


public class XMLLevelEditor : EditorWindow {
    
	string packName ="PACK-";
	string levelName = "LEVEL-";
    string levelNumber = "1";
	string nextlevel ="LEVEL-";
    private string challengeTime = "30";
    private string cameraSize = "9.3";
    private string memekoForce = "40";
    private string maxturns = "1";

    bool encriptar=false;
    
    [MenuItem ("Memeko/XML Level")]
    static void Init () {
        
        XMLLevelEditor window = (XMLLevelEditor)EditorWindow.GetWindow (typeof (XMLLevelEditor));
		window.title="Memeko";
    }

    private void SavePackAndLevelFirstTime(LevelObject []prefabs,bool encriptar)
    {
        var xml = GetPrefabsXML(prefabs);
        string filepath = Application.dataPath + @"/UI/LevelPacks/Resources/" + packName + ".xml";
        string xmlSalvar = string.Format(@"<pack name=""{0}""> {1} </pack>", packName, xml.ToString());
        if (encriptar)
        {
            File.WriteAllText(filepath, DataManager.Encrypt(xmlSalvar));
        }
        else
        {
            File.WriteAllText(filepath, xmlSalvar);

        }
    }

    private void SaveLevelData(LevelObject[] prefabs,string TextContent,bool encriptar)
    {
        
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(TextContent);

        // Locate the level if exists, then remove it so we can recreate it
        XmlElement node = (XmlElement)doc.SelectSingleNode("//level[@name='" + levelName + "']");
        if (node != null)
        {
            node.ParentNode.RemoveChild(node);
        }

      
        var xml = GetPrefabsXML(prefabs);


        XmlDocumentFragment xfrag = doc.CreateDocumentFragment();
        xfrag.InnerXml = @xml.ToString();
        doc.DocumentElement.AppendChild(xfrag);
        
        string filepath = Application.dataPath + @"/UI/LevelPacks/Resources/" + packName + ".xml";
        string xmlSalvar = doc.ToString();
        if (encriptar)
        {
            xmlSalvar = DataManager.Encrypt(xmlSalvar);
        }
      
        File.WriteAllText(filepath, xmlSalvar);
        doc.Save(filepath);
    }

    private StringWriter GetPrefabsXML(LevelObject[] prefabs)
    {
        var xml = new System.IO.StringWriter();
        xml.WriteLine(string.Format(@"<level name=""{0}"" nextlevel=""{1}"" challengetime=""{2}"" levelnumber=""{3}""  cameraSize=""{4}"" memekoForce=""{5}"" maxturns=""{6}""  >", levelName, nextlevel, challengeTime, levelNumber, cameraSize, memekoForce,maxturns));

        foreach (LevelObject prefab in prefabs)
        {
            string position = prefab.transform.position.x + "," + prefab.transform.position.y + "," +
                              prefab.transform.position.z;
            string rotation = prefab.transform.localEulerAngles.x + "," + prefab.transform.localEulerAngles.y + "," +
                              prefab.transform.localEulerAngles.z;
            string scale = prefab.transform.localScale.x + "," + prefab.transform.localScale.y + "," +
                           prefab.transform.localScale.z;

            if (prefab.type == LevelObjectType.Text)
            {
                string textmessage = prefab.GetComponent<TextMesh>().text;
                xml.WriteLine(string.Format(@"<prefab name=""{0}"" position=""{1}"" rotation=""{2}"" scale=""{3}"" type=""{4}"" text=""{5}"" />",
                prefab.name, position, rotation, scale, prefab.type.ToString(), textmessage));
            }
            else
            {
                xml.WriteLine(string.Format(@"<prefab name=""{0}"" position=""{1}"" rotation=""{2}"" scale=""{3}"" type=""{4}"" />",
                prefab.name, position, rotation, scale, prefab.type.ToString()));

            }
        }

        xml.WriteLine(string.Format(@"</level>"));
        return xml;
    }

    private void OnGUI()
    {
        GUILayout.Label("Level XML Generator", EditorStyles.boldLabel);
        packName = EditorGUILayout.TextField("Pack Name", packName);
        levelName = EditorGUILayout.TextField("Level Name", levelName);
        levelNumber = EditorGUILayout.TextField("Level Number", levelNumber);
        
         nextlevel = EditorGUILayout.TextField("Next Level Name", nextlevel);
        challengeTime = EditorGUILayout.TextField("Challenge Time", challengeTime);

        cameraSize = EditorGUILayout.TextField("Camera size", cameraSize);
        memekoForce = EditorGUILayout.TextField("MemekoBall Force", memekoForce);

        maxturns = EditorGUILayout.TextField("Maximum Turns ", maxturns);


        encriptar = EditorGUILayout.Toggle("Encriptar", encriptar);
        if (GUILayout.Button("Generate Level"))
        {
            LevelObject[] prefabs = GameObject.FindObjectsOfType(typeof (LevelObject)) as LevelObject[];
          
            TextAsset xmlFile = (TextAsset) Resources.Load(packName);

            if (xmlFile == null)
            {
                //For the first time is creating this pack
                SavePackAndLevelFirstTime(prefabs, encriptar);

            }
            else // Pack file already exists, save only the level inside the pack.xml file
            {
                if (encriptar)
                {
                    SaveLevelData(prefabs,DataManager.Decrypt(xmlFile.text), encriptar);
                }
                else
                {
                    SaveLevelData(prefabs, xmlFile.text, encriptar);
                }
                    

            }


        }
    }
}
