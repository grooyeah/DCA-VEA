using DCA_VEA.Core.Domain.Aggregates.Event;
using DCA_VEA.Core.Domain.Aggregates.Location;
using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Domain.Common.Values;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("xUnitTests")]


namespace DCA_VEA.Core.Domain.Aggregates.Events
{
    public class EventVEA : AggregateRoot<EventId>
    {
        public EventId Id { get; }
        public EventTitle Title { get; }
        public EventDescription Description { get; }
        public TimeInterval Date { get; }
        public EventMaxGuests MaxGuests { get; }
        public EventStatus Status { get; }
        public EventType EventType { get; }
        public LocationId LocationId { get; }


        public EventVEA(EventId id, EventTitle title, EventDescription description, 
            TimeInterval date,  EventMaxGuests maxGuests, EventStatus status,
            EventType eventType, LocationId locationId)
        {
            Id = id;
            Title = title;
            Description = description;
            Date = date;
            MaxGuests = maxGuests;
            Status = status;
            EventType = eventType;
            LocationId = locationId;
        }

        public static EventVEA CreateEmpty()
        {
            DateTime now = DateTime.Now;
            DateTime start = now.AddHours(now.Hour < 8 ? 8 - now.Hour : 24 - now.Hour + 8);
            DateTime end = start.AddHours(5);

            if (end.TimeOfDay > new TimeSpan(1, 0, 0))
            {
                start = start.AddHours(-(end.Hour - 1));
                end = start.AddHours(5);
            }

            return new EventVEA(new EventId(Guid.NewGuid()), new EventTitle("Working Title"),new EventDescription(""),
                new TimeInterval(start, end), new EventMaxGuests(5), new EventStatus(EventStatuses.Draft), new EventType(EventTypes.Private), new LocationId(Guid.NewGuid()));
        }

        public void UpdateTitle(string title)
        {
            if (Status.Value == EventStatuses.Cancelled || Status.Value == EventStatuses.Active)
            {
                throw new InvalidOperationException("Cannot update the title of a cancelled or active event.");
            }

            if(Status.Value == EventStatuses.Ready)
            {
                Status.SetValue(EventStatuses.Draft);
            }

            Title.SetValue(title);
        }

        public void UpdateDescription(string description)
        {
            if (Status.Value == EventStatuses.Cancelled || Status.Value == EventStatuses.Active)
            {
                throw new InvalidOperationException("Cannot update the description of a cancelled or active event.");
            }

            if(Status.Value == EventStatuses.Ready)
            {
                Status.SetValue(EventStatuses.Draft);
            }

            Description.SetValue(description);
        }

        public void UpdateTimeInterval(DateTime start, DateTime end)
        {

            if (Status.Value == EventStatuses.Active || Status.Value == EventStatuses.Cancelled)
            {
                throw new InvalidOperationException("Cannot update times for active or cancelled events.");
            }

            try
            {
                var newTimeInterval = new TimeInterval(start, end);
                Date.SetValue(start, end); 

                if (Status.Value == EventStatuses.Ready)
                {
                    Status.SetValue(EventStatuses.Draft);
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Failed to update event time interval: {ex.Message}");
            }
            Date.SetValue(start, end);
        }

        public void UpdateMaxGuests(int maxGuests)
        {
            if(Status.Value == EventStatuses.Active)
            {
                if(maxGuests <= MaxGuests.Value)
                {
                    throw new InvalidOperationException("The number of guests of an ACTIVE event cannot be reduced (can only be increased).");
                }
            }

            if(Status.Value == EventStatuses.Cancelled)
            { 
                throw new InvalidOperationException("A cancelled event cannot be modified.");
            }

            if(maxGuests < 5)
            {
                throw new InvalidOperationException("Cannot set the number of guests lower than 5.");
            }

            if (maxGuests >= MaxGuests.Value)
            {
                MaxGuests.SetValue(maxGuests);
            }
        }

        public void UpdateLocation(Guid locationId)
        {
            LocationId.SetValue(locationId);
        }

        public void Ready()
        {
            if(Status.Value == EventStatuses.Cancelled)
            {
                throw new InvalidOperationException("A cancelled event cannot be readied.");
            }

            if(Title.Value.Equals("Working title"))
            {
                throw new InvalidOperationException("The tile must be updated from the default one.");
            }

            if(DateTime.Now > Date.Start || DateTime.Now > Date.End)
            {
                throw new InvalidOperationException("An event in the past cannot be readied.");
            }

            if (Status.Value == EventStatuses.Draft)
            {
                // S1 Validate title, description, times, visibility, maximum guests

                // F1 if not validated, fail, throw exception

                Status.SetValue(EventStatuses.Ready);
            }


        }

        public void Activate()
        {
            Status.SetValue(EventStatuses.Active);
        }

        public void Cancel()
        {
            Status.SetValue(EventStatuses.Cancelled);
        }


        public void MakePublic()
        {
            if (Status.Value == EventStatuses.Cancelled)
            {
                throw new InvalidOperationException("A cancelled event cannot be modified.");
            }

            EventType.SetValue(EventTypes.Public);
        }

        public void MakePrivate()
        {
            if(EventType.Value == EventTypes.Private)
            {
                return;
            }

            if (Status.Value == EventStatuses.Active || Status.Value == EventStatuses.Cancelled)
            {
                throw new InvalidOperationException("A cancelled event cannot be modified.");
            }

            if(Status.Value == EventStatuses.Ready || Status.Value == EventStatuses.Draft)
            {
                EventType.SetValue(EventTypes.Private);
                Status.SetValue(EventStatuses.Draft);
            }
        }
    }
}
