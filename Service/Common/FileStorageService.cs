using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace Service.Common
{
    public class FileStorageService : IStorageService
    {
        private readonly string _userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private readonly ILogger<FileStorageService> _logger;

        public FileStorageService(IWebHostEnvironment webHostEnvironment, ILogger<FileStorageService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            if (webHostEnvironment == null)
            {
                _logger.LogError("Web host environment is null");
                throw new ArgumentNullException(nameof(webHostEnvironment), "Web host environment cannot be null");
            }

            if (string.IsNullOrEmpty(webHostEnvironment.WebRootPath))
            {
                _logger.LogError("WebRootPath is null or empty");
                throw new ArgumentException("WebRootPath cannot be null or empty", nameof(webHostEnvironment.WebRootPath));
            }

            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
            _logger.LogInformation($"User content folder path: {_userContentFolder}");
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }

}
