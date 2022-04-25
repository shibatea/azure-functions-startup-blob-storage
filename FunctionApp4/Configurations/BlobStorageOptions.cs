namespace FunctionApp4.Configurations;

public class BlobStorageOptions
{
    public const string SectionName = "BlobStorage";
    public string Endpoint { get; set; }
    public string ContainerName { get; set; }
}