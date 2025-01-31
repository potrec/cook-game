using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;
    [SerializeField] private Transform counterTopPoint;
    
    private KitchenObject kitchenObject;
    
    public virtual void Interact(Player player)
    {
        Debug.LogError("Interact not implemented");
    }
    
    public virtual void InteractAlternate(Player player)
    {
        Debug.LogError("InteractAlternate not implemented");
    }
    
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        
        if(kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }
}
