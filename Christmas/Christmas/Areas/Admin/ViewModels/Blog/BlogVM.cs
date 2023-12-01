using Christmas.Models;
using Christmas.Areas.Admin.ViewModels.Tag;

namespace Christmas.Areas.Admin.ViewModels.Blog
{
    public class BlogVM
    {
        public int Id { get; set; }
        public List<BlogImage> Images { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime DateTime { get; set; }
    }
}
