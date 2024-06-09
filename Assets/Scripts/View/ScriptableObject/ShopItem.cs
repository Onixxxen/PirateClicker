using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "ShopItem/Create Shop Item")]
public class ShopItem : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public float Price;
    public float AddCoinsPerClick;
}
