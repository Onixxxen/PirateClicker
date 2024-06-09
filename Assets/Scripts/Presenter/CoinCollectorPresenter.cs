public class CoinCollectorPresenter
{
    private CoinCollector _coinCollector;
    private CoinCollectorView _coinCollectorView;
    private Coin _coin;
    private CharacterView _characterView;

    public void Init(CoinCollectorView coinView, CharacterView characterView, CoinCollector coinCollector, Coin coin)
    {
        _coinCollectorView = coinView;
        _characterView = characterView;
        _coinCollector = coinCollector;
        _coin = coin;
    }

    public void Enable()
    {
        _characterView.OnCharacterClick += _coinCollector.CollectCoin;
        _coinCollectorView.TryGetCoinsCount += RequestCoinCount;

        _coinCollector.CoinsCountChanged += OnChangeCoins;
        _coin.GetCoinsCount += OnGetCoinsCount;
    }

    public void Disable()
    {
        _characterView.OnCharacterClick -= _coinCollector.CollectCoin;
        _coinCollectorView.TryGetCoinsCount -= RequestCoinCount;

        _coinCollector.CoinsCountChanged -= OnChangeCoins;
    }

    private void OnChangeCoins(float count, float perClick)
    {
        _coinCollectorView.ChangeCoinsView(count);
        _coinCollectorView.RequestAddCoinText(perClick);
    }

    private void RequestCoinCount()
    {
        _coin.CoinsCountRequest();
    }

    private void OnGetCoinsCount(float count)
    {
        _coinCollectorView.ChangeCoinsView(count);
    }
}
