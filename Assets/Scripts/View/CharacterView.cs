using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterView : MonoBehaviour
{
    private float _normalScale;

    public event Action OnCharacterClick;

    private void Awake()
    {
        _normalScale = transform.localScale.x;
        gameObject.GetComponent<Button>().onClick.AddListener(CharacterClick);
    }

    private void CharacterClick()
    {
        StartCoroutine(ChangeChangacterScale());
        OnCharacterClick?.Invoke();
    }

    private IEnumerator ChangeChangacterScale()
    {
        transform.DOScale(_normalScale - 0.2f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        transform.DOScale(_normalScale, 0.5f);
    }
}
