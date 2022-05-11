using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving_Monster1 : MonoBehaviour
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
        Vector3 desiredPosition = new Vector3(
           Mathf.Clamp(target.position.x + offset.x, limitMinX, limitMaxX),   // X
           Mathf.Clamp(target.position.y + offset.y, limitMinY, limitMaxY), // Y
           -10);             // Z

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }
}
