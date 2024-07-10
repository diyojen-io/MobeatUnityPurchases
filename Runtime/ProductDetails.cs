[System.Serializable]
public class ProductDetails
{
    public string productId;
    public string price;
    public string currency;

    public ProductDetails(string productId, string price, string currency)
    {
        this.productId = productId;
        this.price = price;
        this.currency = currency;
    }
}