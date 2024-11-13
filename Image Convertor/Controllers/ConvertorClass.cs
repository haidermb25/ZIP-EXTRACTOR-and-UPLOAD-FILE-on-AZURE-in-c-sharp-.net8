using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Image_Convertor.DBContext;
using Image_Convertor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System.IO;
using System.IO.Compression;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Image_Convertor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertorClass : ControllerBase
    {
        private readonly packContext _packContext;
        //Inject the dbcontext file
        public ConvertorClass(packContext packContext)
        {
            _packContext = packContext;
        }
        // POST api/<ValuesController>/upload
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostData([FromForm] fileUpload data)
        {
            // Check if the file is null or empty

            if (data.file == null || data.file.Length == 0)
            {
                return BadRequest("No file uploaded or the file is empty.");
            }

            var extractedFiles = new List<string>();
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\ali.haider\\Desktop\\Icon Project\\iconProject\\src\\assets\\uploads");
            var fileExtension = Path.GetExtension(data.file.FileName).ToLower();
            string azureConnectionString = "DefaultEndpointsProtocol=https;AccountName=iconproject;AccountKey=8HDxW4+ioSgc0B0IsKLTEDN7KE50Px7YP459fzmFwSNEa4Fr4V+cRtiZnO60VuxsEOz8059AiLcG+AStcMYVjQ==;EndpointSuffix=core.windows.net";
            string containerName = "iconimages";

            //Create the object of the blob service and container
            BlobServiceClient blobServiceClient = new BlobServiceClient(azureConnectionString);
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            if (fileExtension == ".zip")
            {
                using (var memoryStream = new MemoryStream())
                {
                    data.file.CopyTo(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
                    {
                        foreach (var entry in zipArchive.Entries)
                        {
                            if (!entry.FullName.EndsWith("/")) // Exclude directories
                            {
                                var fileStream = new MemoryStream();
                                using (var entryStream = entry.Open())
                                {
                                   await entryStream.CopyToAsync(fileStream);
                                }
                                fileStream.Position=0;

                                string blobImageName = entry.Name;
                                string zippingFileExtensions = Path.GetExtension(entry.FullName).ToLower();
                                string contentType = "image/svg+xml";
                                //Handle content type according to the file extension type
                                switch (zippingFileExtensions)
                                {
                                    case ".png":
                                        contentType = "image/png";
                                        break;
                                    case ".svg":
                                        contentType = "image/svg+xml";
                                        break;
                                    case ".jpg":
                                    case ".jpeg":
                                        contentType = "image/jpeg";
                                        break;
                                    case ".gif":
                                        contentType = "image/gif";
                                        break;
                                    default:
                                        continue;
                                }

                                BlobClient blobClient=blobContainerClient.GetBlobClient(blobImageName);
                                await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
                                string Url=blobClient.Uri.ToString();
                                extractedFiles.Add(Url);
                            }
                        }
                    }
                }
                return Ok(new { Message = "File uploaded and extracted successfully", Files = extractedFiles });
            }
            else if (fileExtension == ".svg")
            {
                // Upload SVG file to Azure Blob Storage
                string blobFileName = data.file.FileName;
                BlobClient blobClient = blobContainerClient.GetBlobClient(blobFileName);

                using (var stream = data.file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = "image/svg+xml" });
                }

                // Return the blob URL
                string blobUrl = blobClient.Uri.ToString();
                return Ok(new { Message = "SVG file has been uploaded successfully!", FileUrl = blobUrl });
            }
            else
            {
                return BadRequest("Unsupported file type. Please upload a .zip or .svg file.");
            }
        }


        //Here we submit the pack data in the database
        [HttpPost("submitPackData")]
        public async Task<IActionResult> SubmitPackData([FromBody] IconPack pack)
        {
            // Validate model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the pack to the context
            await _packContext.AddAsync(pack);

            // Save changes to the database
            await _packContext.SaveChangesAsync();

            // The ID property will now have the value generated by the database
            var packId = pack.ID; // Assuming 'ID' is the property name in your IconPack model

            return Ok(new { Message = "Icon Pack submitted successfully.", ID = packId });
        }


        //Here we submit the icons data for the iconPack

        [HttpPost("submitIconsData")]
        public async Task<IActionResult> SubmitIconsData([FromBody] List<Icon> icons)
        {
            // Validate the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add icons to the database
            await _packContext.Icons.AddRangeAsync(icons);

            // Save changes to the database
            await _packContext.SaveChangesAsync();

            return Ok(new { Message = "Icons saved successfully!" });
        }

        [HttpPost("GetIcons")]
        public async Task<IActionResult> GetIcons([FromBody] string searchString)
        {
            // Fetching icon packs along with their associated icons
            var iconPacks = await _packContext.Pack
                .Include(ip => ip.ID) 
                .ToListAsync();

            // Return the data
            return Ok(iconPacks);
        }


    }
}
