namespace PasSecWebApi.Shared.Dtos
{
    public class VaultDto
    {
        public Guid VaultId { get; set; }
        public string VaultName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<VaultStorageKeyDto>? StorageKeys { get; set; }
    }
}
