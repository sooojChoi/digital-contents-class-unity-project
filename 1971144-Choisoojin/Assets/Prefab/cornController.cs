using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cornController : MonoBehaviour
{
   // private bool pickUpAllowed;
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;

        float characterX = character.transform.position.x;
        float offsetX = 0.5f;
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
        Destroy(gameObject);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("corn trigger enter.");
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        pickUpAllowed = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("corn trigger exit.");
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        pickUpAllowed = false;
    //    }
    //}

}
