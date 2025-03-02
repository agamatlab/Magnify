using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBlock : MonoBehaviour
{
    int chosenBlock = 0;
    bool isColliding = false;

    [SerializeField] GameObject magneticBlock;
    [SerializeField] float newBlockRange;

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


    // Start is called before the first frame update
    void Start()
    {
        rc.enabled = false;
    }

    void ChooseBlock(int blockId) {
        chosenBlock = blockId;
        rc.enabled = true;
        rc.material.color = MagneticBlock.GetColor(blockTypes[blockId - 1]);
    }

    // Update is called once per frame
    void Update()
    {
        RespondToButtons();
        if (chosenBlock != 0)
        {
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
            block.transform.position = transform.position;
            block.GetComponent<MagneticBlock>().type = blockTypes[chosenBlock - 1];
            chosenBlock = 0;
            rc.enabled = false;
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
