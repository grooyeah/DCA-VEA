using DCA_VEA.Core.Domain.Aggregates.Event;
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

        public Result<bool> UpdateTitle(string title)
        {
            try
            {
                if (Status.Value == EventStatuses.Cancelled || Status.Value == EventStatuses.Active)
                {
                    return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Cannot update the title of a cancelled or active event."));
                }

                if (Status.Value == EventStatuses.Ready)
                {
                    Status.SetValue(EventStatuses.Draft);
                }

                Title.SetValue(title);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.InternalServerError, ex.Message));
            }
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

            Description.SetValue(description);
            return Result<bool>.Success(true);
        }

        public Result<bool> UpdateTimeInterval(DateTime start, DateTime end)
        {
            if (Status.Value == EventStatuses.Active || Status.Value == EventStatuses.Cancelled)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "Cannot update times for active or cancelled events."));
            }

            try
            {
                Date.SetValue(start, end);
                if (Status.Value == EventStatuses.Ready)
                {
                    Status.SetValue(EventStatuses.Draft);
                }
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.InternalServerError, $"Failed to update event time interval: {ex.Message}"));
            }
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

            MaxGuests.SetValue(maxGuests);
            return Result<bool>.Success(true);
        }

        public Result<bool> UpdateLocation(Guid locationId)
        {
            LocationId.SetValue(locationId);
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

            Status.SetValue(EventStatuses.Ready);
            return Result<bool>.Success(true);
        }

        public Result<bool> Activate()
        {
            if (Status.Value == EventStatuses.Cancelled)
            {
                return Result<bool>.Failure(new Error(ErrorCodes.SpecificError, "A cancelled event cannot be activated."));
            }

            if (Status.Value != EventStatuses.Active)
            {
                Status.SetValue(EventStatuses.Active);
                return Result<bool>.Success(true);
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
    }
}