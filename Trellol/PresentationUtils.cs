
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq;
using Trellol;
using System.IO;
using System.Configuration;
using Amazon.S3;
using Amazon.S3.Model;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

namespace Trellol
{
    public class PresentationUtils
    {

        internal static IEnumerable<CardUrlInfo> GetCardUrlInfoResults(string search)
        {
            var cards = PresentationData.GetCardResults(search, 5);
            return cards.Select(card => new CardUrlInfo
                                            {
                                                Name = card.Name,
                                                Description = card.Description,
                                                Url =
                                                    "/Boards/" + card.BoardName + "/Lists/" + card.ListName + "/Cards/Details/" +
                                                    card.Id
                                            });
        }


        private static void SetValidation<T>(PropertyInfo pi, Action<T> set, Action nullify) where T : Attribute
        {
            T a;
            if ((a = (T)pi.GetCustomAttribute(typeof(T))) != null)
            {
                set(a);
            }
            else { nullify(); }
        }

        internal static Dictionary<string, ValidationInfo> getValidationInfo<T>(IEnumerable<T> all, Func<T, string> selector)
        {
            return Validate<T>(all, selector);
        }

        internal static Dictionary<string, ValidationInfo> getValidationInfo<T>()
        {
            return Validate<T>(null, null);
        }

        private static Dictionary<string, ValidationInfo> Validate<T>(IEnumerable<T> all, Func<T, string> selector)
        {

            PropertyInfo[] properties = typeof(T).GetProperties();
            var valInfo = new Dictionary<string, ValidationInfo>();
            ValidationInfo vi;

            foreach (PropertyInfo pi in properties)
            {
                vi = new ValidationInfo();

                SetValidation<StringLengthAttribute>(pi, length =>
                {
                    vi.length.min = length.MinimumLength;
                    vi.length.max = length.MaximumLength;
                    vi.length.error_msg = String.Format("The {0} field must be a string with a minimum length of {1} and maximum length of {2}.", pi.Name, length.MinimumLength, length.MaximumLength);
                },
                () => vi.length = null);

                SetValidation<RequiredAttribute>(pi, r =>
                {
                    vi.required.error_msg = String.Format("The {0} field is required.", pi.Name);
                },
                () => vi.required = null);

                SetValidation<RegularExpressionAttribute>(pi, v =>
                {
                    vi.valid_chars.regex = v.Pattern;
                    vi.valid_chars.error_msg = v.ErrorMessage;
                },
                () => vi.valid_chars = null);

                if (all == null) vi.names = null;
                else
                    SetValidation<KeyAttribute>(pi, v =>
                    {
                        vi.names.existing_names = all.Select(selector);
                        vi.names.error_msg = "The name already exists.";
                    },
                    () => vi.names = null);

                valInfo.Add(pi.Name, vi);

            }

            return valInfo;

        }

        internal class CardUrlInfo
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Url { get; set; }
        }


        internal class ValidationInfo
        {
            private Length _length;
            private Required _required;
            private ValidChars _valid_chars;
            private Names _names;

            internal ValidationInfo()
            {
                _length = new Length();
                _required = new Required();
                _valid_chars = new ValidChars();
                _names = new Names();
            }

            internal class Length
            {
                public int min { get; set; }
                public int max { get; set; }
                public string error_msg { get; set; }
            }

            internal class Required
            {
                public string error_msg { get; set; }
            }

            internal class ValidChars
            {
                public string regex { get; set; }
                public string error_msg { get; set; }
            }

            internal class Names
            {
                public IEnumerable<string> existing_names { get; set; }
                public string error_msg { get; set; }
            }

            public Length length { get { return _length; } set { _length = value; } }
            public Required required { get { return _required; } set { _required = value; } }
            public ValidChars valid_chars { get { return _valid_chars; } set { _valid_chars = value; } }
            public Names names { get { return _names; } set { _names = value; } }

        }

        internal static string UploadProfileImage(HttpPostedFileBase file)
        {
            if (file == null)
                throw new ArgumentNullException("file", "O argumento passado ao método UploadProfileImage não pode ser null");

            string bucket = ConfigurationManager.AppSettings["AWSBucketProfileImages"];
            string imageName = GenerateImageName(file.FileName);
            Stream stream = resizeImage(file, 150, 150);

            UploadFile(stream, imageName, bucket);

            return imageName;

        }

        public static string GetProfileImageUrl(string imageFileName)
        {
            if (imageFileName == null) return "";
            return GetFileUrl(imageFileName, ConfigurationManager.AppSettings["AWSBucketProfileImages"]);
        }

        private static string GenerateImageName(string extension)
        {
            return Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(extension);
        }

        private static Stream resizeImage(HttpPostedFileBase file, int maxHeight, int maxWidth)
        {
            Image image = Image.FromStream(file.InputStream);

            if (image.Height > maxHeight || image.Width > maxWidth)
            {
                var ratioX = (double)maxWidth / image.Width;
                var ratioY = (double)maxHeight / image.Height;
                var ratio = Math.Min(ratioX, ratioY);

                var newWidth = (int)(image.Width * ratio);
                var newHeight = (int)(image.Height * ratio);

                var newImage = new Bitmap(newWidth, newHeight);
                Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);

                Stream resizedStream = new MemoryStream();

                newImage.Save(resizedStream, ImageFormat.Png);

                return resizedStream;
            }

            return file.InputStream;
        }

        private static void UploadFile(Stream stream, string key, string bucket)
        {
            string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

            using (AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(AWSAccessKey, AWSSecretKey))
            {
                PutObjectRequest request = new PutObjectRequest();
                request.WithBucketName(bucket);
                request.WithKey(key);
                request.WithInputStream(stream);
                request.AutoCloseStream = true;
                request.CannedACL = S3CannedACL.PublicRead;

                client.PutObject(request);
            }
        }

        private static string GetFileUrl(string key, string bucket)
        {
            if (key == null) return "";

            string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

            using (AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(AWSAccessKey, AWSSecretKey))
            {
                GetPreSignedUrlRequest get = new GetPreSignedUrlRequest()
                    .WithBucketName(bucket)
                    .WithKey(key)
                    .WithExpires(DateTime.Now.AddMinutes(30));

                return client.GetPreSignedURL(get);
            }
        }

    }
}