using MediatR;

namespace PasSecWebApi.Application.Features.Vaults.Commands.CreateVault
{
    public class CreateVaultCommand : IRequest<CreateVaultCommandResponse>
    {
        public string VaultName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool UseUserKey { get; set; }
        public string UserKey { get; set; } = string.Empty;
        public List<CreateVaultCommandKey>? Keys { get; set; } = [];
    }
    public class CreateVaultCommandKey
    {
        public string KeyName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string AccessLocation { get; set; } = string.Empty;
        public List<CreateVaultCommandKeySecurityQuestion>? SecurityQAs { get; set; } = [];
    }
    public class CreateVaultCommandKeySecurityQuestion
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}
