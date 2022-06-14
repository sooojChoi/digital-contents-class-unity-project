using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalUIManager : MonoBehaviour
{
    public Button inputButton;
    public Button exitButton;
    public GameObject noticeImage;
    public Button closeNotice;

    // Start is called before the first frame update
    void Start()
    {
        inputButton.onClick.AddListener(showNoticeImage);
        exitButton.onClick.AddListener(ExitGame);
        closeNotice.onClick.AddListener(CloseNoticeImage);
        noticeImage.SetActive(false);
    }

    void showNoticeImage()
    {
        //  SceneManager.LoadScene("HomeScene");
        noticeImage.SetActive(true);
    }
    void CloseNoticeImage()
    {
        noticeImage.SetActive(false);
    }

    void ExitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
