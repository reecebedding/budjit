using AutoMapper;
using budjit.core.models;

namespace budjit.ui.API.ViewModel
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tag, TagViewModel>().ReverseMap();
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();
        }
    }
}