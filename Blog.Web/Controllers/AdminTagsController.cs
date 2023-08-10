﻿using Blog.Web.Data;
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
    private readonly ITagRepository tagRepository;

    public AdminTagsController(ITagRepository tagRepository)
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
        ValidateAddTagRequest(addTagRequest);
        if (ModelState.IsValid == false)
        {
            return View();
        }

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


    private void ValidateAddTagRequest(AddTagRequest addTagRequest)
    {
        if (addTagRequest.Name is not null && addTagRequest.DisplayName is not null)
        {
            if (addTagRequest.Name == addTagRequest.DisplayName)
            {
                ModelState.AddModelError("DisplayName", "Name cannot be the same as DisplayName.");
            }
        }
    }

}
