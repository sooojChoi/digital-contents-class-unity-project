using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject InventoryObject;  // 인벤토리 이미지를 가지고 있는 게임 오브젝트
    public Button openInventoryButton;  // 인벤토리 여는 버튼


    // Start is called before the first frame update
    void Start()
    {
        InventoryObject.SetActive(false);
        InventoryObject.SetActive(false);

        openInventoryButton.onClick.AddListener(openInventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //인벤토리 여는 함수
    void openInventory()
    {
        InventoryObject.SetActive(true);
    }
}
