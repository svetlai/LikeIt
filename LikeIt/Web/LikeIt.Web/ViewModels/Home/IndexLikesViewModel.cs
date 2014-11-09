namespace LikeIt.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using LikeIt.Models;
    using LikeIt.Web.Infrastructure.Mapping;

    public class IndexLikesViewModel : IMapFrom<Like>
    {
        public string Name { get; set; }
    }
}