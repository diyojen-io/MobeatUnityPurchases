using UnityEngine;
using UnityEngine.Purchasing;
using System.Collections.Generic;

public abstract class AbstractPurchaseListener : MonoBehaviour, IDetailedStoreListener
{
    public static IStoreController storeController;
    public static IExtensionProvider extensionProvider;

    public void InitializePurchasing(List<ProductConfig> productConfigs)
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        foreach (var productConfig in productConfigs)
        {
            builder.AddProduct(productConfig.productId, productConfig.productType);
        }

        UnityPurchasing.Initialize(this, builder);
    }

    public static bool IsInitialized()
    {
        return storeController != null && extensionProvider != null;
    }

    public virtual void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        extensionProvider = extensions;
        Debug.Log("In-App Purchasing initialized successfully.");
    }

    public virtual void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log($"Initialization Failed: {error}. Message: {message}");
    }

    public virtual void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"Initialization Failed: {error}");
    }

    public virtual void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log($"Purchase Failed: {product.definition.id}, {failureDescription.reason}, {failureDescription.message}");
    }

    public virtual PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log($"Purchase Successful: {args.purchasedProduct.definition.id}");
        return PurchaseProcessingResult.Complete;
    }
}

[System.Serializable]
public class ProductConfig
{
    public string productId;
    public ProductType productType;

    public ProductConfig(string id, ProductType type)
    {
        productId = id;
        productType = type;
    }
}