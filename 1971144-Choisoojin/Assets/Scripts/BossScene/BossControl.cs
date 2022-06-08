using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossControl : MonoBehaviour
{
    int hp;

    Animator animator;

    public GameObject rock;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        float rndTime = Random.Range(2.5f, 4); // 공격하지 않는 휴식 시간.
        StartCoroutine("delayTime", rndTime);

        hp = 1000;
        SetMaxHealth(hp);
    }

    // Update is called once per frame
    void Update()
    {
        if(hp < 0)
        {
            animator.SetBool("die", true);
        }
        SetHealth(hp);
    }

    IEnumerator delayTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        float rndX = Random.Range(-7, 3); // startX ~ endX 사이의 실수를 생성

        animator.SetTrigger("throw");
        // 돌을 생성한다.
        Instantiate(rock, new Vector2(rndX, 10), rock.transform.rotation);
        Instantiate(rock, new Vector2(rndX+7, 10), rock.transform.rotation);  
        Instantiate(rock, new Vector2(rndX + 14, 10), rock.transform.rotation);  

        float rndTime = Random.Range(2.5f, 4); // 공격하지 않는 휴식 시간.
        StartCoroutine("delayTime", rndTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        // 현재 게임 오브젝트가 누군가와 충돌했는데 그 충돌체의 태그가 "Weapon"이라면, 
        if (collision.gameObject.tag == "Weapon")
        {
            hp -= 50;
            Destroy(collision.gameObject);

        }else if(collision.gameObject.tag == "Bomb")
        {
            hp -= 70;
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
