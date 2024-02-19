﻿using FitnessAI.Application.Common.Exceptions;
using FitnessAI.Application.Dictionaries;
using FitnessAI.Application.Files.Commands.DeleteFile;
using FitnessAI.Application.Files.Commands.UploadFile;
using FitnessAI.Application.Files.Queries.GetFiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize(Roles = RolesDict.ADMINISTRATOR)]
public class FileController : BaseController
{
    private readonly ILogger<FileController> _logger;

    public FileController(ILogger<FileController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Files()
    {
        return View(await Mediator.Send(new GetFilesQuery()));
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IEnumerable<IFormFile> files)
    {
        try
        {
            await Mediator.Send(
                new UploadFileCommand
                {
                    Files = files
                });

            return Json(new { success = true });
        }
        catch (ValidationException exception)
        {
            return Json(new
            {
                success = false,
                message = string.Join(". ", exception.Errors.Select(x => string.Join(". ", x.Value.Select(y => y))))
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, null);
            return Json(new { success = false });
        }
    }

    [HttpPost]
    public async Task<IActionResult> UploadEditor(IEnumerable<IFormFile> files)
    {
        try
        {
            await Mediator.Send(
                new UploadFileCommand
                {
                    Files = files
                });

            return Json(new
            {
                success = true,
                fullPath = Path.Combine($"{Request.Scheme}://{Request.Host}{Request.PathBase}", "ftp",
                    files.First().FileName),
                name = files.First().FileName
            });
        }
        catch (ValidationException exception)
        {
            return Json(new
            {
                success = false,
                message = string.Join(". ", exception.Errors.Select(x => string.Join(". ", x.Value.Select(y => y))))
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, null);
            return Json(new { success = false });
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteFile(int id)
    {
        try
        {
            await Mediator.Send(new DeleteFileCommand { Id = id });

            return Json(new { success = true });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, null);
            return Json(new { success = false });
        }
    }

    public async Task<IActionResult> GetFiles()
    {
        try
        {
            var files = await Mediator.Send(
                new GetFilesQuery());

            return Json(new { success = true, images = files });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, null);
            return Json(new { success = false });
        }
    }
}