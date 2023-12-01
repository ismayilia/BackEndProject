using Christmas.Areas.Admin.ViewModels.Tag;

namespace Christmas.Areas.Admin.ViewModels.Blog
{
    public class BlogPageVM
    {
        public List<BlogVM> Blogs { get; set; }
        public List<TagVM> Tags { get; set; }
    }
}
