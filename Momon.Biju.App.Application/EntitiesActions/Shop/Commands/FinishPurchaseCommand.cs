using System.Text;
using MediatR;
using Microsoft.Extensions.Options;
using Momon.Biju.App.Application.Common;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.App.Domain.Model;
using static System.String;

namespace Momon.Biju.App.Application.EntitiesActions.Shop.Commands;

public record FinishPurchaseCommand(
    string Name,
    string AdditionalInformation,
    Dictionary<Guid, int> ProductsAndQuantities)
    : IRequest<Result<OrderRequest>>
{ }

public class CreateProductCommandHandler : IRequestHandler<FinishPurchaseCommand, Result<OrderRequest>>
{
    private readonly OrderContentOptions _orderContent;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(
        IOptions<OrderContentOptions> orderContentOptions,
        IProductRepository productRepository)
    {
        _orderContent = orderContentOptions.Value;
        _productRepository = productRepository;
    }

    public async Task<Result<OrderRequest>> Handle(FinishPurchaseCommand request, CancellationToken cancellationToken)
    {
        var orderMessage = new StringBuilder(Format(_orderContent.WhatsappMessage.HelloMessage, request.Name));
        
        List<Product> products = await _productRepository.ListProductsByIdsAsync(request.ProductsAndQuantities.Keys);

        var total = 0M;
        
        foreach (var product in products)
        {
            var quantity = request.ProductsAndQuantities[product.Id];
            
            total += product.Price * quantity;
            
            orderMessage.AppendLine(Format(_orderContent.WhatsappMessage.OrderMessage, product.ReferenceNumber, product.Name, quantity, product.Price));
        }
        
        orderMessage.AppendLine(Format(_orderContent.WhatsappMessage.TotalPurchaseMessage, total));

        if (!IsNullOrWhiteSpace(request.AdditionalInformation))
        {
            orderMessage.AppendLine(Format(_orderContent.WhatsappMessage.AdditionalInformationMessage, request.AdditionalInformation));
        }

        return new OrderRequest(
            _orderContent.PhoneNumber,
            orderMessage.ToString());
    }
}