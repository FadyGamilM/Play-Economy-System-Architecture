namespace Play.Catalog.Service.Configurations
{
   public class MongoDbConfig
   {
      public string Host { get; init; }
      public int Port { get; init; }
      // public string User { get; set; }
      // public string Password { get; set; }
      public string ConnectionString {
         get {
            // return $"mongodb://{User}:{Password}@{Host}:{Port}";
            return $"mongodb://{Host}:{Port}";
         }
      }      
   }
}