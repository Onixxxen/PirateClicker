using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CoinCollectorView : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsCount;
    [SerializeField] private TMP_Text _addTextTemplate;
    [SerializeField] private GameObject _addTextContainer;
    [SerializeField] private AudioSource _sound;

    private float _normalScale;

    public TMP_Text CoinsCount => _coinsCount;

    public event Action TryGetCoinsCount;

    private void Start()
    {
        _normalScale = transform.localScale.x;
        TryGetCoinsCount?.Invoke();
    }

    public void ChangeCoinsView(float count)
    {
        decimal coinsCount = Math.Ceiling((decimal)count);
        _coinsCount.text = $"{FormatNumberExtension.FormatNumber((float)coinsCount)}";
        StartCoroutine(ChangeCoinsTextScale());
    }

    public void RequestAddCoinText(float perClick)
    {
        _sound.Play();

        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        var text = Instantiate(_addTextTemplate, worldPosition, Quaternion.identity, _addTextContainer.transform);
        text.text = $"+{FormatNumberExtension.FormatNumber(perClick)}";

        StartCoroutine(DestroyAddText(text));
    }

    private IEnumerator ChangeCoinsTextScale()
    {
        transform.DOScale(_normalScale + 0.2f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        transform.DOScale(_normalScale, 0.5f);
    }

    private IEnumerator DestroyAddText(TMP_Text text)
    {
        text.transform.DOMoveY(text.transform.position.y + 1, 1);
        text.DOColor(new Color(text.color.r, text.color.g, text.color.b, 0), 1);
        yield return new WaitUntil(() => text.color.a <= 0.1f);
        Destroy(text.gameObject);
    }
}
