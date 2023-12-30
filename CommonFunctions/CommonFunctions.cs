using Microsoft.AspNetCore.Components.Forms;
using System.Text;
using System.Security.Cryptography;

namespace EmptyProject.CommonFunctions
{
    public class CommonFunctions
    {
        
        public long MaxFileSize { get; } = 1024L * 1024L; //3MB max.

        public async Task<bool> SaveFileInLocalServer(IBrowserFile file)
        {
            try
            {
                string path = Path.Combine(
                    Environment.CurrentDirectory,
                    "wwwroot",
                    "Uploads",
                    file.Name);

                await using FileStream FileStream = new(path, FileMode.Create);
                await file.OpenReadStream(MaxFileSize).CopyToAsync(FileStream);

                FileStream.Close();
            }
            catch (Exception) { throw; }
            
            return true;
        }

        public string EncodeString(string stringToEncode)
        {
            try
            {
                UnicodeEncoding Encoder = new();

                string EncodedString = Convert
                    .ToBase64String(SHA512
                    .HashData(Encoder
                    .GetBytes(stringToEncode)));

                return EncodedString;
            }
            catch (Exception) { throw; }
        }
    }
}
