using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;

    public DeleteOrderCommandHandler(
        IOrderRepository orderRepository,
        IMapper mapper,
        ILogger<DeleteOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);

        if (orderToDelete is null)
        {
            _logger.LogError("Order not exist on databse");
            throw new NotFoundException(nameof(Order), request.Id);
        }

        _mapper.Map(request, orderToDelete, typeof(UpdateOrderCommand), typeof(Order));

        await _orderRepository.UpdateAsync(orderToDelete);

        _logger.LogInformation($"Order {orderToDelete.Id} is successfully deleted.");
    }
}
