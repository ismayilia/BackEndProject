using Christmas.Areas.Admin.ViewModels.Tag;
using Christmas.Models;

namespace Christmas.Areas.Admin.ViewModels.Blog
{
    public class BlogDetailVM
    {
        public BlogVM Blog { get; set; }
        public List<TagVM> Tags { get; set; }
    }
}
