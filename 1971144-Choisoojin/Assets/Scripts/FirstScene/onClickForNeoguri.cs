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
    public GameObject portalObject;  // 포탈 오브젝트(너구리와 대화가 끝나면 보인다)

    // Start is called before the first frame update
    void Start()
    { 
        // 처음에는 포탈이 보이지 않게 한다. (너구리와 대화가 끝나면 포탈이 보임)
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
        firstChracterSaying = InitTextArray();
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
    void ShowNextText()
    {
        textNum++;
        if (textNum < firstChracterSaying.Length)
        {
            sayingText.text = firstChracterSaying[textNum];
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
