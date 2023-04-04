using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;
// /AdminTags
public class AdminTagsController : Controller
{
    //constructor creation + injection
    private readonly BlogDbContext _blogDbContext;
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

        return RedirectToAction("List");
    }

    [HttpGet]
    [ActionName("List")]
    public IActionResult List()
    {
        var tags = _blogDbContext.Tags.ToList();

        return View(tags);
    }

    [HttpGet]
    [ActionName("Edit")]
    public IActionResult Edit(Guid id)
    {
        var tag = _blogDbContext.Tags.FirstOrDefault(x => x.Id == id);

        if (tag != null)
        {
            var editTagRequest = new EditTagRequest
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayName = tag.DisplayName
            };
            return View(editTagRequest);
        }
        return View(null);
    }
    [HttpPost]
    public IActionResult Edit(EditTagRequest editTagRequest)
    {
        var tag = new Tag
        {
            Id = editTagRequest.Id,
            Name = editTagRequest.Name,
            DisplayName = editTagRequest.DisplayName,
        };
        var existingTag = _blogDbContext.Tags.Find(tag.Id);

        if (existingTag != null)
        {
            existingTag.Name = tag.Name;
            existingTag.DisplayName = tag.DisplayName;

            _blogDbContext.SaveChanges();

            return RedirectToAction("List");
        }
        return RedirectToAction("Edit", new { id = editTagRequest.Id });
    }



    [HttpDelete]
    [ActionName("Delete")]
    public IActionResult Delete(int id)
    {


        return RedirectToAction("List");
    }

}
