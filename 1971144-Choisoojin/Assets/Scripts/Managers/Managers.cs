using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    DataManager _data = new DataManager();
    static Managers Instance;  //유일성이 보장된다.

    public static DataManager Data { get { return Instance._data; } }
    public static Managers GetInstance() { Init(); return Instance; }  //유일한 매니저를 갖고온다.

    // Start is called before the first frame update
    void Start()
    {
		//Init();

	}
    private void Awake()
    {
		Init();
	}

    static void Init()
	{
		if (Instance == null)
		{
			GameObject go = GameObject.Find("@Managers");
			if (go == null)
			{
				go = new GameObject { name = "@Managers" };
				go.AddComponent<Managers>();
			}

			DontDestroyOnLoad(go);
			Instance = go.GetComponent<Managers>();

			Instance._data.Init();
		}
	}
}
