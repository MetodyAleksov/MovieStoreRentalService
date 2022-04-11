namespace MovieStoreRentalService.Data;

public static class DatabaseConfiguration
{
    public static string ConnectionString
        => @"Server=tcp:moviestorerentalservicedbserver.database.windows.net,1433;Initial Catalog=MovieStoreRentalService_db;Persist Security Info=False;User ID=teddy;Password=Metodi2003;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
}