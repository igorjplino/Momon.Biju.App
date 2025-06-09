namespace Momon.Biju.Web.Helpers;

public static class ImageUploadHelper
{
    public static async Task<string?> SaveProductImageAsync(IFormFile? imageFile)
    {
        if (imageFile is null || imageFile.Length == 0)
        {
            return null;
        }

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

        return $"/images/{folder}/{fileName}";
    }
}