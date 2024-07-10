using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine.Networking;
using System.Collections;
using System.Threading.Tasks;
using Unity.Services.Core;

public class CustomPurchaseListener : AbstractPurchaseListener
{
    public List<ProductConfig> productConfigs;
    private string apiUrl = "https://u7zowesrmk.execute-api.eu-central-1.amazonaws.com";

    async void Start()
    {
        await WaitForUnityServicesInitialization();
        // Initialize purchasing with dynamic product configurations
        InitializePurchasing(productConfigs);
    }

    private async Task WaitForUnityServicesInitialization()
    {
        while (UnityServices.State != ServicesInitializationState.Initialized)
        {
            await Task.Yield();
        }
    }

    public override PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // Custom logic for processing purchases
        Debug.Log($"Purchase Successful: {args.purchasedProduct.definition.id}");

        // Extract product and purchase details
        ProductDetails productDetails = new ProductDetails(
            args.purchasedProduct.definition.id,
            args.purchasedProduct.metadata.localizedPriceString,
            args.purchasedProduct.metadata.isoCurrencyCode
        );

        PurchaseDetails purchaseDetails = new PurchaseDetails(
            args.purchasedProduct.definition.id,
            args.purchasedProduct.transactionID,
            args.purchasedProduct.receipt
        );

        // Send the event
        StartCoroutine(SendEvent("purchase_successful", productDetails, purchaseDetails));

        return base.ProcessPurchase(args);
    }

    private IEnumerator SendEvent(string eventType, ProductDetails productDetails, PurchaseDetails purchaseDetails)
    {
        // string url = $"{apiUrl}/sdk-{Application.platform.ToString().ToLower()}";
        string url = $"{apiUrl}/sdk-apple";
        var messageBody = new MessageBody(eventType, productDetails, purchaseDetails);
        var payload = new Payload(messageBody);
        string jsonPayload = JsonUtility.ToJson(payload);

        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"Failed to send {eventType} event: {request.error}");
        }
        else
        {
            Debug.Log($"{eventType} event sent successfully: {request.downloadHandler.text}");
        }
    }
}