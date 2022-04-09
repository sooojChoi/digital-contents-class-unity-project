using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 3;
    public float slowSpeed = 1;
    public Vector2 offset;
    public float limitMinX, limitMaxX, limitMinY, limitMaxY;

    public static int meetingSellerNow = 0;
    public static int sellerForCanvasScript = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       // meetingSeller();
        
    }
    private void LateUpdate()
    {
        if (transform.position.x > 30.0 && transform.position.x < 31.0 && CanvasScript.sellerScriptEnd == 0)
        {
            meetingSellerNow = 1;
            sellerForCanvasScript = 1;  // canvasScript에서 지금 상인을 만났다는 것을 알기 위함.
        }
       
        if (meetingSellerNow == 1)
        {
            Vector3 des = new Vector3(33, 0, -10);
            transform.position = Vector3.Lerp(transform.position, des, Time.deltaTime * 2);
            return;
        }
        Vector3 desiredPosition = new Vector3(
           Mathf.Clamp(target.position.x + offset.x, limitMinX, limitMaxX),   // X
           Mathf.Clamp(target.position.y + offset.y, limitMinY, limitMaxY), // Y
           -10);             // Z

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }

}
