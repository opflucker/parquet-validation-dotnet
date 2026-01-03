namespace ParquetValidation.WebApiTest.Controllers;

public class ParquetValidationRequest
{
    public class AdlsCredentials
    {
        public required string AccountName { get; set; }
        public required string TenantId { get; set; }
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
    }

    public required JsonParquetSchema Schema { get; set; }
    public required AdlsCredentials Credentials { get; set; }
    public required string ContainerName { get; set; }
    public required string ParquetPath { get; set; }
}
