using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 moveVec;
    private Vector3 offset;
    private Ray ray;
    private Camera cam;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
        moveVec = Vector3.zero;
        cam = transform.GetComponent<Camera>();
        speed = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        //move camera code
        if (Input.GetMouseButtonDown(2))
        {
            //generate a plane to receive the ray in order to transform mouse position into global position
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //get the start position
                startPos = hit.point;
            }
        }
        else if (Input.GetMouseButton(2))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //get the end position
                endPos = hit.point;
                //get the move distance
                moveVec = endPos - startPos;
                //get the new position
                offset = transform.position - moveVec;
            }
        }
        //adjust camera fov code
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            offset = transform.position;
            offset -= 20 * Input.GetAxis("Mouse ScrollWheel") * offset.normalized;
            offset.x = transform.position.x;
        }
        if (offset.y < 6)
        {
            offset.z = 6 * (offset.z / offset.y);
            offset.y = 6;
        }
        else if (offset.y > 18)
        {
            offset.z = 18 * (offset.z / offset.y);
            offset.y = 18;
        }
        //using vector3.lerp to make grabing camera smooth
        transform.position = Vector3.Lerp(transform.position, offset, speed * Time.deltaTime);
    }
}
