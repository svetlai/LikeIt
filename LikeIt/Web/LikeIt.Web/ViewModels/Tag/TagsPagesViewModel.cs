namespace LikeIt.Web.ViewModels.Tag
{
    using System.Collections.Generic;

    using PagedList;

    using LikeIt.Web.ViewModels.Page;

    public class TagsPagesViewModel
    {
        public ICollection<TagViewModel> Tags { get; set; }

        public IPagedList<ListPagesViewModel> Pages { get; set; }
    }
}