using Blog.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Repositories;

public interface IBlogPostCommentRepository
{
    Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

    Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId);
}
