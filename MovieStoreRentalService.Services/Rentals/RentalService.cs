using System.Linq.Expressions;
using System.Text;
using MovieStoreRentalService.Core;
using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.DTO.Common.Enums;

namespace MovieStoreRentalService.Services.Rentals;

public class RentalService : IRentalService
{
    private readonly IRepository repo;

    public RentalService(IRepository repo)
    {
        this.repo = repo;
    }


    private (bool, string) ValidateModel(RentalDTO dto)
    {
        bool isValid = true;
        StringBuilder sb = new StringBuilder();

        if (dto.Name.Length > ValidationConstants.RENTAL_NAME_MAX_L ||
            dto.Name.Length < ValidationConstants.RENTAL_NAME_MIN_L)
        {
            isValid = false;
            sb.AppendLine("Username is invalid!");
        }

        if (dto.ImageURL.Length > ValidationConstants.RENTAL_IMAGEURL_MAX_L ||
            dto.ImageURL.Length < ValidationConstants.RENTAL_IMAGEURL_MIN_L)
        {
            isValid = false;
            sb.AppendLine("Image url is invalid!");
        }

        if (dto.AmountAvailable > ValidationConstants.RENTAL_AMOUNTAVAILABLE_MAX ||
            dto.AmountAvailable < ValidationConstants.RENTAL_AMOUNTAVAILABLE_MIN)
        {
            isValid = false;
            sb.AppendLine("Amount available is invalid!");
        }

        if (dto.Price > ValidationConstants.RENTAL_PRICE_MAX ||
            dto.Price < ValidationConstants.RENTAL_PRICE_MIN)
        {
            isValid = false;
            sb.AppendLine("Price is invalid!");
        }

        return (isValid, sb.ToString());
    }

    public (bool, string) AddRental(RentalDTO dto)
    {
        (bool isValid, string errors) =  ValidateModel(dto);


        if (isValid)
        {
            Data.Models.Rentals rental = new Data.Models.Rentals()
            {
                Id = dto.Id,
                ImageUrl = dto.ImageURL,
                Name = dto.Name,
                AmountAvailable = dto.AmountAvailable,
                Price = dto.Price,
                Type = dto.RentalType.ToString(),
                Description = dto.Description
            };

            repo.Add(rental);
            repo.SaveChanges();

            return (true, null);
        }

        return (false, errors);
    }

    public IEnumerable<RentalDTO> ListAllRentals()
    {
        return repo.All<Data.Models.Rentals>()
            .Select(r => new RentalDTO()
            {
                Id = r.Id,
                Name = r.Name,
                ImageURL = r.ImageUrl,
                RentalType = r.Type.ToString().ToLower() == "movie" ? RentalType.Movie : RentalType.VideoGame,
                AmountAvailable = r.AmountAvailable,
                Price = r.Price,
                Description = r.Description
            });
    }
}