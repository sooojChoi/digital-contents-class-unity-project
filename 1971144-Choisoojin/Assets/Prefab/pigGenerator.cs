using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pigGenerator : MonoBehaviour
{
    public GameObject obj; //MonsterPrefab 설정

    // 어느 공간에 랜덤으로 생성될지 알기 위해 startX, endX, startY 지정.
    public float startX = 0.0f; 
    public float endX = 0.0f;
    public float positionY = 0.0f;

    public int monsterNumber = 1;   // 처음에 생성할 몬스터 개수

    // Start is called before the first frame update
    void Start()
    {
        // 몬스터 생성
        generateMonster(obj, startX, endX, positionY, monsterNumber);
    }

    public static void generateMonster(GameObject mon, float startx, float endx, float y, int num)
    {
        SpriteRenderer sr = mon.GetComponent<SpriteRenderer>();

        // 몬스터를 num개 만큼 랜덤 생성한다.
        for (int i = 0; i < num; i++)
        {
            float rndX = Random.Range(startx, endx); // startX ~ endX 사이의 실수를 생성
            int rndR = Random.Range(0, 2);  // 0 또는 1을 생성

            Vector2 pos = new Vector2(rndX, y);
            // 어느 방향을 바라보게 생성될지도 랜덤이다.
            if (rndR == 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }

            Instantiate(mon, pos, mon.transform.rotation);  // 몬스터를 생성한다.
        }

    }
}
