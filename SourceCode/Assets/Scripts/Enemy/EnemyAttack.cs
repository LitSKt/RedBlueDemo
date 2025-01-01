using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    private GameObject player;
    private GameObject cam;
    private NavMeshAgent nav;
    private Vector3 playerOffset;
    private Vector3 enemyOffset;
    private Vector3 offset;
    private Quaternion targetRot;
    private Animator ani;
    private bool isReadyAttack;
    private bool ifRot;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.Find("Player");
        nav = transform.GetComponent<NavMeshAgent>();
        ani = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerOffset = player.transform.position;
        playerOffset.y = 0;
        enemyOffset = transform.position;
        enemyOffset.y = 0;
        offset = playerOffset - enemyOffset;

        //give effect while enemy is attacking
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("EnemyNormalAttackAnimation"))
        {
            transform.Find("Hands").Find("Hand1").GetComponent<EnemyAttackEffect>().UseEffect = true;
            transform.Find("Hands").Find("Hand2").GetComponent<EnemyAttackEffect>().UseEffect = true;
        }
        else
        {
            transform.Find("Hands").Find("Hand1").GetComponent<EnemyAttackEffect>().UseEffect = false;
            transform.Find("Hands").Find("Hand2").GetComponent<EnemyAttackEffect>().UseEffect = false;
        }

        if (!transform.GetComponent<EnemyMovement>().CombatStatus)
        {
            isReadyAttack = false;
        }

        //enemy ready attck code (rotate to player before attack)
        if (transform.GetComponent<EnemyMovement>().CombatStatus)
        {
            //if close then stop at first
            if (Vector3.Distance(playerOffset, enemyOffset) <= 5f)
            {
                nav.isStopped = true;

                //enemy will keep looking at player
                if (Vector3.Angle(transform.forward, offset) <= 1f)
                {
                    ifRot = false;
                    isReadyAttack = true;
                }
                else
                {
                    ifRot = true;
                    isReadyAttack = false;
                    transform.rotation = Quaternion.LookRotation(offset);
                }
            }
            else
            {
                isReadyAttack = false;
                nav.isStopped = false;
            }
        }

        //enemy rotate code
        if (ifRot && nav.isStopped)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 5f * Time.deltaTime);
        }

        //enemy attack code
        if (isReadyAttack)
        {
            ani.SetBool("NormalAttack", true);
        }
        else
        {
            ani.SetBool("NormalAttack", false);
        }
    }

    //enemy normal attck function
    void EnemyNormalAttack(float attackMulti)
    {
        if ((Vector3.Distance(playerOffset, enemyOffset) <= 5f) && (Vector3.Angle(transform.forward, offset) <= 30f))
        {
            transform.GetComponent<EnemyAttackSoundEffect>().NormalAttackSE = true;
            player.GetComponent<PlayerLifeInformation>().IfGetHurt = true;
            player.GetComponent<PlayerLifeInformation>().GetDamage = 1 * attackMulti;
        }
    }
}
