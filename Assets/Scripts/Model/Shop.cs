using System;

public class Shop
{
    private float _price;
    private float _improvement;
    private Coin _coin;
    private SaverData _saverData;

    public event Action<float, float, float, int> SellShopItem;
    public event Action<int, float> GiveCoinsPerClick;
    public event Action<int> OpenItem;
    public event Action<int> CloseItem;
    public event Action<int> LockItem;
    public event Action<int> UnlockItem;

    public Shop(Coin coin, SaverData saverData)
    {
        _coin = coin;
        _saverData = saverData;
    }

    public void TrySellItem(int index, float price, float addCoinsPerClick)
    {
        _price = price;
        _improvement = addCoinsPerClick;
        double newPrice = Math.Ceiling(_price * 1.2f);

        if (_coin.Count >= _price)
        {
            _coin.RemoveCoin(_price);
            _coin.ChangeCoinsPerClick(_improvement);

            _saverData.SaveShopItemPrices(index, (float)newPrice);

            SellShopItem?.Invoke(_coin.Count, (float)newPrice, _coin.PerClick, index);
        }
    }

    public void CoinsPerClickRequest(int index)
    {
        GiveCoinsPerClick?.Invoke(index, _coin.PerClick);
    }

    public void OpenItemRequest(int index, float price)
    {
        _saverData.SaveShopOpenStatus(index, _coin.Count >= price);

        if (_coin.Count >= price)
        {
            OpenItem?.Invoke(index);
        }
    }

    public void CloseItemRequest(int index, float price)
    {
        if (_coin.Count < price)
            CloseItem?.Invoke(index);
    }

    public void LockItemRequest(int index, float price)
    {
        double newPrice = Math.Ceiling(price * 1.2f);

        if (_coin.Count < newPrice)
            LockItem?.Invoke(index);
    }

    public void UnlockItemRequest(int index, float price)
    {
        if (_coin.Count >= price)
            UnlockItem?.Invoke(index);
    }
}
