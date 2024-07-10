using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Purchasing;

public class CustomPurchaseListener : AbstractPurchaseListener
{
    public List<ProductConfig> productConfigs;

    void Start()
    {
        // Initialize purchasing with dynamic product configurations
        InitializePurchasing(productConfigs);
    }

    public override PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // Custom logic for processing purchases
        Debug.Log($"Purchase Successful: {args.purchasedProduct.definition.id}");
        return base.ProcessPurchase(args);
    }
}