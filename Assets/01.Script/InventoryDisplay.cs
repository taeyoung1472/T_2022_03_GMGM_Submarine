using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private TextMesh amountTM;
    [SerializeField] private TextMesh weightTM;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void Set(Sprite sprite, int amount, int weight)
    {
        spriteRenderer.sprite = sprite;
        amountTM.text = amount != 0 ? $"{amount}°³" : "";
        weightTM.text = amount != 0 ? $"{amount * weight}KG" : "";
    }
}