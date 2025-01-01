using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameOver : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;
    private bool isPlayerDead;
    private bool isEnemyDead;

    public bool IsPlayerDead { get => isPlayerDead; set => isPlayerDead = value; }
    public bool IsEnemyDead { get => isEnemyDead; set => isEnemyDead = value; }

    // Start is called before the first frame update
    void Start()
    {
        IsPlayerDead = false;
        IsEnemyDead = false;
        if (!IsPlayerDead)
        {
            player = GameObject.Find("Player");
        }
        if (!isEnemyDead)
        {
            enemy = GameObject.Find("Enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //enemy will do something after killed player
        if (IsPlayerDead)
        {
            if (!isEnemyDead)
            {
                enemy.GetComponent<EnemyMovement>().CombatStatus = false;
                EnemyCongratulations();
            }
        }

        //so does player
        if (isEnemyDead)
        {
            if (!IsPlayerDead)
            {
                player.GetComponent<PlayerAttack>().CombatStatus = false;
                PlayerCongratulations();
            }
        }
    }

    void PlayerCongratulations()
    {
        player.GetComponent<Animator>().SetBool("NormalAttack", false);
        player.GetComponent<Animator>().SetTrigger("Victory");
    }

    void EnemyCongratulations()
    {
        enemy.GetComponent<Animator>().SetBool("NormalAttack", false);
        enemy.GetComponent<Animator>().SetTrigger("Victory");
    }
}
