using System;
using UnityEngine;
using UnityEditor;

public class FSMLineRender
{
    public static Texture2D aaLineTex = null;
    public static Texture2D lineTex = null;


    public static void DrawTransition(Rect wr, Rect wr2)
    {
        // CurveFromTo(wr2, wr3, new Color(0.7f,0.2f,0.3f));
        FSMLineRender.BezierLine(
            new Vector2(wr.x + wr.width, wr.y + wr.height / 2),
            new Vector2(wr.x + wr.width, wr.y + wr.height / 2),
            //new Vector2(wr.x + wr.width + Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr.y + wr.height / 2),
            new Vector2(wr2.x, wr2.y + wr2.height / 2),
             new Vector2(wr2.x, wr2.y + wr2.height / 2),
            // new Vector2(wr2.x - Mathf.Abs(wr2.x - (wr.x + wr.width)) / 2, wr2.y + wr2.height / 2), 
            new Color(0.7f, 0.2f, 0.3f), 2, true, 1
        );
    }

    public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width, bool antiAlias)
    {
       
        Handles.color = color;
        Handles.DrawLine(pointA,pointB);

    }

    public static void BezierLine(Vector2 start, Vector2 startTangent, Vector2 end, Vector2 endTangent, Color color, float width, bool antiAlias, int segments)
    {
        Vector2 lastV = CubeBezier(start, startTangent, end, endTangent, 0);
        for (int i = 1; i <= segments; ++i)
        {
            Vector2 v = CubeBezier(start, startTangent, end, endTangent, i/(float)segments);
            FSMLineRender.DrawLine(
                lastV,
                v,
                color, width, antiAlias);
            lastV = v;
        }
    }

    private static Vector2 CubeBezier(Vector2 s, Vector2 st, Vector2 e, Vector2 et, float t){
        float rt = 1-t;
        float rtt = rt * t;
        return rt*rt*rt * s + 3 * rt * rtt * st + 3 * rtt * t * et + t*t*t* e;
    }

    private static Matrix4x4 TranslationMatrix(Vector3 v)
    {
        return Matrix4x4.TRS(v,Quaternion.identity,Vector3.one);
    }
}
