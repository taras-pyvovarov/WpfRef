using People.Domain;
using Prism.Events;
using System;

namespace Module.People.PubSubEvents
{
    public class PersonSelectionChangedEvent : PubSubEvent<Tuple<Person, bool>> { }
}
