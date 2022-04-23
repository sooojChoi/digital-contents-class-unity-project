using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class StoreManager : MonoBehaviour
{
    public Button LeftButton;
    public Button RightButton;
    public Button buttonForHPItem;  // 버튼을 누르면 회복 아이템만 보여줌.
    public Button buttonForMPItem;  // 버튼을 누르면 공격 아이템만 보여줌.
    public Button purchaseButton;  // '구매하기' 버튼
    public Button exitButton;  // '나가기' 버튼
    int isHPMode; // 1면 화면에 hp아이템 보여주고, 0이면 mp아이템 보여줌.
    public Image selectedImage;  // 선택된 아이템을 보여주는 이미지
    public Text selectedItemName;  // 선택된 아이템 이름
    public Text selectedItemDes;  // 선택된 아이템 설명
    public Text selectedItemPrice;  // 선택된 아이템 가격
    public Text moneyText;  // 사용자 돈 나타내는 text
    public Text moneyTextForStore;  // 상점에서만 보이는 사용자 money Text UI

    List<Item> hpItemList;
    List<Item> mpItemList;
    int hpItemPage = 0;  // 화살표 버튼 활성화 설정을 위한 변수
    int mpItemPage = 0;
    int currentPage = 1;  // item 에서 화살표 버튼 클릭하면 넘어가는 페이지 변수
    int showedItemNum = 8;  // 한 화면에 보이는 아이템 개수

    string firstItemName = "banana";  // 처음 시작할 때 선택되어있는 아이템 설정.
    string selectedItemEngName = "";  // 아이템이 선택되면 여기에 영어 이름(딕셔너리 키값)이 저장됨.

    void Init()
    {
        initList();

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
        RightButton.onClick.AddListener(rightBtnClicked);
        LeftButton.onClick.AddListener(leftBtnClicked);
    }
    void initList()
    {
        hpItemList = new List<Item>();
        mpItemList = new List<Item>();

        foreach (KeyValuePair<string, Item> item in Managers.Data.ItemData)
        {
            if (item.Value.hp == 0)
            {
                mpItemList.Add(item.Value);
            }else if(item.Value.mp == 0)
            {
                hpItemList.Add(item.Value);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isHPMode = 1;
        Init();
        itemSet((currentPage - 1) * showedItemNum + 1);

        selectedImage.color = new Color(1, 1, 1, 1);
        selectedImage.sprite = Managers.Data.ItemSprite[firstItemName];
        selectedItemName.text = Managers.Data.ItemData[firstItemName].koname;
        selectedItemDes.text = "회복 +"+Managers.Data.ItemData[firstItemName].hp;
        selectedItemPrice.text = Managers.Data.ItemData[firstItemName].price.ToString();

        int money = Managers.Data.PlayerData["money"].content;
        if (Managers.Data.ItemData[firstItemName].price > money)
        {
            purchaseButton.interactable = false;
        }
        else
        {
            purchaseButton.interactable = true;
        }
        selectedItemEngName = firstItemName;

        buttonForHPItem.onClick.AddListener(changeItemModeToHP);
        buttonForMPItem.onClick.AddListener(changeItemModeToMP);
        purchaseButton.onClick.AddListener(onClickForPurchase);

        InitItemButton();
        exitButton.onClick.AddListener(exitStore);

        moneyTextForStore.text = moneyText.text;
    }
    void exitStore()
    {
        selectedImage.color = new Color(1, 1, 1, 1);
        selectedImage.sprite = Managers.Data.ItemSprite[firstItemName];
        selectedItemName.text = Managers.Data.ItemData[firstItemName].koname;
        selectedItemDes.text = "회복 +" + Managers.Data.ItemData[firstItemName].hp;
        selectedItemPrice.text = Managers.Data.ItemData[firstItemName].price.ToString();

        int money = Managers.Data.PlayerData["money"].content;
        if (Managers.Data.ItemData[firstItemName].price > money)
        {
            purchaseButton.interactable = false;
        }
        else
        {
            purchaseButton.interactable = true;
        }

        selectedItemEngName = firstItemName;
    }
    void itemSet(int itemCnt)
    {
        int i = 1;
        int num = 0;
        int realITemNum = 0;
        List<Item> list = new List<Item>();
        if(isHPMode == 0)
        {
            list = mpItemList;
        }
        else
        {
            list = hpItemList;
        }
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
                break;
            }
            Image itemImage = GameObject.Find($"Item{i++}").GetComponent<Image>();
            itemImage.color = new Color(1, 1, 1, 1);
            itemImage.sprite = Managers.Data.ItemSprite[item.name];
        }

        for (i = realITemNum+1; i <= showedItemNum; i++)
        {
            Image image = GameObject.Find($"Item{i}").GetComponent<Image>();
            image.color = new Color(1, 1, 1, 0);
        }
    }
    void rightBtnClicked()
    {
        LeftButton.interactable = true;
        currentPage++;
        if(isHPMode == 0)
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

    void InitItemButton()
    {
        for(int i = 1; i <= 8; i++)
        {
            Button button = GameObject.Find($"item{i}").GetComponent<Button>();
            Image image = GameObject.Find($"Item{i}").GetComponent<Image>();
            button.onClick.AddListener(delegate { onClickForItem(image); });
        }
    }
    //아이템을 클릭할 때마다 아래에 선택된 아이템의 이미지와 정보를 나타냄.
    void onClickForItem(Image image)
    {
        selectedImage.sprite = image.sprite;
        foreach (KeyValuePair<string, Item> item in Managers.Data.ItemData)
        {
            if (item.Value.name == image.sprite.name)
            {
                selectedItemName.text = item.Value.koname;
                selectedItemPrice.text = item.Value.price.ToString();
                if (isHPMode == 1)
                {
                    selectedItemDes.text =  "회복 +" + item.Value.hp;
                }else if(isHPMode == 0)
                {
                    selectedItemDes.text = "공격력 +" + item.Value.mp;
                }

                int money = Managers.Data.PlayerData["money"].content;
                if(item.Value.price > money)
                {
                    purchaseButton.interactable = false;
                }
                else
                {
                    purchaseButton.interactable = true;
                }

                // onClickForPurchase에서 현재 선택된 아이템의 키값(영어이름)을 알기 위해서..
                selectedItemEngName = item.Value.name;

                break;
            }
        }

    }

    void onClickForPurchase()  // 구매하는 버튼 누르면 호출되는 함수
    {
        // 이미 playerData에 있는 아이템인지 확인하고, 없으면 추가, 있으면 content수 증가.
        if (Managers.Data.PlayerData.ContainsKey(selectedItemEngName))
        {
            // 이미 구매한 적이 있는 경우
            Managers.Data.PlayerData[selectedItemEngName].content += 1;   // 아이템 수량을 늘리고
            Managers.Data.PlayerData["money"].content -= Managers.Data.ItemData[selectedItemEngName].price;  // 돈을 빼준다.
        }
        else
        {
            // 처음 구매하는 경우
            playerData itemForPurchase = new playerData();
            itemForPurchase.name = selectedItemEngName;
            itemForPurchase.content = 1;
            if (Managers.Data.ItemData[selectedItemEngName].hp == 0)  // 회복 아이템인지 공격 아이템인지..
            {
                itemForPurchase.sort = "mp";
            }
            else
            {
                itemForPurchase.sort = "hp";
            }

            Managers.Data.PlayerData.Add(selectedItemEngName, itemForPurchase);
            Managers.Data.PlayerData["money"].content -= Managers.Data.ItemData[selectedItemEngName].price;
        }
        // 상단 좌측과 상점 쪽에 나타나는 내 돈 액수를 수정해준다.
        moneyText.text = Managers.Data.PlayerData["money"].content.ToString();
        moneyTextForStore.text = Managers.Data.PlayerData["money"].content.ToString();
        // 만약 더 이상 구매할 돈이 없다면 '구매하기'버튼을 비활성화 시킨다. 
        if (Managers.Data.PlayerData["money"].content< Managers.Data.ItemData[selectedItemEngName].price)
        {
            purchaseButton.interactable = false;
        }
        // json 파일에 변경사항을 저장해준다. 
        playerInfoSave("/Resources/Data/playerData.json");
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

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
