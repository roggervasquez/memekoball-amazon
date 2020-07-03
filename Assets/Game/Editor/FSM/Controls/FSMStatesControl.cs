using System.Runtime.InteropServices;

using UnityEditor;
using UnityEngine;
using System.Collections;

public class FSMStatesControl{

    public FSMEditor MyFSMEditor;
    public Vector2 ScrollPos = Vector2.zero;
    public Vector2 Position = new Vector2(0,100);

    private FSMDragStateControl FSMDragStateControl;


    public FSMStatesControl(FSMEditor MyFSMEditor)
    {
        this.MyFSMEditor = MyFSMEditor;
        FSMDragStateControl = new FSMDragStateControl(this);
    }


    public FSMEditorState FindStateUnderMouse()
    {
        foreach (var editorState in MyFSMEditor.EditorStates)
        {
            var rect = editorState.State.Frame;
            rect.y +=  Position.y-ScrollPos.y;
            rect.x -= ScrollPos.x;
            if (rect.Contains(FSMEditorEvent.Instance.MousePosition))
            {
                Debug.Log(editorState.State.StateName);
                return editorState;
            }
        }
        return null;
    }

   

    public Vector2 CalculateViewport()
    {
        Vector2 viewport = Vector2.zero;
        foreach (var editorState in MyFSMEditor.EditorStates)
        {
            if (editorState.State.Frame.x > viewport.x)
                viewport.x = editorState.State.Frame.x;
            if (editorState.State.Frame.y > viewport.y)
                viewport.y = editorState.State.Frame.y;
        }

        return viewport;
    }

    public void Render()
    {
        Vector2 viewport = CalculateViewport();

        GUI.Box(new Rect(0, Position.y, MyFSMEditor.position.width, this.MyFSMEditor.position.height - Position.y),"");
        ScrollPos = GUI.BeginScrollView(new Rect(0, Position.y, MyFSMEditor.position.width, this.MyFSMEditor.position.height - Position.y), 
                                        ScrollPos, new Rect(0, 0, viewport.x + Position.y+100, viewport.y + Position.y+100));
        
        MyFSMEditor.BeginWindows();
        foreach (FSMEditorState editorState in MyFSMEditor.EditorStates)
            editorState.Render();
        
        MyFSMEditor.EndWindows();

     

        foreach (var fromEditorState in MyFSMEditor.EditorStates)
        {
            int i = 0;
            foreach (var transition in fromEditorState.State.Transitions)
            {
                if (transition.ToState != "")
                {
                    if (MyFSMEditor.EditorStatesIndex.ContainsKey(transition.ToState))
                    {
                        var toEditorState = MyFSMEditor.EditorStatesIndex[transition.ToState];
                        FSMLineRender.DrawTransition(fromEditorState.GetEventRect(i, toEditorState), toEditorState.GetHeaderRect(fromEditorState));
                    }
                }
                i++;
            }
        }

        FSMDragStateControl.Render();
        GUI.EndScrollView();
    }


}
