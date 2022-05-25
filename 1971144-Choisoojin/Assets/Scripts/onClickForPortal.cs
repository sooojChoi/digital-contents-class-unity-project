using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class onClickForPortal : MonoBehaviour
{
    public string gameSceneName;
    public GameObject character;
    public Image foregroundImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
  
        float characterX = character.transform.position.x;
        float offsetX = 2;
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (characterX < x + offsetX && characterX > x - offsetX)
            {
                FadeOut(1.5f, goToOtherScene);   // 화면이 점점 어두워지고, 씬 이동한다.
            }
        }
    }

    void goToOtherScene()
    {
        switch (gameSceneName)
        {
            case "MonsterScene":
                {
                    SceneManager.LoadScene("MonsterScene");
                    break;
                }
            default:
                break;
        }
    }

    public void FadeOut(float fadeOutTime, System.Action nextEvent = null)
    {
        StartCoroutine(CoFadeIn(fadeOutTime, nextEvent));
    }

    // 투명 -> 불투명
    IEnumerator CoFadeIn(float fadeOutTime, System.Action nextEvent = null)
    {
        float black = 0.5f;
        foregroundImage.transform.SetAsLastSibling();
         Color tempColor = foregroundImage.color;
        while (tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / fadeOutTime;
            foregroundImage.color = tempColor;

            if (tempColor.a >= 1f) tempColor.a = 1f;

            yield return null;
        }

        foregroundImage.color = tempColor;

        while (black < 1f)
        {
            black += Time.deltaTime / fadeOutTime;
            
            yield return null;
        }
        if (nextEvent != null) nextEvent();
    }
}
