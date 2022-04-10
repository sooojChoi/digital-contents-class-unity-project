using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickForSeller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mouseX = position.x;
        float mouseY = position.y;
        float offsetX = 2;
        float offsetY = 1.5f;
        if (Input.GetMouseButtonDown(0))
        {
            if(mouseX<x+ offsetX && mouseX > x - offsetX)
            {
                if(mouseY<y+ offsetY && mouseY > y - offsetY)
                {
                    Debug.Log("물체 범위가 눌림");
                }
            }
        }
    }
    public void showStore()
    {
        Debug.Log("showStore button is clicked");
    }
}
