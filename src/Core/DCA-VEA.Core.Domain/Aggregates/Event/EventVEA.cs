using DCA_VEA.Core.Domain.Aggregates.Event;
using DCA_VEA.Core.Domain.Aggregates.Guest;
using DCA_VEA.Core.Domain.Aggregates.Location;
using DCA_VEA.Core.Domain.Common.Bases;
using DCA_VEA.Core.Domain.Common.Values;
using DCA_VEA.Core.Tools.OperationResult;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("xUnitTests")]


namespace DCA_VEA.Core.Domain.Aggregates.Events
{
    public class EventVEA : AggregateRoot<EventId>
    {
        internal EventId Id { get; private set; }
        internal EventTitle Title { get; private set; }
        internal EventDescription Description { get; private set; }
        internal TimeInterval Date { get; private set; }
        internal EventMaxGuests MaxGuests { get; private set; }
        internal EventStatus Status { get; private set; }
        internal EventType EventType { get; private set; }
        internal LocationId LocationId { get; private set; }
        internal List<GuestId> Guests { get; private set; } = new List<GuestId>();


        public EventVEA(EventId id, EventTitle title, EventDescription description, 
            TimeInterval date,  EventMaxGuests maxGuests, EventStatus status,
            EventType eventType, LocationId locationId) : base(id) 
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

            return new EventVEA(EventId.Create(Guid.NewGuid()), EventTitle.Create("Working Title"), EventDescription.Create(""),
                 TimeInterval.Create(start, end), EventMaxGuests.Create(5), new EventStatus(EventStatuses.Draft), new EventType(EventTypes.Private),
                 LocationId.Create(Guid.NewGuid()));
        }

        public Result<bool> UpdateTitle(string title)
        {
            
            if (Status.Value == EventStatuses.Cancelled || Status.Value == EventStatuses.Active)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Cannot update the title of a cancelled or active event."));
            }

            if (Status.Value == EventStatuses.Ready)
            {
                Status.SetValue(EventStatuses.Draft);
            }

            Title = EventTitle.Create(title);
            return Result<bool>.Success(true);
        }

        public Result<bool> UpdateDescription(string description)
        {
            if (Status.Value == EventStatuses.Cancelled || Status.Value == EventStatuses.Active)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Cannot update the description of a cancelled or active event."));
            }

            if (Status.Value == EventStatuses.Ready)
            {
                Status.SetValue(EventStatuses.Draft);
            }

            Description = EventDescription.Create(description);
            return Result<bool>.Success(true);
        }

        public Result<bool> UpdateTimeInterval(DateTime start, DateTime end)
        {
            if (Status.Value == EventStatuses.Active || Status.Value == EventStatuses.Cancelled)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Cannot update times for active or cancelled events."));
            }

            Date = TimeInterval.Create(start, end);

            if (Status.Value == EventStatuses.Ready)
            {
                Status.SetValue(EventStatuses.Draft);
            }

            return Result<bool>.Success(true);

        }

        public Result<bool> UpdateMaxGuests(int maxGuests)
        {
            if (maxGuests < 5)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Cannot set the number of guests lower than 5."));
            }

            if (Status.Value == EventStatuses.Cancelled)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "A cancelled event cannot be modified."));
            }

            if (Status.Value == EventStatuses.Active && maxGuests <= MaxGuests.Value)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "The number of guests of an ACTIVE event cannot be reduced (can only be increased)."));
            }

            MaxGuests = EventMaxGuests.Create(maxGuests);
            return Result<bool>.Success(true);
        }

        public Result<bool> UpdateLocation(Guid locationId)
        {
            LocationId = LocationId.Create(locationId);
            return Result<bool>.Success(true);
        }

        public Result<bool> Ready()
        {
            if (Status.Value == EventStatuses.Cancelled)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "A cancelled event cannot be readied."));
            }

            if (Title.Value.Equals("Working Title"))
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "The title must be updated from the default one."));
            }

            if (DateTime.Now > Date.Start || DateTime.Now > Date.End)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "An event in the past cannot be readied."));
            }

            if (Status.Value == EventStatuses.Draft)
            {

                if (Title.Value.Length < 3 || Title.Value.Length > 75 || string.IsNullOrWhiteSpace(Title.Value))
                {
                    return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Title must be between 3 and 75 characters long."));
                }

                if (Description.Value.Length <= 0 || Description.Value.Length > 250 || string.IsNullOrWhiteSpace(Description.Value))
                {
                    return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Description must be between 3 and 250 characters."));
                }

                if (MaxGuests.Value < 5 || MaxGuests.Value > 50)
                {
                    return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Cannot set the number of max guests that is not between 5 and 50."));
                }

                Status.SetValue(EventStatuses.Ready);
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Could not process ready request."));
        }

        public Result<bool> Activate()
        {
            if (Status.Value == EventStatuses.Cancelled)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "A cancelled event cannot be activated."));
            }

            if (Status.Value != EventStatuses.Active)
            {
                if (Status.Value == EventStatuses.Draft)
                {
                    if (Title.Value.Length < 3 || Title.Value.Length > 75 || string.IsNullOrWhiteSpace(Title.Value))
                    {
                        return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Title must be between 3 and 75 characters long."));
                    }

                    if (Description.Value.Length <= 0 || Description.Value.Length > 250 || string.IsNullOrWhiteSpace(Description.Value))
                    {
                        return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Description must be between 3 and 250 characters."));
                    }

                    if (MaxGuests.Value < 5 || MaxGuests.Value > 50)
                    {
                        return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Cannot set the number of max guests that is not between 5 and 50."));
                    }

                    Status.SetValue(EventStatuses.Active);
                    return Result<bool>.Success(true);
                }

                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Could not process activate request."));
            }

            return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Event is already active or not in a state that can be activated."));
        }

        public Result<bool> Cancel()
        {
            Status.SetValue(EventStatuses.Cancelled);
            return Result<bool>.Success(true);
        }


        public Result<bool> MakePublic()
        {
            if (Status.Value == EventStatuses.Cancelled)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "A cancelled event cannot be modified."));
            }

            EventType.SetValue(EventTypes.Public);
            return Result<bool>.Success(true);
        }

        public Result<bool> MakePrivate()
        {
            if (Status.Value == EventStatuses.Cancelled || Status.Value == EventStatuses.Active)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Cannot make a cancelled or active event private."));
            }

            EventType.SetValue(EventTypes.Private);
            return Result<bool>.Success(true);
        }

        public void AddGuest(GuestId guestId)
        {
            Guests.Add(guestId);
        }

        public void RemoveGuest(GuestId guestId)
        {
            Guests.Remove(guestId);
        }

        public bool EventFull()
        {
            return Guests.Count == MaxGuests.Value;
        }

        public bool GuestAttends(GuestId guestId)
        {
            return Guests.Contains(guestId);
        }

        public int AttendingGuests()
        {
            return Guests.Count;
        }
    }
}