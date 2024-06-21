using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]

public class ItemSOtemp : ScriptableObject
{
    public string itemName;
    public int price;
    public int weight;
    public GameObject PreFab;
}