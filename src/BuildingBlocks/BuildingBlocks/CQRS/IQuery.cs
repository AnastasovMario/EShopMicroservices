using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull
    {
        //Designed to return a result, it is used for the read operations
    }
}
