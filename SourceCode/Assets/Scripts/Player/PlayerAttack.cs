using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    private GameObject enemy;
    private NavMeshAgent nav;
    private Vector3 enemyOffset;
    private Vector3 playerOffset;
    private Vector3 offset;
    private Quaternion targetRot;
    private Animator ani;
    private float rotSpeed;
    private bool ifRot;
    private bool isReadyAttack;
    private bool combatStatus;

    public bool CombatStatus { get => combatStatus; set => combatStatus = value; }
    public float RotSpeed { get => rotSpeed; set => rotSpeed = value; }

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        nav = transform.GetComponent<NavMeshAgent>();
        ani = transform.GetComponent<Animator>();
        RotSpeed = 8f;
        ifRot = false;
        isReadyAttack = false;
        CombatStatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerOffset = transform.position;
        playerOffset.y = 0;
        enemyOffset = enemy.transform.position;
        enemyOffset.y = 0;
        offset = enemyOffset - playerOffset;

        //enable hands effect while attacking
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("PlayerNormalAttackAnimation"))
        {
            transform.Find("Hands").Find("Hand2").GetComponent<PlayerAttackEffect>().UseEffect = true;
        }
        else
        {
            transform.Find("Hands").Find("Hand2").GetComponent<PlayerAttackEffect>().UseEffect = false;
        }

        if (!combatStatus)
        {
            isReadyAttack = false;
        }

        if (transform.GetComponent<PlayerSelect>().IsEnemyBeSelect)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                CombatStatus = true;
            }
        }

        if (combatStatus)
        {
            nav.SetDestination(enemyOffset);
        }

        //prepare for player's attack
        if ((Vector3.Distance(playerOffset, enemyOffset) <= 3.6f) && CombatStatus)
        {
            nav.isStopped = true;
            {
                //if player is face to enemy then ready to attack
                if (Vector3.Angle(transform.forward, offset) <= 1f)
                {
                    ifRot = false;
                    isReadyAttack = true;
                }
                //if not player will rotate at first
                else
                {
                    targetRot = Quaternion.LookRotation(offset);
                    isReadyAttack = false;
                    ifRot = true;
                }
            }
        }
        else
        {
            nav.isStopped = false;
        }

        //player rotate code
        if (ifRot && nav.isStopped)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
        }

        //player attack code
        if (isReadyAttack)
        {
            ani.SetBool("NormalAttack", true);
        }
        else
        {
            ani.SetBool("NormalAttack", false);
        }
    }

    //player normal attack function
    void PlayerNormalAttack(float attackMulti)
    {
        if (Vector3.Distance(playerOffset, enemyOffset) <= 3.6f)
        {
            transform.GetComponent<PlayerAttackSoundEffect>().NormalAttackSE = true;
            enemy.GetComponent<EnemyLifeInformation>().IfGetHurt = true;
            enemy.GetComponent<EnemyLifeInformation>().GetDamage = 1 * attackMulti;
        }
    }
}
