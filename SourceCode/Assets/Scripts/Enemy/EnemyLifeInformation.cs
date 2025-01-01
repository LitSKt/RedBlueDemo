using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class EnemyLifeInformation : MonoBehaviour
{
    public GameObject enemyBlood;
    public GameObject enemyEye;
    public GameObject enemyHand;
    private GameObject player;
    private GameObject enemyHealthBar;
    private GameObject cam;
    public float maxLife;
    private float currentLife;
    private float getDamage;
    private bool ifGetHurt;

    public bool IfGetHurt { get => ifGetHurt; set => ifGetHurt = value; }
    public float GetDamage { get => getDamage; set => getDamage = value; }
    public float CurrentLife { get => currentLife; set => currentLife = value; }

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        maxLife = 120;
        CurrentLife = maxLife;
        player = GameObject.Find("Player");
        enemyHealthBar = GameObject.Find("EnemyHealthBar");
        enemyHealthBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerSelect>().IsEnemyBeSelect)
        {
            enemyHealthBar.SetActive(true);
        }

        if (IfGetHurt)
        {
            ifGetHurt = false;
            //blood effect
            GenerateBlood((int)GetDamage);
            //get attack and lose life
            if (currentLife - getDamage <= 0)
            {
                currentLife = 0;
            }
            else
            {
                CurrentLife -= GetDamage;
            }
            if (currentLife == 0)
            {
                cam.GetComponent<GameOver>().IsEnemyDead = true;
                GenerateBlood(120);
                DeadEyesFly(1);
                DeadHandsFly(2);
                gameObject.SetActive(false);
            }
        }
    }

    void GenerateBlood(int loopNum)
    {
        for (int i = 0; i < loopNum; i++)
        {
            //give blood a initial velocity to make it real
            float x = Random.Range(-0.2f, 0.2f);
            float y = Random.Range(0f, 0.3f);
            float z = Random.Range(-0.2f, 0.2f);
            Vector3 bloodSpeed = new Vector3(x, y, z);
            GameObject eBlood = Instantiate(enemyBlood, transform.position, Quaternion.identity);
            eBlood.GetComponent<Rigidbody>().linearVelocity = bloodSpeed * 20f;
        }
    }

    void DeadEyesFly(int loopNum)
    {
        for (int i = 0; i < loopNum; i++)
        {
            //give blood a initial velocity to make it real
            float x = Random.Range(-0.2f, 0.2f);
            float y = Random.Range(0.5f, 1f);
            float z = Random.Range(-0.2f, 0.2f);
            Vector3 eyeSpeed = new Vector3(x, y, z);
            GameObject eEye = Instantiate(enemyEye, transform.position, Quaternion.identity);
            eEye.GetComponent<Rigidbody>().linearVelocity = eyeSpeed * 20f;
        }
    }

    void DeadHandsFly(int loopNum)
    {
        for (int i = 0; i < loopNum; i++)
        {
            //give blood a initial velocity to make it real
            float x = Random.Range(-0.2f, 0.2f);
            float y = Random.Range(0.5f, 1f);
            float z = Random.Range(-0.2f, 0.2f);
            Vector3 handSpeed = new Vector3(x, y, z);
            GameObject pEye = Instantiate(enemyHand, transform.position, Quaternion.identity);
            pEye.GetComponent<Rigidbody>().linearVelocity = handSpeed * 10f;
        }
    }
}
