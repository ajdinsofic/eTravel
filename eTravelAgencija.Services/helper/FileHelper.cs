using System.IO;

namespace eTravelAgencija.Services.helper
{
    public static class FileHelper
    {
        public static string GetMimeType(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();

            return ext switch
            {
                ".pdf"  => "application/pdf",
                ".doc"  => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream"
            };
        }
    }
}
