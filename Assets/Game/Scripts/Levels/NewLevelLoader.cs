using System;
using System.Xml.XPath;
using UnityEngine;
using System.Collections;
using System.Xml.Linq;

public class NewLevelLoader : MonoBehaviour 
{
    public static void LoadLevel(string PackName, string LevelName)
    {
        TextAsset xml = (TextAsset)Resources.Load(PackName);

       
        string xmlContent = "";
        if (Managers.Game.AreLevelFilesEncrypted)
        {
            xmlContent = DataManager.Decrypt(xml.text);
        }
        else
        {
            xmlContent = xml.text;
        }
       
        XDocument doc = XDocument.Parse(xmlContent);
        XElement node = doc.XPathSelectElement("pack/level[@name='"+ LevelName +"']");
        if (node != null)
        {
            Managers.Game.CurrentLevelXmlInfo.NextLevelName = node.Attribute("nextlevel").Value;
            Managers.Game.CurrentLevelXmlInfo.PackName = PackName;
            Managers.Game.CurrentLevelXmlInfo.CurrentLevelName = LevelName;
            Managers.Game.CurrentLevelXmlInfo.ChallengeTime = System.Convert.ToInt32(node.Attribute("challengetime").Value);
            Managers.Game.CurrentLevelXmlInfo.LevelNumber = System.Convert.ToInt32(node.Attribute("levelnumber").Value);
            if (node.Attribute("maxturns") != null)
            {
                Managers.Game.CurrentLevelXmlInfo.Maxturns = System.Convert.ToInt32(node.Attribute("maxturns").Value);
            }
            
            if (node.Attribute("cameraSize") != null)
            {
                Managers.Game.CurrentLevelXmlInfo.CameraSize =(float) System.Convert.ToDecimal(node.Attribute("cameraSize").Value);
            }
            if (node.Attribute("memekoForce") != null)
            {
                Managers.Game.CurrentLevelXmlInfo.MemekoForce = (float)System.Convert.ToDecimal(node.Attribute("memekoForce").Value);
            }
            

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
                    obj = (GameObject) Instantiate(res);
                    
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


}
