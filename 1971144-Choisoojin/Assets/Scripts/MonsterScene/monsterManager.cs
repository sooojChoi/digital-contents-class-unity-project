using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterManager : MonoBehaviour
{
    public static int killNumber = 0;  // 플레이어가 죽인 몬스터 수 (총 개수)

    public static int monsterNumPoint1 = 2;  // 1구역에 현재 존재하는 몬스터 개수
    public static int monsterNumPoint2 = 3;  // 2구역에 현재 존재하는 몬스터 개수
    public static int monsterNumPoint3 = 5;  // 3구역에 현재 존재하는 몬스터 개수

    // 어느 공간에 랜덤으로 생성될지 알기 위해 startX, endX, positionY 지정.(구역마다 다르게 설정되는 것)
    float[] startX = new float[3];
    float[] endX = new float[3];
    float[] positionY = new float[3];

    public GameObject mon1;  // 구역1에 생성되는 몬스터 프리팹
    public GameObject mon2;  // 구역2에 생성되는 몬스터 프리팹
    public GameObject mon3;  // 구역3에 생성되는 몬스터 프리팹

    // Start is called before the first frame update
    void Start()
    {
        startX[0] = 12;
        endX[0] = 22;
        positionY[0] = 2.75f;

        startX[1] = 20;
        endX[1] = 36;
        positionY[1] = 10.8f;

        startX[2] = 56;
        endX[2] = 83;
        positionY[2] = 2.8f;

    }

    // Update is called once per frame
    void Update()
    {
        // 원래 있어야 하는 몬스터 수보다 적게 있으면(플레이어가 죽여서), 3초뒤에 몬스터를 한 마리 더 생성한다.
        if(monsterNumPoint1 < 2)
        {
            // 3초 뒤에 이 함수를 호출하도록 함.
            StartCoroutine("generateObj", 1);
            monsterNumPoint1++;
        }
        if(monsterNumPoint2 < 3)
        {
            // 3초 뒤에 이 함수를 호출하도록 함.
            StartCoroutine("generateObj", 2);
            monsterNumPoint2++;
        }
        if (monsterNumPoint3 < 5)
        {
            // 3초 뒤에 이 함수를 호출하도록 함.
            StartCoroutine("generateObj", 3);
            monsterNumPoint3++;
        }

        
    }

    IEnumerator generateObj(int sort)
    {
        // sort는 구역을 의미. 6초 쉬고 새로운 몬스터를 지정된 구역에 생성한다.
        yield return new WaitForSeconds(6.0f);

        if (sort == 1)
        {
            pigGenerator.generateMonster(mon1, startX[0], endX[0], positionY[0], 1);
        }
        else if (sort == 2)
        {
            pigGenerator.generateMonster(mon2, startX[1], endX[1], positionY[1], 1);
        }
        else if (sort == 3)
        {
            pigGenerator.generateMonster(mon3, startX[2], endX[2], positionY[2], 1);

        }
    }
}
