using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataInventory : MonoBehaviour
{
    public List<Item> items;

}

[System.Serializable]

public class Item
{
    public int id;
    public string name;
    public Sprite image;
    
}
