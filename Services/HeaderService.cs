using System;
using System.IO;
using System.Threading.Tasks;

namespace RockwellBlog.Services
{
    public class HeaderService
    {
        private string _image { get; set; }

        public string Image
        {
            get
            {
                //So when I call object.Image I want either the image or a default...
                var defaultImageData = DecodeImage(EncodeFile("defaultPostImage.png"), "png");
                return string.IsNullOrEmpty(_image) ? defaultImageData : _image;
            }
            set
            {
                _image = value;
            }
        }
        public string MainText { get; set; }
        public string SubText { get; set; }
        public string AuthorText { get; set; }

        public void Set(byte[] imageData, string contentType, string mainText, string subText, DateTime created)
        {
            Image = DecodeImage(imageData, contentType);
            MainText = mainText;
            SubText = subText;
            AuthorText = $"Created by John Flynn on {created.ToString("MMM dd, yyyy")}";
        }

        public void Reset()
        {
            Image = MainText = SubText = AuthorText = "";
        }

        private string DecodeImage(byte[] data, string type)
        {
            return $"data:image/{type};base64,{Convert.ToBase64String(data)}";
        }

        public byte[] EncodeFile(string fileName)
        {
            var file = $"{Directory.GetCurrentDirectory()}/wwwroot/images/{fileName}";
            return File.ReadAllBytes(file);
        }
    }
}