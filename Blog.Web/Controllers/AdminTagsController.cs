using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;
                  // /AdminTags
public class AdminTagsController : Controller
{
    //constructor creation + injection
    private BlogDbContext _blogDbContext;
    public AdminTagsController(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }

    [HttpGet]
              //         /Add
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [ActionName("Add")]
    public IActionResult Add(AddTagRequest addTagRequest)
    {
        //read input elements (map AddTagRequest to Tag domain model
        var tag = new Tag
        {
            Name = addTagRequest.Name,
            DisplayName = addTagRequest.DisplayName,
        };

        //post in db
        _blogDbContext.Tags.Add(tag);
        _blogDbContext.SaveChanges();

        return View("Add");
    }
}
