using UnityEngine;

public class ScrollBag : MonoBehaviour
{
    [SerializeField] LayerMask bagItemLayers;
    [SerializeField] Transform itemStartTransform;
    [SerializeField, Header("Sets dynamically")] bool _canPlaceItem;

    void OnTriggerStay(Collider other)
    {
        if (!Utils.IsInLayerMask(other.gameObject, bagItemLayers)) return;
        _canPlaceItem = true;
    }

    public bool CanPlaceItem
    {
        get => _canPlaceItem;
        set => _canPlaceItem = value;        
    }

    public void SetCanPlaceItem(bool newValue) => _canPlaceItem = newValue;

    public void SetItemBackToStart(GameObject item)
    {
        item.transform.position = itemStartTransform.transform.position;
        item.transform.rotation = itemStartTransform.transform.rotation;
        _canPlaceItem = false;
    }
} 
