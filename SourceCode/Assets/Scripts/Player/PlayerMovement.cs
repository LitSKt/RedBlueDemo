using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public GameObject moveIcon;
    private GameObject enemy;
    public GameObject cam;
    private NavMeshAgent nav;
    private Vector3 offset;
    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        //right mouse button move
        if (Input.GetMouseButtonDown(1))
        {
            //interrupt player's attack
            transform.GetComponent<PlayerAttack>().CombatStatus = false;
            nav.isStopped = false;

            //get the mouse hit point on the ground (the layer 6 is Ground)
            ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //hit ground then move to the hit point
                if (hit.collider.gameObject.layer == 6)
                {
                    //draw a move icon on the ground
                    offset = new Vector3(hit.point.x, hit.point.y + 0.05f, hit.point.z);
                    GameObject mi = Instantiate(moveIcon, offset, Quaternion.Euler(new Vector3(90, 0, 0)));
                    Destroy(mi, 0.6f);
                    //go to destination using ai
                    nav.SetDestination(hit.point);
                }
                if (!cam.GetComponent<GameOver>().IsEnemyDead)
                {
                    //hit enemy then move to him
                    if (hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        offset = enemy.transform.position;
                        offset.y = 0;
                        nav.SetDestination(offset);
                    }
                }
            }
        }
    }
}
