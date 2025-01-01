using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private GameObject enemy;
    private Slider slider;
    private Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        slider = transform.GetComponent<Slider>();
        startRot = Quaternion.Euler(new Vector3(-36, -180, 0));
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = startRot;
        slider.value = enemy.GetComponent<EnemyLifeInformation>().CurrentLife * 0.01f;
    }
}
