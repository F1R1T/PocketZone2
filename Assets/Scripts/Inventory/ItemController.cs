using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item itemId; // Идентификатор предмета

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Inventory inventory = collision.GetComponent<Inventory>();
            if (inventory != null && inventory.enabled)
            {
                inventory.AddItemToInventory(itemId);
                Destroy(collision.gameObject); // Удаляем предмет из мира
            }
        }
    }
}
