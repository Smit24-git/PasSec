namespace PasSecWebApi.Shared.Dtos
{
    public class VaultStorageKeyDto
    {
        public Guid VaultStorageKeyId { get; set; }
        public string KeyName { get; set; } = string.Empty;
        public string? Username { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? EmailAddress { get; set; }
        public string? AccessLocation { get; set; }
        public List<VaultStorageKeySecurityQADto>? SecurityQAs { get; set; }
    }
}
