using MediatR;

namespace FlighService.Core.Domain.Common.Domain;
public interface IDomainEvent : INotification
{

}
public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }

    protected void AddEvent(IDomainEvent @event)
    {

    }
}
