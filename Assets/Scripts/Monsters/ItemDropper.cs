using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public GameObject itemPrefab; // ������ ��������, ������� ����� ��������

    public void DropItem()
    {
        Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }
}

