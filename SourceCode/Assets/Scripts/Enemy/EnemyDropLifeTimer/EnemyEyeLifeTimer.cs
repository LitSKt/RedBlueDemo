using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyeLifeTimer : MonoBehaviour
{
    private Vector3 offset;
    private float timer;
    private bool isStop;

    // Start is called before the first frame update
    void Start()
    {
        isStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!transform.GetComponent<SphereCollider>().enabled)
        {
            if ((Mathf.Abs(transform.localPosition.x) >= 2f) || (Mathf.Abs(transform.localPosition.y) >= 2f) || (Mathf.Abs(transform.localPosition.z) >= 2f))
            {
                transform.GetComponent<SphereCollider>().enabled = true;
            }
        }

        //eyes on ground timer code
        if (Mathf.Abs(transform.position.y - 0.25f) <= 0.001f)
        {
            timer += Time.deltaTime;
        }

        //destroy code (after 2s)
        if (timer >= 2.5f)
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
                offset = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, offset, 1.5f * Time.deltaTime);
            }
        }
        if (transform.position.y <= -0.25f)
        {
            Destroy(gameObject);
        }
    }
}
