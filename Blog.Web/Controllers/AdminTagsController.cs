using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Controllers;
// /AdminTags
[Authorize(Roles = "Admin")]
public class AdminTagsController : Controller
{
    //constructor creation + injection
    //private readonly BlogDbContext _blogDbContext;
    private readonly ITagRepository tagRepository;

    public AdminTagsController(ITagRepository tagRepository)//BlogDbContext blogDbContext)
    {
        this.tagRepository = tagRepository;
    }


    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [ActionName("Add")]
    public async Task<IActionResult> Add(AddTagRequest addTagRequest)
    {
        var tag = new Tag
        {
            Name = addTagRequest.Name,
            DisplayName = addTagRequest.DisplayName,
        };

        await tagRepository.AddAsync(tag);
        return RedirectToAction("List");
    }

    [HttpGet]
    [ActionName("List")]
    public async Task<IActionResult> List()
   {
        var tags = await tagRepository.GetAllAsync();

        return View(tags);
    }
    
    [HttpGet]
    [ActionName("Edit")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var tag = await tagRepository.GetAsync(id);

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
    public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
    {
        var tag = new Tag
        {
            Id = editTagRequest.Id,
            Name = editTagRequest.Name,
            DisplayName = editTagRequest.DisplayName,
        };


        var updatedTag = await tagRepository.UpdateAsync(tag);

        if (updatedTag != null)
        {
            //show success
        }
        else
        {
            //show error
        }

        //return RedirectToAction("Edit", new { id = editTagRequest.Id });
        return RedirectToAction("List");
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
    {
        var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

        if (deletedTag != null)
        {
            //success notification
            return RedirectToAction("List");
        }
        
           //fail notification    
        return RedirectToAction("Edit", new { id = editTagRequest.Id });
        //return RedirectToAction("List");
    }

}
