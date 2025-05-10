namespace Momon.Biju.Web.Helpers;

public static class ImageUploadHelper
{
    public static async Task<string> SaveProductImageAsync(IFormFile imageFile)
    {
        if (imageFile.Length == 0)
            throw new Exception();

        const string folder = "products";

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", folder);

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        // Return the relative path to use in <img src=...>
        return $"/images/{folder}/{fileName}";
    }
}