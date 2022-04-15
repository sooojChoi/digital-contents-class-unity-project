using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public Button LeftButton;
    public Button RightButton;
    public Button buttonForHPItem;  // 버튼을 누르면 회복 아이템만 보여줌.
    public Button buttonForMPItem;  // 버튼을 누르면 공격 아이템만 보여줌.
    public Button purchaseButton;  // '구매하기' 버튼
    int isHPMode; // 1면 화면에 hp아이템 보여주고, 0이면 mp아이템 보여줌.
    public Image selectedImage;  // 선택된 아이템을 보여주는 이미지
    public Text selectedItemName;  // 선택된 아이템 이름
    public Text selectedItemDes;  // 선택된 아이템 설명
    public Text selectedItemPrice;  // 선택된 아이템 가격

    List<Item> hpItemList;
    List<Item> mpItemList;
    int hpItemPage = 0;  // 화살표 버튼 활성화 설정을 위한 변수
    int mpItemPage = 0;
    int currentPage = 1;  // item 에서 화살표 버튼 클릭하면 넘어가는 페이지 변수
    int showedItemNum = 8;  // 한 화면에 보이는 아이템 개수

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

        string firstItemName = "apple";
        selectedImage.color = new Color(1, 1, 1, 1);
        selectedImage.sprite = Managers.Data.ItemSprite[firstItemName];
        selectedItemName.text = Managers.Data.ItemData[firstItemName].koname;
        selectedItemDes.text = "회복 +"+Managers.Data.ItemData[firstItemName].hp;
        selectedItemPrice.text = Managers.Data.ItemData[firstItemName].price.ToString();

        buttonForHPItem.onClick.AddListener(changeItemModeToHP);
        buttonForMPItem.onClick.AddListener(changeItemModeToMP);
        purchaseButton.onClick.AddListener(onClickForPurchase);

        InitItemButton();
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
            }
        }

    }

    void onClickForPurchase()  // 구매하는 버튼 누르면 호출되는 함수
    {
        // 구매하기 누르면 아이템을 구매할 수 있고, List에 추가된 다음
        // "나가기"버튼을 누를 때 json 파일에도 저장된다.
        // 플레이어인포의 money를 보고, 현재 소유한 money보다 비싼
        //아이템을 구매할 시 구매를 못하게 해야됨. (비활성화?)
        // 
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어의 money보다 아이템 가격이 높으면 구매하기 버튼 비활성화.
        // 아이템을 구매할 때 변경되는 변수값과 비교해야할 것. 
        // 실제 json의 money값은 바로바로 바뀌지 않을지도 몰라ㅏㅏ
        
    }
}
