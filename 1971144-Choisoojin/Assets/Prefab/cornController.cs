using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cornController : MonoBehaviour
{
    GameObject character;
    public bool isBossScene = false;


    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("CollegeStudent Variant");
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;

        float characterX = character.transform.position.x;
        float offsetX = 0.7f;
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("charac X: " + characterX + ", x: " + x);
            Debug.Log("getKeyDown E");
            if (characterX < x + offsetX && characterX > x - offsetX)
            {
                Debug.Log("can't pick up");
                pickUp();
            }
        }

    }

    void pickUp()
    {
        monsterManager.cornNumber += 1;
        if (isBossScene == true)
        {
            BossControl.numOfRealCorn += 1;
        }
        Destroy(gameObject);
    }
   
}



