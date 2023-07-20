using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPlane;

    public void ToggleInventoryVisibility()
    {
        inventoryPlane.SetActive(!inventoryPlane.activeSelf);
    }
}