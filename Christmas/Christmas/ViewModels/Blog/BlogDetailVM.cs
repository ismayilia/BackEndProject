using Christmas.Models;
using Christmas.ViewModels.Tag;

namespace Christmas.ViewModels.Blog
{
    public class BlogDetailVM
    {
        public BlogVM Blog { get; set; }
        public List<TagVM> Tags { get; set; }
    }
}
