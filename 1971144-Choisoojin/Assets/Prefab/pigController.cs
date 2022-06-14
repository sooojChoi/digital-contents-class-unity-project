using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pigController : MonoBehaviour
{
    Animator animator;
    // 몬스터가 움직일 때마다 보여지는 이미지의 flip을 변경하기 위해 필요.
    // 돼지 몬스터 이미지는 flipX가 false면 오른쪽을 보고, true이면 하면 왼쪽을 본다.
    SpriteRenderer sr;

    public GameObject fakeCorn;  // 몬스터가 죽으면서 생성되는 아이템

    public int whoAmI = 0;  // 1구역 돼지면 1, 2구역이면 2, 3구역이면 3

    // 어느 공간에 랜덤으로 생성될지 알기 위해 startX, endX, positionY 지정.
    public float startX = 0.0f;
    public float endX = 0.0f;
    public float positionY = 0.0f;
    public float speed = 5f;//Monster의 이동 속도
    string direction = "any";  // 몬스터가 이동할 방향. "any", "left", "right" 중 하나이다.

    public float idleMinTime = 1.5f;
    public float idleMaxTime = 3.0f;
    public float walkingMinTime = 2.0f;
    public float walkingMaxTime = 4.0f;

    float offset = 0.0f;  // 몬스터의 이동 구역을 지정할 때 이용한다.

    // 현재 몬스터의 상태
    string state = "idle";

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        float rndTime = Random.Range(idleMinTime, idleMaxTime); // idle 랜덤 유지시간.
        StartCoroutine("delayTime", rndTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "walking")
        {
            if(sr.flipX == false)  // 오른쪽으로 이동할 것.
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else if(sr.flipX == true){  //왼쪽으로 이동할 것.
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }


            // 이동한 위치가 정해진 구역을 벗어나면, 걷는 것을 멈추고 idle상태로 변환.
            if(transform.position.x < startX+ offset || transform.position.x > endX - offset)
            {
                // 다음에 걷기 시작할 때는 반대 방향으로 걸어가도록 한다.
                if(transform.position.x < startX + offset)
                {
                    direction = "right";
                }
                else
                {
                    direction = "left";
                }
                state = "idle";
                animator.SetBool("isWalking", false);
            }
        }
    }

    IEnumerator delayTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        if (state == "idle")
        {
            state = "walking";
            animator.SetBool("isWalking", true);

            if(direction == "any")
            {
                // 0 또는 1을 생성, 새로 걸어나갈 방향을 랜덤으로 정한다.
                int rndFlip = Random.Range(0, 2); 

                if (rndFlip == 0)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }
            }
            else if(direction == "left")
            {
                // 왼쪽으로 가야함.
                sr.flipX = true;
            }else if(direction == "right")
            {
                // 오른쪽으로 가야함.
                sr.flipX = false;
            }
            
            float rndTime = Random.Range(walkingMinTime, walkingMaxTime); // walking 랜덤 유지시간.
            StartCoroutine("delayTime", rndTime);
        }
        else if(state == "walking")
        {
            state = "idle";
            animator.SetBool("isWalking", false);
            float rndTime = Random.Range(idleMinTime, idleMaxTime); // idle 랜덤 유지시간.
            StartCoroutine("delayTime", rndTime);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("돼지랑 무언가랑 충돌함.");
        // 현재 게임 오브젝트가 누군가와 충돌했는데 그 충돌체의 태그가 "Weapon"이라면, 
        if (collision.gameObject.tag == "Weapon")
        {
            Debug.Log("돼지랑 무기랑 충돌함.");
            if (whoAmI == 1)
            {
                monsterManager.monsterNumPoint1 -= 1;
            }
            else if (whoAmI == 2)
            {
                monsterManager.monsterNumPoint2 -= 1;
            }
            else if (whoAmI == 3)
            {
                monsterManager.monsterNumPoint3 -= 1;
            }
            monsterManager.killNumber += 1;  // 내가 죽인 몬스터 수가 1만큼 증가한다.
            Destroy(collision.gameObject);


            animator.SetBool("isDie", true);

            // 나도 없애고 그 충돌체도 없애라.
            StartCoroutine("dieMonster");
        }
    }



    IEnumerator dieMonster()
    {   
        yield return new WaitForSecondsRealtime(0.8f);
        Destroy(gameObject);
        Instantiate(fakeCorn, gameObject.transform.position, fakeCorn.transform.rotation);  // 옥수수깡을 생성한다.
    }
}
