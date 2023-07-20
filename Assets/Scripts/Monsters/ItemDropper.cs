using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public GameObject itemPrefab; // Префаб предмета, который будет выпадать

    public void DropItem()
    {
        Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }
}

