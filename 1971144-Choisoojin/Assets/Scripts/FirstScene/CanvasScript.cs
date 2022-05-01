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
    public GameObject storeObject;  // 가게 이미지를 가지고 있는 게임 오브젝트
    public GameObject InventoryObject;  // 인벤토리 이미지를 가지고 있는 게임 오브젝트
    public Button exitButton;
    public Button openInventoryButton;  // 인벤토리 여는 버튼
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
        InventoryObject.SetActive(false);

        storeObject.SetActive(false);
        InventoryObject.SetActive(false);
        exitButton.onClick.AddListener(exitStore);
        openInventoryButton.onClick.AddListener(openInventory);
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
        string[] tempArray = new string[9];
        tempArray[0] = "게임을 시작하지! 친구, 전설의 옥수수깡에 대해 혹시 아는가?";
        tempArray[1] = "뭐? 이건 그냥 과자가 아냐! 고소하고 매콤한데 짭짤하기까지 하다고!";
        tempArray[2] = "크흠.. 너무 흥분했군. 하지만 내가 먹어본 것 중 최고였는걸.";
        tempArray[3] = "하지만 마지막으로 본지가 언제인지 기억도 안나.";
        tempArray[4] = "몬스터들이 모두 독차지 했으니까 말이야! 흑흑";
        tempArray[5] = "뭐? 너가 몬스터를 모두 무찌르고 가져다 주겠다고?";
        tempArray[6] = "그럼 가기전에 상점에 먼저 들러.";
        tempArray[7] = "몬스터는 만만한 녀석들이 아니야! 회복 아이템이 꼭 필요하다고!";
        tempArray[8] = "그럼 부탁해 친구! 전설의 옥수수깡을 먹을 수 있는 그 날까지!";

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

    //인벤토리 여는 함수
    void openInventory()
    {
         InventoryObject.SetActive(true);
    }

}
