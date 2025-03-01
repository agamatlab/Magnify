using System.Collections;
using System.Collections.Generic;
using TarodevController;
using Unity.VisualScripting;
using UnityEngine;

public class MagneticBlock : MonoBehaviour
{
    GameObject Player;
    PlayerController PlayerStats;
    public ScriptableStats OriginalStarts;
    private ParticleSystem particleSystem;

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
                RevertJumpBoost(PlayerStats._stats.JumpPower, 3f);
                PlayerStats._stats.JumpPower = 36;
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

    public Color GetColor(BlockType type)
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
        switch (type)
        {
            case BlockType.North:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case BlockType.South:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case BlockType.JumpBoost:
                particleSystem.startColor = Color.green;
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case BlockType.SpeedBoost:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case BlockType.Floating:
                gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        Player = GameObject.FindWithTag("Player");
        PlayerStats = Player.GetComponent<PlayerController>();
            
    }

    // Update is called once per frame
    void Update()
    {
        SetColor();
        
    }
}
