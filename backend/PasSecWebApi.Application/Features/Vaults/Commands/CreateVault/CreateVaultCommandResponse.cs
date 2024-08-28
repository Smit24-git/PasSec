using PasSecWebApi.Application.Responses;

namespace PasSecWebApi.Application.Features.Vaults.Commands.CreateVault
{
    public  class CreateVaultCommandResponse : BaseResponse
    {
        public Guid VaultId {  get; set; }
        public string VaultName { get; set; } = string.Empty;
    }
}
