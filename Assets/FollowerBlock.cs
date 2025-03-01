using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBlock : MonoBehaviour
{
    int chosenBlock = 0;
    MagneticBlock.BlockType blockType;
    MagneticBlock.BlockType[] blockTypes = {
        MagneticBlock.BlockType.North,
        MagneticBlock.BlockType.South,
        MagneticBlock.BlockType.JumpBoost
    };

    Renderer rc() {
        return GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rc().enabled = false;
    }

    void ChooseBlock(int blockId) {
        chosenBlock = blockId;
        rc().enabled = true;
        rc().material.color = MagneticBlock.GetColor(blockTypes[blockId - 1]);
    }

    // Update is called once per frame
    void Update()
    {
        if(chosenBlock==0)
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
        } else {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            transform.position = mousePosition;
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                chosenBlock = 0;
                rc().enabled = false;
            }
        }
    }
}
