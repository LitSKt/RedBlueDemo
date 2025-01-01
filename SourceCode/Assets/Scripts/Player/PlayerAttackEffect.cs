using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEffect : MonoBehaviour
{
    private bool useEffect;

    public bool UseEffect { get => useEffect; set => useEffect = value; }

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<TrailRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (UseEffect)
        {
            transform.GetComponent<TrailRenderer>().enabled = true;
        }
        else
        {
            transform.GetComponent<TrailRenderer>().enabled = false;
        }
    }
}
