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
    float originalJumpPower;
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
                particleSystem.Play();
                StartCoroutine(RevertJumpBoost(originalJumpPower, 3f));
                PlayerStats._stats.JumpPower = originalJumpPower*1.8f;
                break;
            case BlockType.SpeedBoost:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case BlockType.Floating:
                gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
            default:
                break;
        }
    }



    private IEnumerator RevertJumpBoost(float originalJumpPower, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerStats._stats.JumpPower = originalJumpPower;
        print($"Power set to: {originalJumpPower}");
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
                return Color.cyan;
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
    }

    // Update is called once per frame
    void Update()
    {
        SetColor();
        
    }
}
