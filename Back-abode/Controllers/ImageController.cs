// ImageController.cs
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

[Route("api/images")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly ImageService _imageService;

    public ImageController(ImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet]
    public IActionResult GetImages()
    {
        var images = _imageService.GetAllImages();
        return Ok(images);
    }

    [HttpPost]
    public IActionResult UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Invalid file");

        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);

            var image = new Image
            {
                FileName = file.FileName,
                ImageData = memoryStream.ToArray()
            };

            _imageService.AddImage(image);
        }

        return Ok("Image uploaded successfully");
    }
}
