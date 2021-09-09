using System;
using AutoMapper;
using AirboxSystems.API.Domain.Models;
using AirboxSystems.API.Resources;

namespace AirboxSystems.API.Mapping
{
    public class ModelToResourceUserPosition : Profile
    {
        public ModelToResourceUserPosition()
        {
            CreateMap<UserPosition, UserPositionResource>().ReverseMap();
            CreateMap<LocationHistory, LocationHistoryResource>().ReverseMap();
        }
    }
}
