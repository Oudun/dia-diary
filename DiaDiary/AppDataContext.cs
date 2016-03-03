using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace PhoneAppDB {

    public class AppDataContext : DataContext {

        public static string DBConnectionString = "Data Source=isostore:/AppData.sdf";

        public AppDataContext (string connectionString) : base(connectionString) {

        }

        public Table<GlucoseRecord> GlucoseRecordsTable;

        public ObservableCollection<GlucoseRecord> GlucoseRecords;

    }

    [Table]
    public class GlucoseRecord {

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int GlucoseRecordId { get; set; }

        [Column]
        public float GlucoseRecordValue { get; set; }

        [Column]
        public DateTime GlucoseTime { get; set; }
        
    }

}


