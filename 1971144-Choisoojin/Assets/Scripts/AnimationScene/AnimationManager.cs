using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimationManager : MonoBehaviour
{
    public Image image1, image2;
    public Image midImage;

    // Start is called before the first frame update
    void Start()
    {
        image2.transform.SetAsFirstSibling();
        StartCoroutine("delayTime", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator delayTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        FadeOut(2);
    }
        public void FadeOut(float fadeOutTime, System.Action nextEvent = null)
    {
        StartCoroutine(CoFadeOut(fadeOutTime, nextEvent));
    }

    // 투명 -> 불투명
    IEnumerator CoFadeOut(float fadeOutTime, System.Action nextEvent = null)
    {
        float black = 0.5f;
        midImage.transform.SetAsLastSibling();
        Color tempColor = midImage.color;
        while (tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / fadeOutTime;
            midImage.color = tempColor;

            if (tempColor.a >= 1f) tempColor.a = 1f;

            yield return null;
        }

        midImage.color = tempColor;

        while (black < 1f)
        {
            black += Time.deltaTime / fadeOutTime;

            yield return null;
        }

        if (nextEvent != null)
        {
            nextEvent();
        }
        else
        {
            image2.transform.SetAsLastSibling();
            midImage.transform.SetAsLastSibling();
            FadeIn(2);
           
        }
            
    }
    public void FadeIn(float fadeOutTime, System.Action nextEvent = null)
    {
        StartCoroutine(CoFadeIn(fadeOutTime, nextEvent));
    }
    // 불투명 -> 투명
    IEnumerator CoFadeIn(float fadeInTime, System.Action nextEvent = null)
    {
        
        Color tempColor = midImage.color;
        while (tempColor.a > 0f)
        {
            tempColor.a -= Time.deltaTime / fadeInTime;
            midImage.color = tempColor;

            if (tempColor.a <= 0f) tempColor.a = 0f;

            yield return null;
        }
        midImage.color = tempColor;


        StartCoroutine("delayTimeAndGoNextScene", 1.5f);
    }

    //void goToNextScene()
    //{

    //    StartCoroutine("delayTimeAndGoNextScene", 1.5f);
    //}
    IEnumerator delayTimeAndGoNextScene(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        SceneManager.LoadScene("FinalScene");
    }
}
