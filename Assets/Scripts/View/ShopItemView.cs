using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemView : MonoBehaviour
{
    private int _index;

    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _improvement;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _closePrice;
    [SerializeField] private Button _sellButton;
    [SerializeField] private GameObject _closePanel;
    [SerializeField] private GameObject _lockPanel;
    private AudioSource _sound;
    private SaverData _saverData;

    private float _priceValue;
    private float _improvementValue;

    private float _price;
    private ShopItemView[] _items;

    public event Action<int> TryGetCoinsPerClick;
    public event Action<int, float, float> OnShopSellButton;

    public event Action<int, float> TryOpenItem;
    public event Action<int, float> TryCloseItem;
    public event Action<int, float> TryLockItem;
    public event Action<int, float> TryUnlockItem;

    private void Awake()
    {
        _saverData = FindObjectOfType<SaverData>();
    }

    private void OnEnable()
    {
        _price = _priceValue;

        TryGetCoinsPerClick?.Invoke(_index);

        if (_closePanel.activeSelf == true)
            TryOpenItem?.Invoke(_index, _price);

        if (_lockPanel.activeSelf == true)
            TryUnlockItem?.Invoke(_index, _price);
    }

    private void Start()
    {
        _price = _priceValue;
        _items = FindObjectsOfType<ShopItemView>(true);

        TryGetCoinsPerClick?.Invoke(_index);
        TryOpenItem?.Invoke(_index, _price);

        _sellButton.onClick.AddListener(SellItem);
    }

    public void SellItem()
    {
        _price = _priceValue;

        float addCoinsPerClick = _improvementValue;

        OnShopSellButton?.Invoke(_index, _price, addCoinsPerClick);
        _sound.Play();

        for (int i = 0; i < _items.Length; i++)
            _items[i].TryLockItem?.Invoke(_items[i]._index, _items[i]._price);
    }

    public void Render(ShopItem item, int index, AudioSource sound)
    {
        _name.text = item.Name;
        _index = index;
        _icon.sprite = item.Icon;
        _improvement.text = $"+{FormatNumberExtension.FormatNumber(item.AddCoinsPerClick)}/клик";
        _priceText.text = $"{FormatNumberExtension.FormatNumber(item.Price)}";
        _closePrice.text = $"{FormatNumberExtension.FormatNumber(item.Price)}";
        _priceValue = item.Price;
        _improvementValue = item.AddCoinsPerClick;
        _sound = sound;
    }

    public void UpdateValues(float price)
    {
        _priceValue = price;
        _priceText.text = $"{FormatNumberExtension.FormatNumber(price)}";
        _closePrice.text = $"{FormatNumberExtension.FormatNumber(price)}";
    }

    public void OpenItem()
    {
        _closePanel.gameObject.SetActive(false);
    }

    public void RequestCloseItem()
    {
        if (_closePanel.activeSelf == false)
            TryCloseItem?.Invoke(_index, _price);
    }

    public void CloseItem()
    {
        _closePanel.gameObject.SetActive(true);
    }

    public void LockItem()
    {
        _lockPanel.gameObject.SetActive(true);
    }

    public void UnlockItem()
    {
        _lockPanel.gameObject.SetActive(false);
    }

    public void LoadShopItemPriceData()
    {
        _priceValue = _saverData.SavedShopItemPrices[_index];
        _closePanel.gameObject.SetActive(!_saverData.SavedShopItemOpenStatus[_index]);

        UpdateValues(_priceValue);
    }
}
