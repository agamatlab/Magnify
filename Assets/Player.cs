using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;

    public void PlayParticle()
    {
        particleSystem.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.y< -10)
        {
        }
        
    }
}
