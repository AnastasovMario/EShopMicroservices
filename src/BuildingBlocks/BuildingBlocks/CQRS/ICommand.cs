using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommand: ICommand<Unit>
    {
        //empty ICommand - doesn't return any response
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
        //produces a response
    }
}
