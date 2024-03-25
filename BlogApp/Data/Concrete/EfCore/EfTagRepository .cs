using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Data.Concrete.EfCore;
namespace BlogApp.Data.Concrete.EfCore
{
    public class EfTagRepository : ITagRepository
    {
        private BlogContext _blogContext;
        public EfTagRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public IQueryable<Tag> Tags => _blogContext.Tags;
        public void CreateTag(Tag tag)
        {
            _blogContext.Tags.Add(tag);
            _blogContext.SaveChanges();
        }

   
    }
}
