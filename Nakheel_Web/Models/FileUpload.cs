using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Nakheel_Web.Models
{
    public static class FileUpload
    {
        public static async Task<string> UploadSingleFile(List<IFormFile> files, string conn, HttpClient client)
        {
            try
            {
                string? path = "";

                var multipartFormDataContent = new MultipartFormDataContent();
                foreach (IFormFile file in files)
                {
                    byte[] fileData;
                    using (var reader = new BinaryReader(file.OpenReadStream()))
                    {
                        fileData = reader.ReadBytes((int)file.OpenReadStream().Length);
                    }
                    var fileContent = new ByteArrayContent(fileData);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                    multipartFormDataContent.Add(fileContent, "formFiles", file.FileName);

                }

                HttpResponseMessage response = await client.PostAsync(conn, multipartFormDataContent).ConfigureAwait(false);
                string customerJsonString = await response.Content.ReadAsStringAsync();
                string[] deserialized = JsonConvert.DeserializeObject<string[]>(customerJsonString)!;
                if (deserialized != null)
                {
                    path = deserialized[0];
                }

                return path;
            }
            catch (Exception ex)
            {

                return "Failed";
            }
        }
        public static async Task<string[]> UploadMultipleFiles(List<IFormFile> Files, string conn, HttpClient client)
        {

            try
            {
                var multipartFormDataContent = new MultipartFormDataContent();
                foreach (IFormFile file in Files)
                {
                    byte[] fileData;
                    using (var reader = new BinaryReader(file.OpenReadStream()))
                    {
                        fileData = reader.ReadBytes((int)file.OpenReadStream().Length);
                    }
                    var fileContent = new ByteArrayContent(fileData);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                    multipartFormDataContent.Add(fileContent, "formFiles", file.FileName);
                }

                HttpResponseMessage response = await client.PostAsync(conn, multipartFormDataContent).ConfigureAwait(false);
                string customerJsonString = await response.Content.ReadAsStringAsync();
                string[] deserialized = JsonConvert.DeserializeObject<string[]>(customerJsonString)!;
                return deserialized;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
