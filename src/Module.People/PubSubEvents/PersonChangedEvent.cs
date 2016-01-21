using People.Domain;
using Prism.Events;

namespace Module.People.PubSubEvents
{
    public class PersonChangedEvent : PubSubEvent<Person> { }
}
