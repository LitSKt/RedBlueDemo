using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSoundEffect : MonoBehaviour
{
    public AudioClip enemyNormalAttackSE;
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
            aS.clip = enemyNormalAttackSE;
            aS.volume = 1f;
            aS.Play();
            normalAttackSE = false;
        }
    }
}
