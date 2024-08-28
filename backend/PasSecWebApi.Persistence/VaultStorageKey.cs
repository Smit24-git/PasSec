using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Persistence
{
    public class VaultStorageKey
    {
        public Guid VaultStorageKeyId { get; set; }
        public Guid VaultId {  get; set; }
        public string? Username { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? EmailAddress { get; set; }
        public string? AccessLocation { get; set; } 
        public string IV { get; set; } = string.Empty;
        public string AddedBy { get; set; } = string.Empty;
        public DateTime AddedAt { get; set; }

        public virtual Vault? Vault { get; set; }

        public List<VaultStorageKeySecurityQA>? SecurityQA { get; set; }


    }
}
