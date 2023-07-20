using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public DataInventory data;

    public List<ItemInventory> items = new List<ItemInventory>();
    public GameObject gameObjectShow;
    public GameObject inventoryMainObject;
    public int maxCount;

    public Camera camera;

    public EventSystem eventSystem;

    public int currentId;
    public ItemInventory currentItem;

    public Vector3 offset;

    [SerializeField] private Button deleteButton;



    public void Start()
    {
        if (items.Count == 0)
        {
            AddGraphics();
        }

        UpdateInventory();
    }

    public void Update()
    {

    }

    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObject.GetComponent<Image>().sprite = item.image;

        if (count > 1 && item.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddInventoryItem(int id, ItemInventory inventoryItem)
    {
        items[id].id = inventoryItem.id;
        items[id].count = inventoryItem.count;
        items[id].itemGameObject.GetComponent<Image>().sprite = data.items[inventoryItem.id].image;

        if (inventoryItem.count > 1 && inventoryItem.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = inventoryItem.count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjectShow, inventoryMainObject.transform) as GameObject;
            newItem.name = i.ToString();

            ItemInventory inventory = new ItemInventory();
            inventory.itemGameObject = newItem;

            RectTransform rectTransform = newItem.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);
            rectTransform.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();
            tempButton.onClick.AddListener(() => OnInventoryItemClick(newItem));


            items.Add(inventory);
        }
    }
    private void OnInventoryItemClick(GameObject itemGameObject)
    {
        int itemIndex = int.Parse(itemGameObject.name);

        if (items[itemIndex].id == 0)
        {
            // Если индекс предмета равен 0, кнопка удаления не появляется
            deleteButton.gameObject.SetActive(false);
        }
        else
        {
            // Активировать кнопку удаления
            deleteButton.gameObject.SetActive(true);

            // Установить текущий выбранный предмет
            currentId = itemIndex;
            currentItem = CopyInventoryItem(items[itemIndex]);
        }
    }

    public void OnDeleteButtonClick()
    {
        // Удалить предмет из инвентаря
        items[currentId].id = 0;
        items[currentId].count = 0;
        items[currentId].itemGameObject.GetComponent<Image>().sprite = null;
        items[currentId].itemGameObject.GetComponentInChildren<Text>().text = "";

        // Деактивировать кнопку удаления
        deleteButton.gameObject.SetActive(false);

        // Обновить графику инвентаря
        UpdateInventory();
    }
    public void UpdateInventory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id != 0 && items[i].count > 1)
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = "";
            }
            items[i].itemGameObject.GetComponent<Image>().sprite = data.items[items[i].id].image;
        }
    }
    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();

        New.id = old.id;
        New.itemGameObject = old.itemGameObject;
        New.count = old.count;

        return New;
    }

    public void AddItemToInventory(Item item)
    {
        // Найдем свободный слот в инвентаре
        int existingSlotIndex = FindExistingSlot(item.id);
        if (existingSlotIndex != -1)
        {
            // Увеличим количество предмета в существующем слоте
            items[existingSlotIndex].count += 1;
        }
        else
        {
            // Найдем свободный слот в инвентаре
            int emptySlotIndex = FindEmptySlot();
            if (emptySlotIndex != -1)
            {
                // Добавим предмет в инвентарь
                AddItem(emptySlotIndex, item, 1);
            }
        }

        UpdateInventory(); // Обновляем графику инвентаря
    }

    private int FindExistingSlot(int itemId)
    {
        // Ищем слот с предметом, имеющим указанный ID
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == itemId && items[i].count < 64)
            {
                return i;
            }
        }
        return -1; // Возвращаем -1, если нет слотов с предметом
    }

    private int FindEmptySlot()
    {
        // Ищем первый свободный слот в инвентаре
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == 0)
            {
                return i;
            }
        }
        return -1; // Возвращаем -1, если нет свободных слотов
    }


}

[System.Serializable]
public class ItemInventory
{
    public int id;
    public GameObject itemGameObject;
    public int count;
}