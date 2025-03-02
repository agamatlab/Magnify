using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image[] inventoryImages; 
    public GameObject[] blocks;

    private Dictionary<int, bool> placedItems = new Dictionary<int, bool>();

    // Start is called before the first frame update
    void Start()
    {
         for (int i = 0; i < blocks.Length; i++)
        {
            placedItems[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) PlaceItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) PlaceItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) PlaceItem(2);
    }

    void PlaceItem(int index)
    {
        if (index >= blocks.Length || placedItems[index]) return;

        placedItems[index] = true;
        inventoryImages[index].gameObject.SetActive(false); 
    }
}
