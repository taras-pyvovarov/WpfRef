using System;
using People.Domain;
using Prism.Events;

namespace Module.People.PubSubEvents
{
    public class PersonSelectionChangedEvent : PubSubEvent<Tuple<Person, bool>> { }
}
