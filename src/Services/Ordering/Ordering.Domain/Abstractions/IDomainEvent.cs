﻿using MediatR;

namespace Ordering.Domain.Abstractions
{
  //We are INotification domain events to be dispatched through the mediator handlers
  public interface IDomainEvent : INotification
  {
    Guid EventId => Guid.NewGuid();
    public DateTime OcurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
  }
}
