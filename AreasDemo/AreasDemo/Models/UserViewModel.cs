namespace AreasDemo.Models
{
    using AreasDemo.Infrastructure.Mapping;
    using AutoMapper;

    public class UserViewModel : IMapFrom<ApplicationUser> , IHaveCustomMapping
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string MailAddress { get; set; }

        public void ConfigureMapping(Profile profile)
            => profile
                .CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(u => u.MailAddress, cfg => cfg.MapFrom(u => u.Email));
    }
}
