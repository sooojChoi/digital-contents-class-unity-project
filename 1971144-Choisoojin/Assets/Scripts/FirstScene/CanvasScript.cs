using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public Button nextButton;
    public Text sayingText;
    public GameObject firstCanvasObject;
    string[] firstChracterSaying;
    int textNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        firstChracterSaying = InitTextArray();
        sayingText.text = firstChracterSaying[textNum];
        nextButton.onClick.AddListener(ShowNextText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string[] InitTextArray()
    {
        string[] tempArray = new string[5];
        tempArray[0] = "우리 마을에 오신 것을 환영합니다.";
        tempArray[1] = "모험가님, 소문을 듣고 오셨군요.";
        tempArray[2] = "보스 몬스터를 잡으면 엄청난 보물을 받을 수 있다는 소문 말이에요!";
        tempArray[3] = "그 몬스터 때문에 우리 마을 사람들 모두 겁에 질려 있어요.";
        tempArray[4] = "모험가님, 보스 몬스터를 잡고 저를 찾아주세요. 기다리고 있을게요.";

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
