namespace Ordering.Application.Dtos;

//Make sure that Cvv is written in this way, otherwise mapster won't work properly
public record PaymentDto(string CardName, string CardNumber, string Expiration, string Cvv, int PaymentMethod);