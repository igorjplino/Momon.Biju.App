namespace Momon.Biju.App.Domain.Model;

public class OrderContentOptions
{
    public required string PhoneNumber { get; init; }
    public required WhatsappMessage WhatsappMessage { get; init; }
}

public class WhatsappMessage
{
    public required string HelloMessage { get; init; }
    public required string OrderMessage { get; init; }
    public required string TotalPurchaseMessage { get; init; }
    public required string AdditionalInformationMessage { get; init; }
}