using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private GameObject player;
    private Slider slider;
    private Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        slider = transform.GetComponent<Slider>();
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = startRot;
        slider.value = player.GetComponent<PlayerLifeInformation>().CurrentLife * 0.01f;
    }
}
