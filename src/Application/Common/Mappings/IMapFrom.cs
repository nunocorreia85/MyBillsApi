using AutoMapper;

namespace MyBills.Application.Common.Mappings
{
    public interface IMapFrom
    {
        void Mapping(Profile profile);
    }

    public abstract class MapFrom<T> : IMapFrom
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}