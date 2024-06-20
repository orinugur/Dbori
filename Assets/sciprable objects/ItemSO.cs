using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]

public class ItemSO : ScriptableObject
{
    public string itemName;
    public int price;
    public int weight;
    public GameObject PreFab;
}