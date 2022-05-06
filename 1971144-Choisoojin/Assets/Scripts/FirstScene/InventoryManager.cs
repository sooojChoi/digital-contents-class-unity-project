using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    public Button LeftButton;
    public Button RightButton;
    public Button buttonForHPItem;  // 버튼을 누르면 회복 아이템만 보여줌.
    public Button buttonForMPItem;  // 버튼을 누르면 공격 아이템만 보여줌.
    public Button exitButton;  // '나가기' 버튼
    public Button openInventoryButton;  // 인벤토리 여는 버튼
    int isHPMode; // 1면 화면에 hp아이템 보여주고, 0이면 mp아이템 보여줌.

    public Image hpItemImage;  // hp 아이템으로 설정됨.
    public Image mpItemImage;  // mp 아이템으로 설정됨.
    public Text hpTextName;  // hp 아이템으로 설정됨 (아이템 이름)
    public Text mpTextName;  // mp 아이템으로 설정됨 (아이템 이름)
    public Text hpTextPower;  // hp 아이템으로 설정됨 (아이템 강도)
    public Text mpTextPower;  // mp 아이템으로 설정됨 (아이템 강도)

    List<Item> hpItemList;
    List<Item> mpItemList;
    int hpItemPage = 0;  // 화살표 버튼 활성화 설정을 위한 변수
    int mpItemPage = 0;
    int currentPage = 1;  // item 에서 화살표 버튼 클릭하면 넘어가는 페이지 변수
    int showedItemNum = 4;  // 한 화면에 보이는 아이템 개수

    void Init()
    {
        initList();

        Debug.Log("회복 아이템 개수:" + hpItemList.Count);
        hpItemPage = hpItemList.Count / showedItemNum;
        if (hpItemList.Count % showedItemNum != 0)
        {
            hpItemPage++;
        }
        mpItemPage = mpItemList.Count / showedItemNum;
        if (mpItemList.Count % showedItemNum != 0)
        {
            mpItemPage++;
        }

        LeftButton.interactable = false;
        if (hpItemList.Count > showedItemNum)
        {
            RightButton.interactable = true;
        }
        else
        {
            RightButton.interactable = false;
        }
        RightButton.onClick.RemoveAllListeners();
        LeftButton.onClick.RemoveAllListeners();

        RightButton.onClick.AddListener(rightBtnClicked);
        LeftButton.onClick.AddListener(leftBtnClicked);
    }
    void initList()
    {
        hpItemList = new List<Item>();
        mpItemList = new List<Item>();

        foreach (KeyValuePair<string, playerData> item in Managers.Data.PlayerData)
        {
            if (item.Value.sort == "mp")
            {
                mpItemList.Add(Managers.Data.ItemData[item.Value.name]);
            }
            else if (item.Value.sort == "hp")
            {
                hpItemList.Add(Managers.Data.ItemData[item.Value.name]);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isHPMode = 1;
        Init();
        itemSet((currentPage - 1) * showedItemNum + 1);

        buttonForHPItem.onClick.AddListener(changeItemModeToHP);
        buttonForMPItem.onClick.AddListener(changeItemModeToMP);

        InitItemButton();
        exitButton.onClick.AddListener(exitStore);
        openInventoryButton.onClick.RemoveAllListeners();
        openInventoryButton.onClick.AddListener(openInventory);

        // 설정된 회복, 공격 아이템이 있으면 인벤토리에 나타낸다.
        if(Managers.Data.PlayerData["hpItem"].sort != "")
        {
            hpItemImage.color = new Color(1, 1, 1, 1);
            string hpItemName = Managers.Data.PlayerData["hpItem"].sort;
            hpItemImage.sprite = Managers.Data.ItemSprite[hpItemName];
            hpTextName.text = Managers.Data.ItemData[hpItemName].koname;
            hpTextPower.text = "회복 +"+Managers.Data.ItemData[hpItemName].hp.ToString();
        }
        if (Managers.Data.PlayerData["mpItem"].sort != "")
        {
            mpItemImage.color = new Color(1, 1, 1, 1);
            string mpItemName = Managers.Data.PlayerData["mpItem"].sort;
            mpItemImage.sprite = Managers.Data.ItemSprite[mpItemName];
            mpTextName.text = Managers.Data.ItemData[mpItemName].koname;
            mpTextPower.text = "회복 +" + Managers.Data.ItemData[mpItemName].mp.ToString();
        }
    }
    void exitStore()
    {
        changeItemModeToHP();
        gameObject.SetActive(false);
    }
    void InitItemButton()
    {
        for (int i = 1; i <= 4; i++)
        {
            Button button = GameObject.Find($"invItem{i}").GetComponent<Button>();
            Image image = GameObject.Find($"iv_item{i}").GetComponent<Image>();
            button.onClick.AddListener(delegate { onClickForItem(image); });
        }
    }

    //아이템을 클릭할 때마다 아래에 선택된 아이템의 이미지와 정보를 나타냄.
    void onClickForItem(Image image)
    {
        if(image.sprite == null)
        {
            return;
        }
        if(isHPMode == 1)  //hp 아이템을 클릭했을 경우
        {
            hpItemImage.color = new Color(1, 1, 1, 1);
            hpItemImage.sprite = image.sprite;
            foreach (KeyValuePair<string, Item> item in Managers.Data.ItemData)
            {
                if (item.Value.name == image.sprite.name)
                {
                    // 화면에 선택된 아이템 정보를 나타낸다.
                    hpTextName.text = item.Value.koname;  
                    hpTextPower.text = "회복 +" + item.Value.hp;

                    // 사용자의 hpItem 정보를 선택된 아이템으로 수정한다.
                    Managers.Data.PlayerData["hpItem"].sort = item.Value.name;

                    break;
                }
            }
        }
        else  // mp 아이템을 선택했을 경우
        {
            mpItemImage.color = new Color(1, 1, 1, 1);
            mpItemImage.sprite = image.sprite;
            foreach (KeyValuePair<string, Item> item in Managers.Data.ItemData)
            {
                if (item.Value.name == image.sprite.name)
                {
                    // 화면에 선택된 아이템 정보를 나타낸다.
                    mpTextName.text = item.Value.koname;
                    mpTextPower.text = "공격력 +" + item.Value.mp;

                    // 사용자의 hpItem 정보를 선택된 아이템으로 수정한다.
                    Managers.Data.PlayerData["mpItem"].sort = item.Value.name;

                    break;
                }
            }
        }
        // json 파일에 변경사항을 저장해준다. 
        playerInfoSave("/Resources/Data/playerData.json");

    }
    void itemSet(int itemCnt)
    {
        int i = 1;
        int num = 0;
        int realITemNum = 0;
        List<Item> list = new List<Item>();
        if (isHPMode == 0)
        {
            list = mpItemList;
        }
        else
        {
            list = hpItemList;
        }
        Debug.Log("회복 아이템 개수(itemSet): " + hpItemList.Count);
       
        foreach (Item item in list)
        {
            if (isHPMode == 1)
            {
                if (item.hp == 0)
                {
                    break;
                }
            }
            else if (isHPMode == 0)
            {
                if (item.mp == 0)
                {
                    break;
                }
            }

            Debug.Log("itemCnt: " + itemCnt);
            Debug.Log("currentPage : " + currentPage+ ", showedItemNum: "+ showedItemNum);
            num++;
            if (num < itemCnt)
            {
                continue;
            }
            else
            {
                realITemNum++;
            }
            if (i > showedItemNum)
            {
                Debug.Log("for문 끝남. i: "+i);
                break;
            }
            Image itemImage = GameObject.Find($"iv_item{i}").GetComponent<Image>();
            itemImage.color = new Color(1, 1, 1, 1);
            itemImage.sprite = Managers.Data.ItemSprite[item.name];
            Text itemNameText = GameObject.Find($"iv_text{i}").GetComponent<Text>();
            itemNameText.text = Managers.Data.PlayerData[item.name].content.ToString();
            Text itemInfo = GameObject.Find($"iv_info{i++}").GetComponent<Text>();
            if(Managers.Data.PlayerData[item.name].sort == "hp")
            {
                itemInfo.text = item.koname + "\nHP: " + Managers.Data.ItemData[item.name].hp.ToString();
            }else if(Managers.Data.PlayerData[item.name].sort == "mp")
            {
                itemInfo.text = item.koname+"\nMP: "+Managers.Data.ItemData[item.name].mp.ToString();
            }
            
        }

        for (i = realITemNum + 1; i <= showedItemNum; i++)
        {
            Debug.Log("아이템 없다: " + realITemNum);
            Image image = GameObject.Find($"iv_item{i}").GetComponent<Image>();
            image.color = new Color(1, 1, 1, 0);
            Text itemNameText = GameObject.Find($"iv_text{i}").GetComponent<Text>();
            itemNameText.text = "";
            Text itemInfo = GameObject.Find($"iv_info{i}").GetComponent<Text>();
            itemInfo.text = "";
        }
    }
    void rightBtnClicked()
    {
        LeftButton.interactable = true;
        currentPage++;
        if (isHPMode == 0)
        {
            if (currentPage == mpItemPage)
            {
                RightButton.interactable = false;
            }
            itemSet((currentPage - 1) * showedItemNum + 1);
        }
        else
        {
            if (currentPage == hpItemPage)
            {
                RightButton.interactable = false;
            }
            itemSet((currentPage - 1) * showedItemNum + 1);
        }

    }
    void leftBtnClicked()
    {
        RightButton.interactable = true;
        currentPage--;
        if (currentPage == 1)
        {
            LeftButton.interactable = false;
        }
        itemSet((currentPage - 1) * showedItemNum + 1);
    }
    void changeItemModeToHP()
    {
        isHPMode = 1;
        currentPage = 1;
        itemSet((currentPage - 1) * showedItemNum + 1);
        LeftButton.interactable = false;
        if (hpItemList.Count > showedItemNum)
        {
            RightButton.interactable = true;
        }
        else
        {
            RightButton.interactable = false;
        }
    }
    void changeItemModeToMP()
    {
        isHPMode = 0;
        currentPage = 1;
        itemSet((currentPage - 1) * showedItemNum + 1);
        LeftButton.interactable = false;
        if (mpItemList.Count > showedItemNum)
        {
            RightButton.interactable = true;
        }
        else
        {
            RightButton.interactable = false;
        }
    }

    //  playerData.json 파일 저장하는 함수
    void playerInfoSave(string path)
    {
        List<playerData> playerInfo = new List<playerData>();
        playerDataInfo playerData = new playerDataInfo();

        foreach (KeyValuePair<string, playerData> player in Managers.Data.PlayerData)
        {
            playerInfo.Add(player.Value);
        }
        playerData.playerInfo = playerInfo;

        string jsonString = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + path, jsonString);
    }

    //인벤토리 여는 함수
    void openInventory()
    {
        Debug.Log("인벤토리 오픈됨");
        gameObject.SetActive(true);
        currentPage = 1;
        isHPMode = 1;
        Init();
        itemSet((currentPage - 1) * showedItemNum + 1);
        // gameObject.SetActive(true);
    }
}
