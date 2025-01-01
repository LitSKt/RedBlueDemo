using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    public GameObject cam;
    private Ray ray;
    private bool isEnemyBeSelect;

    public bool IsEnemyBeSelect { get => isEnemyBeSelect; set => isEnemyBeSelect = value; }

    // Start is called before the first frame update
    void Start()
    {
        IsEnemyBeSelect = false;
    }

    // Update is called once per frame
    void Update()
    {
        //left mouse button select
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (!cam.GetComponent<GameOver>().IsEnemyDead)
                {
                    //hit enemy then select him
                    if (hit.collider.gameObject.CompareTag("Enemy") && !isEnemyBeSelect)
                    {
                        IsEnemyBeSelect = true;
                    }
                }
            }
        }
    }
}
