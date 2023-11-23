// ImageService.cs
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

public class ImageService
{
    private readonly IMongoCollection<Image> _imageCollection;

    public ImageService(IMongoDatabase database)
    {
        _imageCollection = database.GetCollection<Image>("Images");
    }

    public List<Image> GetAllImages()
    {
        return _imageCollection.Find(image => true).ToList();
    }

    public void AddImage(Image image)
    {
        _imageCollection.InsertOne(image);
    }
}
