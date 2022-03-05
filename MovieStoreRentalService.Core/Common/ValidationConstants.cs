namespace MovieStoreRentalService.Core;

public class ValidationConstants
{
    public const int RENTAL_NAME_MAX_L = 100;
    public const int RENTAL_NAME_MIN_L = 2;

    public const int RENTAL_IMAGEURL_MIN_L = 10;
    public const int RENTAL_IMAGEURL_MAX_L = 2084;

    public const int RENTAL_AMOUNTAVAILABLE_MIN = 2;
    public const int RENTAL_AMOUNTAVAILABLE_MAX = 1000;

    public const decimal RENTAL_PRICE_MIN = 2;
    public const decimal RENTAL_PRICE_MAX = 100;
}