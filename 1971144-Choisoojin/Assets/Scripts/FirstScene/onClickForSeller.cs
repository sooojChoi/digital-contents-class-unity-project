using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onClickForSeller : MonoBehaviour
{
    public int firstMeetingSeller = 1;  //1 이면 상인을 처음 만나는 것, 0이면 이미 한 번 만난 것
    public GameObject noticeBoxImage;
    public GameObject storeObject;
    public Button nextButton;
    public Text sayingText;
    public GameObject firstCanvasObject;
    public Image firstCharacterImage;  // 스크립트의 캐릭터 이미지

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
                    showStore();
                }
            }
        }
    }
    public void showStore()
    {
        if (firstMeetingSeller == 1)
        {
            firstMeetingSeller = 0;
            noticeBoxImage.SetActive(false);
            sayingText.text = "아이템을 사고 싶다고요? 좋아요! 우리 상점엔 정말 좋은 아이템이 많아요. ";
            Sprite[] sprites = Resources.LoadAll<Sprite>("Character/PackForest01");
            firstCharacterImage.sprite = sprites[3];
            nextButton.onClick.AddListener(showStoreForNextButton);
            firstCanvasObject.SetActive(true);
        }
        else
        {
            storeObject.SetActive(true);
        }
    }
    void showStoreForNextButton()
    {
        firstCanvasObject.SetActive(false);
        storeObject.SetActive(true);
    }
}
