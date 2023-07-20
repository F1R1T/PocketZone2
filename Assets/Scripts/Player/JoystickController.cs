using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public float dirX, dirY;
    public float speed;
    public Joystick joystick;
    private Rigidbody2D rigidBody2D;
    private Inventory inventory; // Добавленное поле для ссылки на компонент Inventory

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>(); // Получение ссылки на компонент Inventory
    }

    void Update()
    {
        dirX = joystick.Horizontal * speed;
        dirY = joystick.Vertical * speed;
    }

    private void FixedUpdate()
    {
        rigidBody2D.velocity = new Vector2(dirX, dirY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            ItemController itemController = collision.GetComponent<ItemController>();
            if (itemController != null && inventory != null) // Проверка наличия ссылки на Inventory
            {
                inventory.AddItemToInventory(itemController.itemId);
                Destroy(collision.gameObject); // Удаляем предмет из мира
            }
        }
    }
}
