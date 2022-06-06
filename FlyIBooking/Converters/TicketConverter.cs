using FlyIBooking.Dtos;
using FlyIBooking.Entities;

namespace FlyIBooking.Converters
{
    public static class TicketConverter
    {
        public static TicketDal ToDal(this TicketDto obj)
        {
            return obj == null
                ? null
                : new TicketDal
                {
                    Id = obj.Id,
                    Price = obj.Price,
                    AccountId = obj.AccountId,
                    PlaneId = obj.PlaneId,
                    SeatNumber = obj.SeatNumber
                };
        }
        
        public static TicketDto ToDto(this TicketDal obj)
        {
            return obj == null
                ? null
                : new TicketDto
                {
                    Id = obj.Id,
                    Price = obj.Price,
                    AccountId = obj.AccountId,
                    PlaneId = obj.PlaneId,
                    SeatNumber = obj.SeatNumber
                };
        }
    }
}