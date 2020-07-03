using UnityEngine;
using System.Collections;

public class FSMDragStateControl  {

    public FSMStatesControl MyFSMStatesControl;

    public FSMEditorState DraggedState;
    public int DraggedTransitionIndex = -1;

    public FSMDragStateControl(FSMStatesControl MyFSMStatesControl)
    {
        this.MyFSMStatesControl = MyFSMStatesControl;
    }

  
    void ProcessDrag()
    {
        var editorStates = MyFSMStatesControl.MyFSMEditor.EditorStates;

        if (FSMEditorEvent.Instance.EditorEventType == FSMEditorEventType.MouseDown)
        {
            foreach (var editorState in editorStates)
            {
                int index = 0;
                foreach (var transition in editorState.State.Transitions)
                {
                    Rect transitionRect = editorState.GetTransitionRect(index);
                    transitionRect.y += MyFSMStatesControl.Position.y + editorState.State.Frame.y - MyFSMStatesControl.ScrollPos.y;
                    transitionRect.x += editorState.State.Frame.x - MyFSMStatesControl.ScrollPos.x;
                    if (transitionRect.Contains(FSMEditorEvent.Instance.MousePosition))
                    {
                        DraggedTransitionIndex = index;
                        DraggedState = editorState;
                    }
                    index++;
                }
            }
        }

    }

    public void Render()
    {
        ProcessDrag();
        
            
        if (DraggedState != null)
        {
           
            FSMEditorState hoverState = MyFSMStatesControl.FindStateUnderMouse();
            if (hoverState != null)
            {
                FSMLineRender.DrawTransition(DraggedState.GetEventRect(DraggedTransitionIndex, null), hoverState.GetHeaderRect(DraggedState));
            }
            else
            {
                Vector2 position = FSMEditorEvent.Instance.MousePosition;
                position.y += MyFSMStatesControl.ScrollPos.y - MyFSMStatesControl.Position.y;
                position.x += MyFSMStatesControl.ScrollPos.x;
                FSMLineRender.DrawTransition(DraggedState.GetEventRect(DraggedTransitionIndex, null), new Rect(position.x, position.y, 1, 1));
            }

            if (FSMEditorEvent.Instance.EditorEventType == FSMEditorEventType.MouseUp)
            {
                if(hoverState!=null && hoverState!=DraggedState)
                    DraggedState.State.Transitions[DraggedTransitionIndex].ToState = hoverState.State.StateName;
                DraggedState = null;
            }
        }
    }


}
