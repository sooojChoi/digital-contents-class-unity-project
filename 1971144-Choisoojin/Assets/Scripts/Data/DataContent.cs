using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameSaveData
{
    public string mapNumber;  // "mapNumber"
    public string realMapNumber;  // 실제 맵 이름 들어감.
}

[Serializable]
public class GameSaveInfo : ILoader<string, GameSaveData>
{
    public List<GameSaveData> gameSaveInfo = new List<GameSaveData>();

    public Dictionary<string, GameSaveData> MakeDict()
    {
        Dictionary<string, GameSaveData> dict = new Dictionary<string, GameSaveData>();

        foreach (GameSaveData save in gameSaveInfo)
        {
            dict.Add(save.mapNumber, save);
        }

        return dict;
    }
}


[Serializable]
public class Item
{
    public string name;
    public string koname;
    public int price;
    public int hp; 
    public int mp;
}

[Serializable]
public class ItemInfo : ILoader<string, Item>
{
    public List<Item> itemInfo = new List<Item>();

    public Dictionary<string, Item> MakeDict()
    {
        Dictionary<string, Item> dict = new Dictionary<string, Item>();

        foreach (Item item in itemInfo)
        {
            dict.Add(item.name, item);
        }

        return dict;
    }
}

[Serializable]
public class playerData  // player의 정보를 가진 json. 소유한 아이템이나 돈 정보 등.
{
    public string name;  // 아이템의 경우 아이템의 이름,
                         // 돈 정보는 "money", 설정된 회복 아이템은 "hpItem", 설정된 공격 아이템은 "mpItem", 플레이어 체력은 "hp"
                         // "hpItem"과 "mpItem"의 아이템 이름은 sort에 저장된다. "content"는 항상 1.
                         // money와 hpItem, mpItem, hp는 없어지면 안되는 정보.

    public int content;  // 아이템의 경우 아이템 개수, 돈의 경우 얼마를 소유하고 있는지.
    public string sort;  // 아이템의 경우 hp인지 mp인지. 돈의 경우 null.
}

[Serializable]
public class playerDataInfo : ILoader<string, playerData>
{
    public List<playerData> playerInfo = new List<playerData>();

    public Dictionary<string, playerData> MakeDict()
    {
        Dictionary<string, playerData> dict = new Dictionary<string, playerData>();

        foreach (playerData player in playerInfo)
        {
            dict.Add(player.name, player);
        }

        return dict;
    }
}