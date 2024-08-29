using AutoMapper;
using PasSecWebApi.Persistence;
using PasSecWebApi.Shared.Dtos;

namespace PasSecWebApi.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // map objects here.
            CreateMap<Vault, VaultDto>().ReverseMap();
            CreateMap<VaultStorageKey, VaultStorageKeyDto>().ReverseMap();
            CreateMap<VaultStorageKeySecurityQA, VaultStorageKeySecurityQADto>().ReverseMap();

        }
    }
}
