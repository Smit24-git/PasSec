namespace PasSecWebApi.Shared.Dtos
{
    public class VaultStorageKeySecurityQADto
    {
        public Guid VaultStorageKeySecurityQAId { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}
