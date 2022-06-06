using System.Linq;
using FlyIBooking.Dtos;
using FlyIBooking.Entities;

namespace FlyIBooking.Converters
{
    public static class PlaneConverter
    {
        public static PlaneDto ToDto(this PlaneDal obj)
        {
            return obj == null
                ? null
                : new PlaneDto
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Price = obj.Price,
                    ArrivalDate = obj.ArrivalDate,
                    DepartureDate = obj.DepartureDate,
                    From = obj.From,
                    To = obj.To
                };
        }
        
        public static PlaneDal ToDal(this PlaneDto obj)
        {
            return obj == null
                ? null
                : new PlaneDal
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Price = obj.Price,
                    ArrivalDate = obj.ArrivalDate,
                    DepartureDate = obj.DepartureDate,
                    From = obj.From,
                    To = obj.To
                };
        }
    }
}