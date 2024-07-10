[System.Serializable]
public class PurchaseDetails
{
    public string productId;
    public string transactionId;
    public string receipt;

    public PurchaseDetails(string productId, string transactionId, string receipt)
    {
        this.productId = productId;
        this.transactionId = transactionId;
        this.receipt = receipt;
    }
}