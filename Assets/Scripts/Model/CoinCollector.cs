using System;

public class CoinCollector
{
    private Coin _coin;

    public event Action<float, float> CoinsCountChanged;

    public CoinCollector(Coin coin)
    {
        _coin = coin;
    }

    public void CollectCoin()
    {
        _coin.AddCoin();
        CoinsCountChanged?.Invoke(_coin.Count, _coin.PerClick);
    }
}
