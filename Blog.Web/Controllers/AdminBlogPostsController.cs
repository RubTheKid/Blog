using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;// (selectListItem)

namespace Blog.Web.Controllers;

public class AdminBlogPostsController : Controller
{
    private readonly ITagRepository tagRepository;
    private readonly IBlogPostRepository blogPostRepository;

    public AdminBlogPostsController(ITagRepository tagRepository,IBlogPostRepository blogPostRepository)
    {
        this.tagRepository = tagRepository;
        this.blogPostRepository = blogPostRepository;
    }

    [HttpGet]
   public async Task<IActionResult> Add()
    {
        //get tags from repository
        var tags = await tagRepository.GetAllAsync();

        var model = new AddBlogPostRequest
        {
            Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
        };
            
        return View(model);

    }

    [HttpPost]
    [ActionName("Add")]
    public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
    {
        //mapear o view model para o domain model
        var blogPost = new BlogPost
        {
            Heading = addBlogPostRequest.Heading,
            PageTitle = addBlogPostRequest.PageTitle,
            Content = addBlogPostRequest.Content,
            ShortDescription = addBlogPostRequest.ShortDescription,
            FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
            UrlHandle = addBlogPostRequest.UrlHandle,
            PublishedDate = addBlogPostRequest.PublishedDate,
            Author = addBlogPostRequest.Author,
            Visible = addBlogPostRequest.Visible,
        };

        //mapear tags das tags selecionadas
        var selectedTags = new List<Tag>();
        foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
        {
            var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
            var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);

            if (existingTag != null)
            {
                selectedTags.Add(existingTag);
            }
        }
        //mapear tags para o domain model
        blogPost.Tags = selectedTags;

        await blogPostRepository.AddAsync(blogPost);

        return RedirectToAction("Add");
    }


}
