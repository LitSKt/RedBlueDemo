using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSpeedUp : MonoBehaviour
{
    private bool ifSpeedUp;

    public bool IfSpeedUp { get => ifSpeedUp; set => ifSpeedUp = value; }

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<TrailRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IfSpeedUp)
        {
            transform.GetComponent<TrailRenderer>().enabled = true;
            transform.GetComponent<Animator>().speed = 1.5f;
            transform.GetComponent<NavMeshAgent>().speed = 8;
            transform.GetComponent<NavMeshAgent>().angularSpeed = 2000f;
            transform.GetComponent<PlayerAttack>().RotSpeed = 10f;
        }
    }
}
