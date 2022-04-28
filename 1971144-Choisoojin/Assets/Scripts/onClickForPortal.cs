using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onClickForPortal : MonoBehaviour
{
    public string gameSceneName;
    public GameObject character;  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        // Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //  float mouseX = position.x;
        //  float mouseY = position.y;
        float characterX = character.transform.position.x;
        float offsetX = 2;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (characterX < x + offsetX && characterX > x - offsetX)
            {
                 goToOtherScene();
            }
        }
    }

    void goToOtherScene()
    {
        switch (gameSceneName)
        {
            case "MonsterScene":
                {
                    SceneManager.LoadScene("MonsterScene");
                    break;
                }
            default:
                break;
        }
    }
}
