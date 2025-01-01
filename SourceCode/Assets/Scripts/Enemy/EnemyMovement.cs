using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent nav;
    private Vector3 playerOffset;
    private Vector3 enemyOffset;
    private Vector3 startPos;
    private Vector3 offset;
    private Quaternion targetRot;
    public float maxCatchRange;
    private bool ifRotate;
    private bool combatStatus;
    private bool isInView;

    public bool CombatStatus { get => combatStatus; set => combatStatus = value; }

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        //mark enemy's home
        startPos = transform.position;
        maxCatchRange = 12;
        CombatStatus = false;
        isInView = false;
        ifRotate = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //set player's and enemy's position to the same plane (enemy's transform plane) to decrease error
        playerOffset = player.transform.position;
        playerOffset.y = 0;
        enemyOffset = transform.position;
        enemyOffset.y = 0;
        offset = playerOffset - enemyOffset;

        //detect if player in enemy's view code
        if (!combatStatus)
        {
            //judge if enemy can see the player, set rotate flag false at the same time
            if (Vector3.Angle(transform.forward, offset) <= 70)
            {
                isInView = true;
                ifRotate = false;
                nav.isStopped = false;
            }
            //else set isInView false to show that enemy can't see player
            else
            {
                isInView = false;
                //if enemy get hurt then he will turn to the player
                if (transform.GetComponent<EnemyLifeInformation>().IfGetHurt)
                {
                    targetRot = Quaternion.LookRotation(offset);
                    nav.isStopped = true;
                    ifRotate = true;
                }
            }
        }

        //enemy rotate code
        if (ifRotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 2f * Time.deltaTime);
        }

        //chase player code
        if ((Vector3.Distance(playerOffset, enemyOffset) <= maxCatchRange) && isInView)
        {
            nav.SetDestination(player.transform.position);
            if (!combatStatus)
            {
                //attack player only when is close and player in enemy's view
                CombatStatus = true;
            }
        }
        //enemy will lost attraction if he's too far with player
        if (Vector3.Distance(playerOffset, enemyOffset) > maxCatchRange)
        {
            nav.SetDestination(startPos);
            CombatStatus = false;
        }
    }
}
