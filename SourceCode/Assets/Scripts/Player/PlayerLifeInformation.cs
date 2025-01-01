using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLifeInformation : MonoBehaviour
{
    public GameObject playerBlood;
    public GameObject playerEye;
    public GameObject playerHand;
    public GameObject playerSpear;
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
        maxLife = 100;
        CurrentLife = maxLife;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
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
                cam.GetComponent<GameOver>().IsPlayerDead = true;
                DeadEyesFly(2);
                DeadHandsFly(2);
                DeadSpearFly(1);
                GenerateBlood(100);
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
            GameObject pBlood = Instantiate(playerBlood, transform.position, Quaternion.identity);
            pBlood.GetComponent<Rigidbody>().linearVelocity = bloodSpeed * 10f;
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
            GameObject pEye = Instantiate(playerEye, transform.position, Quaternion.identity);
            pEye.GetComponent<Rigidbody>().linearVelocity = eyeSpeed * 10f;
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
            GameObject pEye = Instantiate(playerHand, transform.position, Quaternion.identity);
            pEye.GetComponent<Rigidbody>().linearVelocity = handSpeed * 10f;
        }
    }

    void DeadSpearFly(int loopNum)
    {
        for (int i = 0; i < loopNum; i++)
        {
            //give blood a initial velocity to make it real
            float x = Random.Range(-0.2f, 0.2f);
            float y = Random.Range(0.5f, 1f);
            float z = Random.Range(-0.2f, 0.2f);
            Vector3 handSpeed = new Vector3(x, y, z);
            GameObject pEye = Instantiate(playerSpear, transform.position, Quaternion.identity);
            pEye.GetComponent<Rigidbody>().linearVelocity = handSpeed * 10f;
        }
    }
}
