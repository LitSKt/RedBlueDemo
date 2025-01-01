using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBloodLifeTimer : MonoBehaviour
{
    private Vector3 offset;
    private float timer;
    private bool isStop;

    public float Timer { get => timer; set => timer = value; }

    // Start is called before the first frame update
    void Start()
    {
        isStop = false;
        transform.GetComponent<SphereCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!transform.GetComponent<SphereCollider>().enabled)
        {
            if ((Mathf.Abs(transform.localPosition.x) >= 0.57f) || (Mathf.Abs(transform.localPosition.y) >= 0.57f) || (Mathf.Abs(transform.localPosition.z) >= 0.57f))
            {
                transform.GetComponent<SphereCollider>().enabled = true;
            }
        }

        //ground blood timer code
        if (Mathf.Abs(transform.position.y - 0.07f) <= 0.001f)
        {
            Timer += Time.deltaTime;
        }

        //destroy code
        if (Timer >= 1f)
        {
            if (!isStop)
            {
                if (transform.GetComponent<Rigidbody>().linearVelocity.magnitude <= 0.5f)
                {
                    Destroy(transform.GetComponent<Rigidbody>());
                    isStop = true;
                }
            }
            else
            {
                offset = new Vector3(transform.position.x, transform.position.y - 0.14f, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, offset, 2.5f * Time.deltaTime);
            }
        }
        if (transform.position.y <= -0.07f)
        {
            Destroy(gameObject);
        }
    }
}
