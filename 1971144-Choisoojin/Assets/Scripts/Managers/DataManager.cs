using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}
public class DataManager : MonoBehaviour
{
    public Dictionary<string, GameSaveData> GameSaveData { get; private set; } = new Dictionary<string, GameSaveData>();
    public Dictionary<string, Sprite> ItemSprite { get; private set; } = new Dictionary<string, Sprite>();  //아이템 이미지를 담을 딕셔너리.
    public Dictionary<string, Item> ItemData { get; private set; } = new Dictionary<string, Item>();


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Init()
    {
        GameSaveData = LoadJson<GameSaveInfo, string, GameSaveData>("gameSaveData").MakeDict();
        ItemData = LoadJson<ItemInfo, string, Item>("itemData").MakeDict();
        foreach (KeyValuePair<string, Item> recp in ItemData)
        {
            //전체 아이템이 담긴 ItemData를 활용하여 {아이템 이름-Key, 이미지-Value} Dictionary를 만든다.
            Sprite sprite = Resources.Load<Sprite>($"Image/Item/{recp.Key}");
            //이미지를 저장할 때 '이미지 파일 이름'과 ItemData.json에 저장하는 "name"의 이름이 동일해야 한다.(대소문자까지)
            //ex) Mint.jpg파일로 저장했으면 ItemData에도 "name":"Mint"로, PlayerData에서도 "element":"Mint"로 해야함
            if (sprite != null)
            {
                ItemSprite.Add(recp.Key, sprite);  //아이템이름과 이미지를 담는다.
            }
        }
    }


    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

}
