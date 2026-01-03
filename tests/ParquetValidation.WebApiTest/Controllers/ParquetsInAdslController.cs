using Azure.Identity;
using Azure.Storage.Files.DataLake;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Parquet;

namespace ParquetValidation.WebApiTest.Controllers;

[ApiController]
[Route("parquets-in-adsl")]
public class ParquetsInAdslController(ILogger<ParquetsInAdslController> logger) : ControllerBase
{
    [HttpPost("validation")]
    public async Task<Results<Ok<List<ValidationFailure>>,NotFound>> ValidateParquet([FromBody] ParquetValidationRequest request)
    {
        var adlClient = CreateAdlsClientInternal(
            accountName: request.Credentials.AccountName,
            tenantId: request.Credentials.TenantId,
            clientId: request.Credentials.ClientId,
            clientKey: request.Credentials.ClientSecret);

        var response = await adlClient.GetFileSystemClient(request.ContainerName).GetFileClient(request.ParquetPath).ReadAsync();
        var rawResponse = response.GetRawResponse();
        if (rawResponse.IsError)
            return TypedResults.NotFound();

        using var memoryStream = CloneToMemoryStream(response.Value.Content);
        using var parquetReader = await ParquetReader.CreateAsync(memoryStream);
        var validationResult = await request.Schema.Validate(parquetReader, 2);
        return TypedResults.Ok(validationResult.Errors);
    }

    private DataLakeServiceClient CreateAdlsClientInternal(string accountName, string tenantId, string clientId, string clientKey)
    {
        var dataLakeUri = new Uri("https://" + accountName);
        logger.LogDebug("Datalake uri to assing: {Uri}", dataLakeUri);

        var tokenCredential = new ClientSecretCredential(tenantId, clientId, clientKey);
        return new DataLakeServiceClient(dataLakeUri, tokenCredential);
    }

    private static MemoryStream CloneToMemoryStream(Stream stream)
    {
        var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        memoryStream.Position = 0L;
        return memoryStream;
    }

}
