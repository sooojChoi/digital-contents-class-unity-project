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