using EventHubPackage.Core;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace WebApi.Services;

public interface ICloudStorageSingletonService
{
	Task<string> UploadFile(Stream stream, string folder = "");
	Task<string> ReplaceFile(string? fileNameForStorage, Stream stream, string folder = "");

	Task DeleteFile(string? fileNameForStorage);

	Task<string?> GetPhoto(string? filename);
	Task<string?> DuplicateAsync(string? filename);
}

public class CloudStorageSingletonService : ICloudStorageSingletonService
{
	private readonly StorageClient _storageClient;
	private readonly UrlSigner _urlSigner;
	private readonly string _bucket = MyEnvironment.Bucket;
	private readonly string _folder = MyEnvironment.Folder;

	public CloudStorageSingletonService()
	{
		var googleCredential = GoogleCredential.FromJson(MyEnvironment.CredentialJson);
		_storageClient = StorageClient.Create(googleCredential);
		_urlSigner = UrlSigner.FromCredential(googleCredential);
	}

	public async Task<string?> GetPhoto(string? filename)
	{
		if (filename == null) return null;
		return await _urlSigner.SignAsync(_bucket, filename, TimeSpan.FromHours(1));
	}

	public async Task<string?> DuplicateAsync(string? filename)
	{
		if (filename == null) return null;
		var newFilename = _generateFilename();
		await _storageClient.CopyObjectAsync(_bucket, filename, _bucket, newFilename);
		return newFilename;
	}
	public async Task<string> UploadFile(Stream stream, string folder = "")
	{
		var filename = _generateFilename(folder);
		await _storageClient.UploadObjectAsync(_bucket, filename, null, stream);
		return filename;
	}

	public async Task<string> ReplaceFile(string? fileNameForStorage, Stream stream, string folder = "")
	{
		if (fileNameForStorage == null) return await UploadFile(stream, folder);
		if (fileNameForStorage.StartsWith(_folder)) await DeleteFile(fileNameForStorage);
		return await UploadFile(stream, folder);
	}

	public async Task DeleteFile(string? fileNameForStorage)
	{
		if (fileNameForStorage == null) return;
		if (!fileNameForStorage.StartsWith(_folder)) return;
		try
		{
			await _storageClient.DeleteObjectAsync(_bucket, fileNameForStorage);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}

	private string _generateFilename(string folder = "")
	{
		var date = DateTime.Now.ToString("yy-MM-dd");
		var guid = Guid.NewGuid();

		return $"{_folder}{folder}{date}_{guid}.png";
	}
}