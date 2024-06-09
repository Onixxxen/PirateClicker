using UnityEngine;

public class SaverData : MonoBehaviour
{
    [SerializeField] private ShopView _shopView;
    [SerializeField] private GameController _gameController;

    public float SavedCoins = 0;
    public float SavedCoinsPerClick = 1;

    public float[] SavedShopItemPrices = new float[12];
    public bool[] SavedShopItemOpenStatus = new bool[12];

    public void SaveCoinsCount(float coinsCount)
    {
        SavedCoins = coinsCount;
        PlayerPrefs.SetFloat("coinsCount", SavedCoins);
        PlayerPrefs.Save();
    }

    public void SaveCoinsPerClick(float perClick)
    {
        SavedCoinsPerClick = perClick;
        PlayerPrefs.SetFloat("perClick", SavedCoinsPerClick);
        PlayerPrefs.Save();
    }

    public void SaveShopItemPrices(int index, float price)
    {
        SavedShopItemPrices[index] = price;
        PlayerPrefs.SetFloat("shopItemPrice" + index, SavedShopItemPrices[index]);
        PlayerPrefs.Save();
    }

    public void SaveShopOpenStatus(int index, bool status)
    {
        if (status == false)
        {
            SavedShopItemOpenStatus[index] = false;
            PlayerPrefs.SetInt("shopItemOpenStatus" + index, SavedShopItemOpenStatus[index] == false ? 0 : 1);
        }
        else if (status == true)
        {
            SavedShopItemOpenStatus[index] = true;
            PlayerPrefs.SetInt("shopItemOpenStatus" + index, SavedShopItemOpenStatus[index] == true ? 1 : 0);
        }

        PlayerPrefs.Save();
    }

    private void Start()
    {
        GetLoad();
    }

    public void GetLoad()
    {
        if (PlayerPrefs.HasKey("coinsCount"))
            SavedCoins = PlayerPrefs.GetFloat("coinsCount", SavedCoins);

        if (PlayerPrefs.HasKey("perClick"))
            SavedCoinsPerClick = PlayerPrefs.GetFloat("perClick", SavedCoinsPerClick);

        for (int i = 0; i < SavedShopItemPrices.Length; i++)
            if (PlayerPrefs.HasKey("shopItemPrice" + i))
                SavedShopItemPrices[i] = PlayerPrefs.GetFloat("shopItemPrice" + i, SavedShopItemPrices[i]);

        for (int i = 0; i < SavedShopItemOpenStatus.Length; i++)
            if (PlayerPrefs.HasKey("shopItemOpenStatus" + i))
                SavedShopItemOpenStatus[i] = PlayerPrefs.GetInt("shopItemOpenStatus" + i, SavedShopItemOpenStatus[i] == false ? 0 : 1) == 0 ? false : true;

        for (int i = 0; i < _shopView.SpawnedItem.Count; i++)
            if (SavedShopItemPrices[i] != 0)
                _shopView.SpawnedItem[i].LoadShopItemPriceData();

        _gameController.Coin.LoadData();
    }

    public void ResetSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}
