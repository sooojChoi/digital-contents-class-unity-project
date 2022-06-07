using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockController : MonoBehaviour
{
    public float speed = 8f;//돌의 이동 속도

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("delayTime", 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1,-1,0) * speed * Time.deltaTime);
    }

    IEnumerator delayTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        Destroy(gameObject);
    }
}
