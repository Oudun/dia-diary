using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace TypeOneControl
{

    public class AppDataContext : DataContext {

        public static string DBConnectionString = "Data Source=isostore:/AppData.sdf";
        //public static string DBConnectionString = "Data Source=appdata:/AppData.sdf; File Mode = read only;";

        public AppDataContext (string connectionString) : base(connectionString) {

        }

        public Table<GlucoseRecord> GlucoseRecordsTable;
        public Table<ShotRecord> ShotRecordsTable;
        public Table<Insulin> InsulinsTable;
        public Table<MealRecord> MealRecordsTable;
        public Table<Meal> MealsTable;

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

    [Table]
    public class ShotRecord {

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public float Value { get; set; }

        [Column]
        public int InsulinId { get; set; }

        [Column]
        public DateTime DateTime { get; set; }

    }

    [Table]
    public class Insulin {

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string ResourceCode { get; set; }

    }

    [Table]
    public class MealRecord {

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public float Quantity { get; set; }

        [Column]
        public int MealId { get; set; }

        [Column]
        public DateTime DateTime { get; set; }
    
    }

    [Table]
    public class Meal {

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string ResourceCode { get; set; }

        [Column]
        public int UnitId { get; set; }

        [Column]
        public int BreadUnits { get; set; }

    }

    [Table]
    public class ActivityRecord {

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public int ActivityId { get; set; }

        [Column]
        public DateTime Start { get; set; }

        [Column]
        public DateTime End { get; set; }

    }

    [Table]
    public class Activity {

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string ResourceCode { get; set; }

    }

    [Table]
    public class Settings {

        [Column]
        public string Name { get; set; }

        [Column]
        public string Value { get; set; }

    }

    [Table]
    public class GlucoseUnit {

        [Column]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string ResourceCode { get; set; }

        [Column]
        public bool Active { get; set; }

    }

    [Table]
    public class MealUnit {

        [Column]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string ResourceCode { get; set; }

    }


}


