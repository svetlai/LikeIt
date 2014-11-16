namespace LikeIt.Web.Areas.Administration.ViewModels.Users
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using LikeIt.Models;
    using LikeIt.Web.Areas.Administration.ViewModels.Base;
    using LikeIt.Web.Infrastructure.Mapping;

    public class UsersViewModel : AdministrationViewModel, IMapFrom<User>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        //public string Role { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            //configuration.CreateMap<LikeIt.Models.User, UsersViewModel>()
            //    .ForMember(m => m.Role, opt => opt.MapFrom(x => x.Roles.FirstOrDefault().RoleId))
            //    .ReverseMap();
        }
    }
}