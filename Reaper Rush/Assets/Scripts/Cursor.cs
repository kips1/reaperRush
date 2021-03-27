using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{

    //public Texture2D cursorTexture;
    //public CursorMode cursorMode = CursorMode.Auto;
    //public Vector2 hotSpot = Vector2.zero;

    public Texture2D cursorArrow;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    private static void SetCursor(Texture2D cursorArrow, Vector2 zero, CursorMode forceSoftware)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
}
