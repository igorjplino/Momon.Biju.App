namespace Momon.Biju.Web.Helpers;

public static class ImageUploadHelper
{
    private static readonly string _uploadDirectory; 
    private static readonly string _imageDirectory; 

    static ImageUploadHelper()
    {
        _imageDirectory = "/images/products";
        _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _imageDirectory.TrimStart('/'));

        CreateFolderIfNotExists();
    }
    
    public static async Task<string?> SaveProductImageAsync(IFormFile? imageFile)
    {
        if (imageFile is null || imageFile.Length == 0)
        {
            return null;
        }

        try
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(_uploadDirectory, fileName);
            
            await using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);
            
            return $"{_imageDirectory}/{fileName}";
        }
        catch (IOException ex)
        {
            //TODO log
        }
        catch (Exception ex)
        {
            //TODO log
        }
        
        return null;
    }

    public static void DeleteImageIfExists(string? imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return;
        }

        try
        {
            var relativePath = imagePath.Replace(_imageDirectory, "").TrimStart('/');
        
            var fullPath = Path.Combine(_uploadDirectory, relativePath);
            
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            //TODO log
        }
        catch (IOException ex)
        {
            //TODO log
        }
        catch (Exception ex)
        {
            //TODO log
        }
    }


    private static void CreateFolderIfNotExists()
    {
        try
        {
            if (!Directory.Exists(_uploadDirectory))
            {
                Directory.CreateDirectory(_uploadDirectory);
                Console.WriteLine($"Created image upload directory: {_uploadDirectory}");
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            //TODO log
        }
        catch (Exception ex)
        {
            //TODO log
        }
    }
}