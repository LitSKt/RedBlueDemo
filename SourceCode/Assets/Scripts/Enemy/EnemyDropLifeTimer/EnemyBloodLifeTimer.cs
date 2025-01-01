using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBloodLifeTimer : MonoBehaviour
{
    private Vector3 offset;
    private float timer;
    private bool isStop;

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
            if ((Mathf.Abs(transform.localPosition.x) >= 1.45f) || (Mathf.Abs(transform.localPosition.y) >= 1.45f) || (Mathf.Abs(transform.localPosition.z) >= 1.45f))
            {
                transform.GetComponent<SphereCollider>().enabled = true;
            }
        }

        //blood on ground timer code
        if (Mathf.Abs(transform.position.y - 0.15f) <= 0.001f)
        {
            timer += Time.deltaTime;
        }

        //destroy code (after 2s)
        if (timer >= 1.5f)
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
                offset = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, offset, 2f * Time.deltaTime);
            }
        }
        if (transform.position.y <= -0.15f)
        {
            Destroy(gameObject);
        }
    }
}
