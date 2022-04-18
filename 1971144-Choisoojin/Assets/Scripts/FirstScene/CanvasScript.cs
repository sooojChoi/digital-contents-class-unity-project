using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public Button nextButton;
    public Text sayingText;
    public GameObject firstCanvasObject;
    public Image firstCharacterImage;  // 스크립트의 캐릭터 이미지
    string[] firstChracterSaying;
    int textNum = 0;
    public static int sellerScriptEnd = 0;
    public Text moneyText;  // 사용자 돈 나타내는 text

    public GameObject noticeBoxImage;
    public GameObject storeObject;
    public Button exitButton;
  //  public Button toTalkWithSeller;

    public GameObject sellerBox;
   // public int firstMeetingSeller = 1;  //1 이면 상인을 처음 만나는 것, 0이면 이미 한 번 만난 것
    // 상인을 처음 만날 때만 대화 스크립트가 나오게 하기 위한 것.

    // Start is called before the first frame update
    void Start()
    {
        noticeBoxImage.SetActive(false);
        firstChracterSaying = InitTextArray();
        sayingText.text = firstChracterSaying[textNum];
        nextButton.onClick.AddListener(ShowNextText);

        storeObject.SetActive(false);
        exitButton.onClick.AddListener(exitStore);
        //  toTalkWithSeller.onClick.AddListener(showStore);

        foreach (KeyValuePair<string, playerData> player in Managers.Data.PlayerData)
        {
            Debug.Log("player money text is update.");
            if (player.Value.name == "money")
            {
                moneyText.text = player.Value.content.ToString();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraMoving.sellerForCanvasScript == 1)
        {
            CameraMoving.sellerForCanvasScript = 0;
            noticeBoxImage.SetActive(true);
        }
        
    }

    void exitStore()
    {
        CameraMoving.meetingSellerNow = 0;
        sellerScriptEnd = 1;
        storeObject.SetActive(false);
       
    }
    string[] InitTextArray()
    {
        string[] tempArray = new string[5];
        tempArray[0] = "모험가님, 우리 마을에 오신 것을 환영합니다.";
        tempArray[1] = "소문을 듣고 오셨군요. 보스 몬스터를 잡으면 엄청난 보물을 받을 수 있다는 소문 말이에요!";
        tempArray[2] = "몬스터를 잡으려면 튼튼한 체력과 강력한 무기가 필요하답니다.";
        tempArray[3] = "걱정마세요, 체력 아이템과 무기는 상점에서 팔고 있으니까요.  ";
        tempArray[4] = "먼저 상인을 찾아서 말을 걸어보세요.";

        return tempArray;
    }
    void ShowNextText()
    {
        textNum++;
        if(textNum< firstChracterSaying.Length)
        {
            sayingText.text = firstChracterSaying[textNum];
        }
        else
        {
            firstCanvasObject.SetActive(false);
        }
    }

}
