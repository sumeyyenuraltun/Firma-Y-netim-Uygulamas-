using System;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage.Json;
using FirmaYonetimWeb.Enum;
using FirmaYonetimWeb.Models;
using Newtonsoft.Json;


using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Helper
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }
        public EntityEntry Entry { get; }
        //public string UserId { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();

        public FirmaYonetimWeb.Models.Audit ToAudit()
        {
            var audit = new FirmaYonetimWeb.Models.Audit(); 
            audit.Type = AuditType.ToString();
            audit.TableName = TableName;
            audit.DateTime = DateTime.UtcNow;
            audit.PrimaryKey = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues.Count ==0 ? "null": JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? "null" : JsonConvert.SerializeObject(NewValues);
            audit.AffectedColumns = ChangedColumns.Count == 0 ? "null" : JsonConvert.SerializeObject(ChangedColumns);

            return audit;
        }




    }
}
