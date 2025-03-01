using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagneticBlock : MonoBehaviour
{
    public enum BlockType
    {
        North, South
    };

    public BlockType type;


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
        }
    }
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        SetColor();
        
    }
}
