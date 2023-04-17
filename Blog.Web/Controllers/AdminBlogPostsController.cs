using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;// (selectListItem)

namespace Blog.Web.Controllers;

public class AdminBlogPostsController : Controller
{
    private readonly ITagRepository tagRepository;
    public AdminBlogPostsController(ITagRepository tagRepository)
    {
        this.tagRepository = tagRepository;
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
        return RedirectToAction("Add");
    }

}
