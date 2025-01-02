using UnityEngine;
using UnityEngine.Serialization;

public class ClearCounter : MonoBehaviour
{
    
    [FormerlySerializedAs("kitchenObjectSo")] [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    public void Interact()
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;
        string objectName = kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName = kitchenObjectSO.objectName;
        Debug.Log("You have placed a " + objectName);
    }
}
