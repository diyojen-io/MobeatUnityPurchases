[System.Serializable]
public class Payload
{
    public MessageBody MessageBody;

    public Payload(MessageBody messageBody)
    {
        this.MessageBody = messageBody;
    }
}