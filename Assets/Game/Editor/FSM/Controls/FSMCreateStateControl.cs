using UnityEngine;
using System.Collections;

public class FSMCreateStateControl
{

    public FSMEditor MyFSMEditor;

    public FSMCreateStateControl(FSMEditor MyFSMEditor)
    {
        this.MyFSMEditor = MyFSMEditor;
    }

    public void Render()
    {
       if( GUI.Button(new Rect(10, 20, 100, 50), "New State"))
           FSMCreateStateWindow.Init(MyFSMEditor);


    }
}
