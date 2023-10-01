using SkiaSharp;
using System.IO;

namespace QuizQuerBeet.Services;

public class ImageService
{
    private readonly IMediaPicker imagePicker;

    public ImageService(IMediaPicker picker)
    {
        this.imagePicker = picker;
    }

    public async Task<byte[]> SelectPictureAsync()
    {
        FileResult picture = await imagePicker.PickPhotoAsync();
        if (picture is null)
            return null;

        var fileStream = await picture.OpenReadAsync();

        return ScaleImage(fileStream);
    }

    private byte[] ScaleImage(Stream filestream)
    {
        const int targetHeight = 480;
        const int targetWidth = 640; // 4:3 aspect ratio for 480p resolution

        //using MemoryStream inputStream = new MemoryStream();
        //filestream.CopyToAsync(inputStream);

        using var originalImage = SKBitmap.Decode(filestream);
        using var scaledImage = originalImage.Resize(new SKImageInfo(targetWidth, targetHeight), SKFilterQuality.High);

        // Save the scaled image to a byte array
        using var outputStream = new MemoryStream();
        using var skiaImage = SKImage.FromBitmap(scaledImage);
        using var data = skiaImage.Encode(SKEncodedImageFormat.Jpeg, 90);

        data.SaveTo(outputStream);
        return outputStream.ToArray();
    }
}

