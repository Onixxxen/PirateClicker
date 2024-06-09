using System;

public class Coin
{
    private float _count;
    private float _perClick;
    private SaverData _saverData;

    public float Count => _count;
    public float PerClick => _perClick;

    public event Action<float> GetCoinsCount;

    public Coin(SaverData saverData)
    {
        _saverData = saverData;
    }     

    public void AddCoin()
    {
        _count += _perClick;
        _saverData.SaveCoinsCount(_count);
    }

    public void RemoveCoin(float count)
    {
        _count -= count;
        _saverData.SaveCoinsCount(_count);
    }

    public void CoinsCountRequest()
    {
        GetCoinsCount?.Invoke(_count);
    }

    public void ChangeCoinsPerClick(float count)
    {
        _perClick += count;
        _saverData.SaveCoinsPerClick(_perClick);
    }

    public void LoadData()
    {
        _count = _saverData.SavedCoins;
        _perClick = _saverData.SavedCoinsPerClick;
        CoinsCountRequest();
    }
}
