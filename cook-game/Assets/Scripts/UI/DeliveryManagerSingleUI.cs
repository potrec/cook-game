using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    private DeliveryRecipe deliveryRecipe;
    
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    
    public void SetDeliveryRecipe(DeliveryRecipe deliveryRecipe)
    {
        this.deliveryRecipe = deliveryRecipe;
        recipeNameText.text = deliveryRecipe.recipeSO.recipeName;
        
        foreach (Transform child in iconContainer)
        {
            if(child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in deliveryRecipe.recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
        
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = deliveryRecipe.timeLeftToDeliver / deliveryRecipe.timeToDeliver
        });
    }
    
    private void Update()
    {
        if (deliveryRecipe == null) return;
        
        deliveryRecipe.timeLeftToDeliver -= Time.deltaTime;
        float progressNormalized = deliveryRecipe.timeLeftToDeliver / deliveryRecipe.timeToDeliver;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = progressNormalized
        });
    }
    
    private void OnDestroy()
    {
        OnProgressChanged = null;
    }
}
