using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using Unity.VisualScripting;
using UnityEngine;

public class MagneticBlock : MonoBehaviour
{
    GameObject Player;
    PlayerController PlayerStats;
    private ParticleSystem particleSystem;
    [SerializeField] float timeToRevert = 3;
    [SerializeField] float speedFactor = 3;
    [SerializeField] float jumpFactor = 1.8f;

    float originalJumpPower;
    float originalMaxSpeed;
    float originalAcceleration;
    bool isJumpBoosted = false;
    bool isSpeedBoosted = false;
    public enum BlockType
    {
        North, South, JumpBoost, SpeedBoost, Floating
    };

    public BlockType type;

    private void Affect()
    {
        switch (type)
        {
            case BlockType.JumpBoost:
                if(!isJumpBoosted)
                {
                    isJumpBoosted = true;
                    particleSystem.Play();
                    StartCoroutine(RevertJumpBoost());
                    PlayerStats._stats.JumpPower = originalJumpPower * jumpFactor;
                }
                break;
            case BlockType.SpeedBoost:
                if(!isSpeedBoosted)
                {
                    isSpeedBoosted = true;
                    StartCoroutine(RevertSpeedBoost());
                    PlayerStats._stats.MaxSpeed = originalMaxSpeed * speedFactor;
                    PlayerStats._stats.Acceleration *= originalAcceleration * speedFactor;
                }
                PlayerStats._stats.Acceleration *= speedFactor;
                break;
            case BlockType.Floating:
                gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
            default:
                break;
        }
    }



    private IEnumerator RevertJumpBoost()
    {
        yield return new WaitForSeconds(timeToRevert);
        PlayerStats._stats.JumpPower = originalJumpPower;
    }

    private IEnumerator RevertSpeedBoost()
    {
        yield return new WaitForSeconds(timeToRevert);
        PlayerStats._stats.MaxSpeed = originalMaxSpeed;
        PlayerStats._stats.Acceleration = originalAcceleration;
        isSpeedBoosted = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get all contact points of the collision
            ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
            collision.GetContacts(contacts);

            foreach (ContactPoint2D contact in contacts)
            {
                if (contact.normal.y < -0.5f) // Collision on Top
                {
                    Affect();
                }
            }
        }
    }

    static public Color GetColor(BlockType type)
    {
        switch (type)
        {
            case BlockType.North:
                return Color.blue;
            case BlockType.South:
                return  Color.red;
            case BlockType.JumpBoost:
                return Color.green;
            case BlockType.SpeedBoost:
                return Color.blue;
            case BlockType.Floating:
                return Color.yellow;
            default:
                return Color.black;
        }
    }


    void SetColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = GetColor(type);
    }
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        Player = GameObject.FindWithTag("Player");
        PlayerStats = Player.GetComponent<PlayerController>();
        originalJumpPower = PlayerStats._stats.JumpPower;
        originalMaxSpeed = PlayerStats._stats.MaxSpeed;
        originalAcceleration = PlayerStats._stats.Acceleration;
        if (type == BlockType.Floating) {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetColor();
    }
}
