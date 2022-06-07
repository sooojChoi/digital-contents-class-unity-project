using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onClickForNeoguri : MonoBehaviour
{
    public GameObject neoguriScript;
    public Text sayingText;
    string[] firstChracterSaying;
    int textNum = 0;
    public Button nextButton;
    public Image firstCharacterImage;  // 스크립트의 캐릭터 이미지
    public Image secondCharacterImage;  // midScene에서만 보이는 두 번째 너구리 이미지
    public GameObject portalObject;  // 포탈 오브젝트(너구리와 대화가 끝나면 보인다)

    public bool midScene = false;

    // Start is called before the first frame update
    void Start()
    {
        // 처음에는 포탈이 보이지 않게 한다. (너구리와 대화가 끝나면 포탈이 보임)
        if (midScene == true)
        {
            neoguriScript.SetActive(false);
        }
        portalObject.SetActive(false);
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
            if (mouseX < x + offsetX && mouseX > x - offsetX)
            {
                if (mouseY < y + offsetY && mouseY > y - offsetY)
                {
                    showScript();
                }
            }
        }
    }

    void showScript()
    {

        if (midScene == false)
        {
            firstChracterSaying = InitTextArray();
        }
        else
        {
            firstChracterSaying = InitTextArrayForMidScene();
        }
           
        sayingText.text = firstChracterSaying[textNum];
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(ShowNextText);

        Sprite[] sprites = Resources.LoadAll<Sprite>("Character/Neoguri_Character");
        firstCharacterImage.sprite = sprites[0];

        neoguriScript.SetActive(true);
    }

    string[] InitTextArray()
    {
        string[] tempArray = new string[4];
        tempArray[0] = "친구! 상점에서 아이템은 잘 샀는가?";
        tempArray[1] = "좋았어! 그럼 이제 몬스터 마을로 가서 전설의 옥수수깡을 찾아줘! 놈들이 가져간 내 옥수수깡은 총 10개야.";
        tempArray[2] = "옆에 포탈을 만들어줄테니 다녀오라구!";
        tempArray[3] = "포탈에서 키보드 'S'를 누르면 이동할 수 있어.";
     
        return tempArray;
    }
    string[] InitTextArrayForMidScene()
    {
        string[] tempArray = new string[8];
        tempArray[0] = "친구! 벌써 옥수수깡 10개를 모두 구해온거야? ";
        tempArray[1] = "흑흑 너무 감동적인 맛이야! 정말 고마워 친구. ";
        tempArray[2] = "근데 말이야, 사실 이건 전설의 옥수수깡이 아니야. 내가 말한 건 이 노란 옥수수깡이 아니라고..";
        tempArray[3] = "다 먹어놓고 무슨 소리냐고? 그치만 내가 말한 전설의 옥수수깡은 매콤하고 짭짤한 하바나 옥수수깡이었단 말이야!";
        tempArray[4] = "돼지놈들이 가져간 것이 아니었나봐. 그렇다면 범인은 무시무시한 골렘이 틀림없어.";
        tempArray[5] = "뭐? 골렘쯤은 식은 죽 먹기라고? 그럼 골렘을 무찌르고 전설의 옥수수깡을 가져다주겠어? ";
        tempArray[6] = "고마워! 역시 친구밖에 없어! 전설의 옥수수깡을 가져오면 마을 사람들을 모두 초대해서 파티를 벌이자. ";
        tempArray[7] = "그럼 또 다시, 행운을 빌어!";


        return tempArray;
    }
    void ShowNextText()
    {
        textNum++;
        if (textNum < firstChracterSaying.Length)
        {
            sayingText.text = firstChracterSaying[textNum];
            if(midScene == true)
            {
                if(textNum == 1)
                {
                    Sprite[] sprites = Resources.LoadAll<Sprite>("Character/Neoguri_Character");
                    secondCharacterImage.sprite = sprites[4];
                    secondCharacterImage.color = new Color(1, 1, 1, 1);
                    firstCharacterImage.color = new Color(1, 1, 1, 0);
                }
                if(textNum == 5)
                {
                    Sprite[] sprites = Resources.LoadAll<Sprite>("Character/Neoguri_Character");
                    firstCharacterImage.sprite = sprites[6];
                    firstCharacterImage.color = new Color(1, 1, 1, 1);
                    secondCharacterImage.color = new Color(1, 1, 1, 0);
                }
            }
        }
        else
        {
            neoguriScript.SetActive(false);

            // 포탈 오브젝트 보이게함.
            portalObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
