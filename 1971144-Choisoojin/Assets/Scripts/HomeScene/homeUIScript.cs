using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class homeUIScript : MonoBehaviour
{
    public Button newGameStartButton;
    public Button loadGameButton;

    void Init()
    {
        newGameStartButton.onClick.AddListener(loadFirstScene);
        string mapNumber = Managers.Data.GameSaveData["mapNumber"].realMapNumber;
        
        if (mapNumber == "firstScene")
        {
            loadGameButton.interactable = false;
        }else if (mapNumber == "secondScene")
        {
            loadGameButton.onClick.AddListener(loadSecondScene);
        }
        // 계속 구현해야됨.
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    void loadSecondScene()
    {
        SceneManager.LoadScene("secondScene");
    }
    void loadFirstScene()
    {
        SceneManager.LoadScene("firstScene");
    }
  

    // Update is called once per frame
    void Update()
    {
        
    }
}
