namespace LikeIt.Common
{
    public class GlobalConstants
    {
        // Users
        public const string AdminRole = "Administrator";
        public const string UserRole = "User";

        // Error messages
        public const string NoPagesFound = "No pages found";
        public const string NoPagesFoundInCategory = "No pages found in this category";
        public const string PageNotFound = "The page you're looking for wasn't found.";
        public const string ImageNotFound = "Image not found";
        public const string InvalidComment = "Invalid comment";

        // Partial views
        public const string PagesListPartial = "_PagesListPartial";
        public const string VotePartial = "_VotePartial";
        public const string CategoriesPartial = "_CategoriesPartial";
        public const string TrendingLikesPartial = "_TrendingLikes";
        public const string TrendingDislikesPartial = "_TrendingDislikes";
        public const string TagsPartial = "_TagsPartial";
       // public const string CommentsPartial = "_CommentsPartial";
        public const string RatingPartial = "_RatingPartial";
        public const string SearchPartial = "_SearchPartial";
        public const string PagingPartial = "_PagingPartial";
        public const string PageDetails = "_PageDetails";

        public const string AddCommentPartialPrivate = "~/Areas/Private/Views/Shared/_AddCommentPartial.cshtml";
        public const string DeleteCommentPartialPrivate = "~/Areas/Private/Views/Shared/_DeleteCommentPartial.cshtml";
        public const string SingleCommentPartialPrivate = "~/Areas/Private/Views/Shared/_SingleCommentPartial.cshtml";
        public const string MyCommentsPartialPrivate = "~/Areas/Private/Views/Shared/_MyCommentsPartial.cshtml";
        public const string PageCommentsPartialPrivate = "~/Areas/Private/Views/Shared/_PageCommentsPartial.cshtml";

    }
}
