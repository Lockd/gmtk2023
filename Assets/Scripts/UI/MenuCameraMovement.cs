using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMovement : MonoBehaviour
{
    public bool isMoving;

    public GameObject moveTo;

    public int direction = 0;

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(moveTo.transform.position.x, moveTo.transform.position.y, transform.position.z), 5 * Time.deltaTime);
            //transform.Translate(new Vector2(moveTo.transform.position.x, moveTo.transform.position.y) * Time.deltaTime, Space.Self);
            if (direction == -1 && ((transform.position.x - 0.1 <= moveTo.transform.position.x)))
            {
                isMoving = false;
            }
            if (direction == 1 && transform.position.x >= moveTo.transform.position.x)
            {
                isMoving = false;
            }
        }
    }
}
