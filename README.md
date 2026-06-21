ZIP Extractor and File Upload to Azure (.NET 8)

This project demonstrates how to extract ZIP files and upload extracted contents to Azure Blob Storage using ASP.NET Core .NET 8.

📖 About

A .NET 8 application that extracts ZIP files and uploads their contents directly to Azure Blob Storage efficiently.

🚀 Features
Upload ZIP file via API
Extract ZIP file content
Upload extracted files to Azure Blob Storage
Stream-based processing (no heavy memory usage)
Secure cloud storage integration
🏗️ Project Workflow
User uploads ZIP file
API extracts ZIP contents
Each file is processed individually
Files are uploaded to Azure Blob Storage
⚙️ Technologies Used
ASP.NET Core (.NET 8)
Azure Blob Storage
System.IO.Compression
C#
REST API
📂 Project Structure
ZIP-EXTRACTOR-and-UPLOAD-FILE-on-AZURE
│
├── Controllers
│   └── FileController.cs
│
├── Services
│   ├── ZipService.cs
│   └── AzureBlobService.cs
│
├── Models
│   └── UploadRequest.cs
│
└── Program.cs
🔧 Setup Instructions
1. Clone Repository
git clone <repo-url>
2. Configure Azure Blob Storage

Add in appsettings.json:

{
  "AzureBlob": {
    "ConnectionString": "<your-azure-connection-string>",
    "ContainerName": "<your-container-name>"
  }
}
3. Run the Project
dotnet build
dotnet run
4. Test API

Use Postman or Swagger:

POST /api/file/upload-zip
☁️ Azure Integration

This project uses:

Azure Blob Storage SDK
Container-based file upload
Secure connection string authentication
