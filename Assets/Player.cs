using System.Collections;
using System.Collections.Generic;
using TarodevController;
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
        var PlayerStats = gameObject.GetComponent<PlayerController>();
        PlayerStats._stats.MaxSpeed = 42;
        PlayerStats._stats.Acceleration= 30;
        PlayerStats._stats.JumpPower = 25;

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
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
