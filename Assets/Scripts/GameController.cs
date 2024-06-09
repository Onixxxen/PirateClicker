using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CoinCollectorView _coinCollectorView;
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private SaverData _saverData;

    private CoinCollector _coinCollector;
    private CoinCollectorPresenter _coinCollectorPresenter;

    private ShopPresenter _shopPresenter;
    private List<ShopItemView> _shopItemView = new List<ShopItemView>();

    public Coin Coin { get; private set; }
    public Shop Shop { get; private set; }
    public List<ShopItemView> ShopItemView => _shopItemView;

    private void OnEnable()
    {
        _coinCollectorPresenter.Enable();

        if (_shopPresenter != null)
            _shopPresenter.Enable();
    }

    private void OnDisable()
    {
        _coinCollectorPresenter.Disable();

        if (_shopPresenter != null)
            _shopPresenter.Disable();
    }

    private void Awake()
    {
        Coin = new Coin(_saverData);
        Shop = new Shop(Coin, _saverData);
        _coinCollector = new CoinCollector(Coin);

        _coinCollectorPresenter = new CoinCollectorPresenter();
        _shopPresenter = new ShopPresenter();

        _coinCollectorPresenter.Init(_coinCollectorView, _characterView, _coinCollector, Coin);

        for (int i = 0; i < _shopView.SpawnedItem.Count; i++)
            _shopItemView.Add(_shopView.SpawnedItem[i]);

        _shopPresenter.Init(Shop, _coinCollectorView, _shopView, _shopItemView);

        _shopView.gameObject.SetActive(false);
    }
}
