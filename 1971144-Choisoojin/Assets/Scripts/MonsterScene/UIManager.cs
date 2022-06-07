using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject InventoryObject;  // 인벤토리 이미지를 가지고 있는 게임 오브젝트
    public Button openInventoryButton;  // 인벤토리 여는 버튼
    public Image hpItemImage;  // 현재 설정된 회복 아이템 이미지
    public Image mpItemImage;  // 현재 설정된 공격 아이템 이미지
    public Text hpItemContent; // 현재 설정된 회복 아이템 개수를 나타내는 텍스트
    public Text mpItemContent;

    // Start is called before the first frame update
    void Start()
    {
        InventoryObject.SetActive(false);
        InventoryObject.SetActive(false);

        openInventoryButton.onClick.AddListener(openInventory);

        // 아이템 이미지 설정
        string mpItemName = Managers.Data.PlayerData["mpItem"].sort;
        string hpItemName = Managers.Data.PlayerData["hpItem"].sort;
        setItemInfo(mpItemName, "mp");
        setItemInfo(hpItemName, "hp");
    }

    // Update is called once per frame
    void Update()
    {
        string mpItemName = Managers.Data.PlayerData["mpItem"].sort;
        string hpItemName = Managers.Data.PlayerData["hpItem"].sort;

        setItemInfo(mpItemName, "mp");
        setItemInfo(hpItemName, "hp");

    }

    //인벤토리 여는 함수
    void openInventory()
    {
        InventoryObject.SetActive(true);
    }

    // 회복 아이템 이미지 설정하는 함수
    void setItemInfo(string imageName, string itemSort)
    {
        if(imageName == null || imageName == "")
        {
            if(itemSort == "hp")
            {
                // 설정된 아이템이 없으면 이미지를 투명하게 하고 return한다.
                hpItemImage.color = new Color(1, 1, 1, 0);
                hpItemContent.text = "";
            }
            else if(itemSort == "mp")
            {
                // 설정된 아이템이 없으면 이미지를 투명하게 하고 return한다.
                mpItemImage.color = new Color(1, 1, 1, 0);
                mpItemContent.text = "";
            }
      
            return;
        }

        Sprite image = Managers.Data.ItemSprite[imageName];
        if (itemSort == "hp")
        {
            hpItemImage.sprite = image;
            hpItemImage.color = new Color(1, 1, 1, 1);

            hpItemContent.text = Managers.Data.PlayerData[imageName].content.ToString();
        }
        else if (itemSort == "mp")
        {
            mpItemImage.sprite = image;
            mpItemImage.color = new Color(1, 1, 1, 1);

            mpItemContent.text = Managers.Data.PlayerData[imageName].content.ToString();
        }
  
    }
    
}
