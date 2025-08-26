namespace Momon.Biju.App.Domain.Model;

public record OrderRequest(
    string PhoneNumber,
    string OrderMesage)
{ }