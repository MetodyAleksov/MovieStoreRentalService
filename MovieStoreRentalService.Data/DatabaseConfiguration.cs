namespace MovieStoreRentalService.Data;

public static class DatabaseConfiguration
{
    public static string ConnectionString
        => @"Server=.;Database=MovieStoreRental;Trusted_Connection=True;Integrated Security=True;";
}