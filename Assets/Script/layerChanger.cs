using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layerChanger : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] sprites; 
    [SerializeField] int layerMutiplier = 0;
    public int LayerMutiplier =>layerMutiplier;
    // Start is called before the first frame update
    void Start()
    {
        ReRangeLayerOrder();
    }

    private void ReRangeLayerOrder()
    {
        int minLayer;
        sprites = transform.GetComponentsInChildren<SpriteRenderer>();
        minLayer = sprites[0].sortingOrder;
        foreach (var sprite in sprites)
        {
            int currentLayer = sprite.sortingOrder;
            minLayer = Math.Min(minLayer, currentLayer);
        }
        int offsetLayer = 1 - minLayer;
        foreach (var sprite in sprites)
        {
            sprite.sortingOrder += offsetLayer;
        }
    }
    public void ChangeToTopOf(layerChanger layerChanger){
        int originMutiplier = layerMutiplier;
        int additionMutplier = originMutiplier - (layerChanger.LayerMutiplier+20);
        layerMutiplier = layerChanger.LayerMutiplier+20;
        foreach (var sprite in sprites)
        {
            sprite.sortingOrder += additionMutplier;
        }
    }
    public void ChangeToButtomOf(layerChanger layerChanger){
        
    }
}
