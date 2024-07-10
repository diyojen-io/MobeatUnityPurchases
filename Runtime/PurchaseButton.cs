using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class PurchaseButton : MonoBehaviour
{
    public string productId;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnPurchaseButtonClicked);
    }

    void OnPurchaseButtonClicked()
    {
        if (CustomPurchaseListener.IsInitialized())
        {
            CustomPurchaseListener.storeController.InitiatePurchase(productId);
        }
        else
        {
            Debug.LogError("Purchase failed: Not initialized.");
        }
    }
}