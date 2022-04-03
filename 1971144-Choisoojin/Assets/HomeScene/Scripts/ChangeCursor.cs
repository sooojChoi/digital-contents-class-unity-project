using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorImg;
    private Vector2 cursorSpot;

    // Start is called before the first frame update
    void Start()
    {
        cursorSpot.x = cursorImg.width / 2;
        cursorSpot.y = cursorImg.height / 2;
        Cursor.SetCursor(cursorImg, cursorSpot, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
