using System.Collections.Generic;

public class ShopPresenter
{
    private CoinCollectorView _coinCollectorView;

    private Shop _shop;
    private ShopView _shopView;
    private List<ShopItemView> _shopItemView = new List<ShopItemView>();

    public void Init(Shop shop, CoinCollectorView coinCollectorView, ShopView shopView, List<ShopItemView> shopItemView)
    {
        _shop = shop;
        _coinCollectorView = coinCollectorView;
        _shopView = shopView;

        for (int i = 0; i < shopItemView.Count; i++)
            _shopItemView.Add(shopItemView[i]);
    }

    public void Enable()
    {
        _shopView.OnSellButtonClick += TrySell;
        _shopView.OnRequsetCoinsPerClick += RequestCoinsPerClick;
        _shopView.OnRequestOpenItem += RequestOpenItem;
        _shopView.OnRequestCloseItem += RequestCloseItem;
        _shopView.OnRequsetLockItem += RequestLockItem;
        _shopView.OnRequsetUnlockItem += RequestUnlockItem;

        _shop.SellShopItem += OnBuying;
        _shop.GiveCoinsPerClick += OnGiveCoinsPerClick;
        _shop.OpenItem += OnOpenItem;
        _shop.CloseItem += OnCloseItem;
        _shop.LockItem += OnLockItem;
        _shop.UnlockItem += OnUnlockItem;
    }

    public void Disable()
    {
        _shopView.OnSellButtonClick -= TrySell;
        _shopView.OnRequsetCoinsPerClick -= RequestCoinsPerClick;
        _shopView.OnRequestOpenItem -= RequestOpenItem;
        _shopView.OnRequestCloseItem -= RequestCloseItem;
        _shopView.OnRequsetLockItem -= RequestLockItem;
        _shopView.OnRequsetUnlockItem -= RequestUnlockItem;

        _shop.SellShopItem -= OnBuying;
        _shop.GiveCoinsPerClick -= OnGiveCoinsPerClick;
        _shop.OpenItem -= OnOpenItem;
        _shop.CloseItem -= OnCloseItem;
        _shop.LockItem -= OnLockItem;
        _shop.UnlockItem -= OnUnlockItem;
    }

    public void TrySell(int index, float price, float addCoinsPerClick)
    {
        _shop.TrySellItem(index, price, addCoinsPerClick);
    }

    public void OnBuying(float newCoinsCount, float newPrice, float newCoinsPerClick, int index)
    {
        _coinCollectorView.ChangeCoinsView(newCoinsCount);
        _shopItemView[index].UpdateValues(newPrice);
        _shopView.UpdateCoinsPerClick(newCoinsPerClick);
    }

    public void RequestCoinsPerClick(int index)
    {
        _shop.CoinsPerClickRequest(index);
    }

    public void OnGiveCoinsPerClick(int index, float coinsPerClick)
    {
        _shopView.SetCoinsPerClick(coinsPerClick);
    }

    public void RequestOpenItem(int index, float price)
    {
        _shop.OpenItemRequest(index, price);
    }

    public void OnOpenItem(int index)
    {
        _shopItemView[index].OpenItem();
    }

    public void RequestCloseItem(int index, float price)
    {
        _shop.CloseItemRequest(index, price);
    }

    public void OnCloseItem(int index)
    {
        _shopItemView[index].CloseItem();
    }

    public void RequestLockItem(int index, float price)
    {
        _shop.LockItemRequest(index, price);
    }

    public void OnLockItem(int index)
    {
        _shopItemView[index].LockItem();
    }

    public void RequestUnlockItem(int index, float price)
    {
        _shop.UnlockItemRequest(index, price);
    }

    public void OnUnlockItem(int index)
    {
        _shopItemView[index].UnlockItem();
    }
}
