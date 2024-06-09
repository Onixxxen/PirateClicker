using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopView : MonoBehaviour
{
    [SerializeField] protected TMP_Text CurrentValue;
    [SerializeField] protected GameObject _container;
    [SerializeField] private List<ShopItem> _shopItems;
    [SerializeField] private ShopItemView _template;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private TMP_Text _perClickText;
    [SerializeField] private AudioSource _sound;

    public List<ShopItem> ShopItems => _shopItems;
    public List<ShopItemView> SpawnedItem { get; private set; } = new List<ShopItemView>();

    public event Action<int, float, float> OnSellButtonClick;
    public event Action<int> OnRequsetCoinsPerClick;
    public event Action<int, float> OnRequestOpenItem;
    public event Action<int, float> OnRequestCloseItem;
    public event Action<int, float> OnRequsetLockItem;
    public event Action<int, float> OnRequsetUnlockItem;

    private void Awake()
    {
        for (int i = 0; i < _shopItems.Count; i++)
            AddItem(_shopItems[i], i);
    }

    private void AddItem(ShopItem shopItem, int index)
    {
        var item = Instantiate(_template, _container.transform);

        item.OnShopSellButton += TrySellItem;
        item.TryGetCoinsPerClick += TryRequestCoinsPerClick;
        item.TryOpenItem += TryRequestOpenItem;
        item.TryCloseItem += TryRequestCloseItem;
        item.TryLockItem += TryRequestLockItem;
        item.TryUnlockItem += TryRequestUnlockItem;

        item.Render(shopItem, index, _sound);

        SpawnedItem.Add(item);
    }

    public void UpdateCoinsPerClick(float coinsPerClick)
    {
        _count.text = $"{coinsPerClick}";
        CurrentValue.text = $"{FormatNumberExtension.FormatNumber(coinsPerClick)}{_perClickText.text}";
    }

    public void SetCoinsPerClick(float coinsPerClick)
    {
        _count.text = $"{coinsPerClick}";
        CurrentValue.text = $"{FormatNumberExtension.FormatNumber(coinsPerClick)}{_perClickText.text}";
    }

    private void TrySellItem(int index, float price, float addCoinsPerClick)
    {
        OnSellButtonClick?.Invoke(index, price, addCoinsPerClick);
    }

    private void TryRequestCoinsPerClick(int index)
    {
        OnRequsetCoinsPerClick?.Invoke(index);
    }

    private void TryRequestOpenItem(int index, float price)
    {
        OnRequestOpenItem?.Invoke(index, price);
    }

    private void TryRequestCloseItem(int index, float price)
    {
        OnRequestCloseItem?.Invoke(index, price);
    }

    private void TryRequestLockItem(int index, float price)
    {
        OnRequsetLockItem?.Invoke(index, price);
    }

    private void TryRequestUnlockItem(int index, float price)
    {
        OnRequsetUnlockItem?.Invoke(index, price);
    }    
}
