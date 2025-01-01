using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSoundEffect : MonoBehaviour
{
    public AudioClip playerNormalAttackSE;
    private AudioSource aS;
    private bool normalAttackSE;

    public bool NormalAttackSE { get => normalAttackSE; set => normalAttackSE = value; }

    // Start is called before the first frame update
    void Start()
    {
        aS = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //normal attack se
        if (normalAttackSE)
        {
            aS.clip = playerNormalAttackSE;
            aS.volume = 8f;
            aS.Play();
            normalAttackSE = false;
        }
    }
}
