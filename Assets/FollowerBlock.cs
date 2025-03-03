using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowerBlock : MonoBehaviour
{
    int chosenBlock = 0;
    bool isColliding = false;

    [SerializeField] GameObject magneticBlock;
    [SerializeField] float newBlockRange;
    [SerializeField] Image[] inventoryImages;

    public Sprite JumpBoostSprite;
    public Sprite SpeedBoostSprite;
    public Sprite FloatingSprite;

    MagneticBlock.BlockType blockType;
    MagneticBlock.BlockType[] blockTypes = {
        MagneticBlock.BlockType.JumpBoost,
        MagneticBlock.BlockType.SpeedBoost,
        MagneticBlock.BlockType.Floating
    };

    Renderer rc => GetComponent<Renderer>();
    BoxCollider2D bc => GetComponent<BoxCollider2D>();
    private static GameObject player => GameObject.FindWithTag("Player");
    List<BoxCollider2D> collidingWith = new List<BoxCollider2D>();
    bool[] placedBlocks = new bool[3];


    // Start is called before the first frame update
    void Start()
    {
        rc.enabled = false;
    }

    void ChooseBlock(int blockId) {
        blockId = (chosenBlock == blockId) ? 0 : blockId;
        chosenBlock = blockId;
        if (blockId == 0)
        {
            rc.enabled = false;
            return;
        } else
        {
            rc.enabled = true;
            rc.material.color = MagneticBlock.GetColor(blockTypes[blockId - 1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RespondToButtons();
        if (chosenBlock != 0)
        {
            switch (blockTypes[chosenBlock-1])
            {
                case MagneticBlock.BlockType.North:
                    break;
                case MagneticBlock.BlockType.South:
                    break;
                case MagneticBlock.BlockType.JumpBoost:
                    gameObject.GetComponent<SpriteRenderer>().sprite = JumpBoostSprite;

                    break;
                case MagneticBlock.BlockType.SpeedBoost:
                    gameObject.GetComponent<SpriteRenderer>().sprite = SpeedBoostSprite;
                    break;
                case MagneticBlock.BlockType.Floating:
                    gameObject.GetComponent<SpriteRenderer>().sprite = FloatingSprite;
                    break;
                default:
                    break;
            }
            RespondToMouse();
        }
    }

    private void RespondToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 playerPosition = player.transform.position + (float)0.5 * Vector3.up;
        Vector3 delta = mousePosition - playerPosition;
        float magnitude = delta.magnitude;

        if (delta.magnitude > newBlockRange)
        {
            mousePosition = playerPosition + delta.normalized * newBlockRange;
        }
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition;
        if (Input.GetKeyUp(KeyCode.Mouse0) && !isColliding)
        {
            var block = Instantiate(magneticBlock);
            var sr = block.GetComponent<SpriteRenderer>();

            switch (blockTypes[chosenBlock - 1])
            {
                case MagneticBlock.BlockType.North:
                    break;
                case MagneticBlock.BlockType.South:
                    break;
                case MagneticBlock.BlockType.JumpBoost:
                    sr.sprite = JumpBoostSprite;
                    break;
                case MagneticBlock.BlockType.SpeedBoost:
                    sr.sprite = SpeedBoostSprite;
                    break;
                case MagneticBlock.BlockType.Floating:
                    sr.sprite = FloatingSprite;
                    break;
                default:
                    break;
            }

            block.transform.position = transform.position;
            block.GetComponent<MagneticBlock>().type = blockTypes[chosenBlock - 1];
            //placedBlocks[chosenBlock - 1] = true; // Mark block as placed
            //inventoryImages[chosenBlock - 1].gameObject.SetActive(false);
            ChooseBlock(0);
        }
    }

    private void RespondToButtons()
    {
        if (Input.GetKeyUp("1"))
        {
            ChooseBlock(1);
        }
        if (Input.GetKeyUp("2"))
        {
            ChooseBlock(2);
        }
        if (Input.GetKeyUp("3"))
        {
            ChooseBlock(3);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isColliding= true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
    }


}
