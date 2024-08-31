using MediatR;
using PasSecWebApi.Persistence;

namespace PasSecWebApi.Application.Features.VaultStorageSecurityKeys.Commands.UpdateStorageKey
{
    public class UpdateStorageKeyCommand : IRequest<UpdateStorageKeyCommandResponse>
    {
        public string? UserKey { get; set; }
        public string VaultStorageKeyId { get; set; } = string.Empty;
        public string KeyName { get; set; } = string.Empty;
        public string? Username { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? AccessLocation { get; set; }
    }
}
