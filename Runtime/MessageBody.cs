[System.Serializable]
public class MessageBody
{
    public string eventType;
    public ProductDetails productDetails;
    public PurchaseDetails purchaseDetails;

    public MessageBody(string eventType, ProductDetails productDetails, PurchaseDetails purchaseDetails)
    {
        this.eventType = eventType;
        this.productDetails = productDetails;
        this.purchaseDetails = purchaseDetails;
    }
}