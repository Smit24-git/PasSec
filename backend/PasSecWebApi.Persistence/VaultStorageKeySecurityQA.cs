using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Persistence
{
    public class VaultStorageKeySecurityQA
    {
        public Guid VaultStorageKeySecurityQAId { get; set; }
        public Guid VaultStorageKeyId { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string IV { get; set; } = string.Empty;
        public string AddedBy { get; set; } = string.Empty;
        public DateTime AddedAt { get; set; }
        
        public virtual VaultStorageKey? VaultStorageKey { get; set; }


    }
}
