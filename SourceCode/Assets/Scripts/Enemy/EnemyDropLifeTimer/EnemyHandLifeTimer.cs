using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandLifeTimer : MonoBehaviour
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
            if ((Mathf.Abs(transform.localPosition.x) >= 2f) || (Mathf.Abs(transform.localPosition.y) >= 2f) || (Mathf.Abs(transform.localPosition.z) >= 2f))
            {
                transform.GetComponent<SphereCollider>().enabled = true;
            }
        }

        //ground hands timer code
        if (Mathf.Abs(transform.position.y - 0.5f) <= 0.001f)
        {
            timer += Time.deltaTime;
        }

        //destroy code
        if (timer >= 2f)
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
                offset = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, offset, 2.5f * Time.deltaTime);
            }
        }
        if (transform.position.y <= -0.5f)
        {
            Destroy(gameObject);
        }
    }
}
