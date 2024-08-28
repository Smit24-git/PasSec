using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Persistence
{
    public class Vault
    {
        public Guid VaultId { get; set; }
        public string VaultName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool AppliedCustomKey { get; set; } = false;
        public string IV { get; set; } = string.Empty;
        
        public string AddedBy { get; set; } = string.Empty;
        public DateTime AddedAt { get; set; }

        public List<VaultStorageKey>? StorageKeys { get; set; }
    }
}
