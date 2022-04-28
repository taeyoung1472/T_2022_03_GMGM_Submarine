using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class VirtualTarget : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    bool isCanSearch = true;
    public void Set(Vector3 pos, Sprite sprite, Action<GameObject> callback)
    {
        if (isCanSearch)
        {
            isCanSearch = false;
            spriteRenderer.sprite = sprite;
            transform.localPosition = pos;
            Sequence seq = DOTween.Sequence();
            seq.Append(spriteRenderer.DOFade(0, 1).SetEase(Ease.InOutQuint));
            seq.Append(spriteRenderer.DOFade(1, 0));
            seq.AppendCallback(() =>
            {
                isCanSearch = true;
                callback(gameObject);
            });
        }
    }
}