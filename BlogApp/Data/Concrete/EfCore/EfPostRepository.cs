using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
namespace BlogApp.Data.Concrete.EfCore
{
    public class EfPostRepository : IPostRepository
    {
        private BlogContext _blogContext;
        public EfPostRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public IQueryable<Post> Posts => _blogContext.Posts;
        public void CreatePost(Post post)
        {
            _blogContext.Posts.Add(post);
            _blogContext.SaveChanges();
        }

        public void EditPost(Post post)
        {
            var entity = _blogContext.Posts.FirstOrDefault(i => i.PostId== post.PostId);
            if(entity!= null)
            {
                entity.Title = post.Title;
                entity.Description = post.Description;
                entity.Content = post.Content;
                entity.Url = post.Url;
                entity.IsAcive = post.IsAcive;
                _blogContext.SaveChanges();
            }
        }

        public void EditPost(Post post, int[] tagIds)
        {
            var entity = _blogContext.Posts.Include(i=> i.Tags).FirstOrDefault(i => i.PostId == post.PostId);
            if (entity != null)
            {
                entity.Title = post.Title;
                entity.Description = post.Description;
                entity.Content = post.Content;
                entity.Url = post.Url;
                entity.IsAcive = post.IsAcive;
                entity.Tags = _blogContext.Tags.Where(tag => tagIds.Contains(tag.TagId)).ToList();
                _blogContext.SaveChanges();
            }
        }
    }
}
