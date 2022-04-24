using MovieStoreRentalService.Core;
using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.Data.Models;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.DTO.Common.Enums;
using System.Text;

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

    public async Task<(bool, string)> AddRental(RentalDTO dto)
    {
        (bool isValid, string errors) =  ValidateModel(dto);


        if (isValid)
        {
            Data.Models.Rentals rental = new Data.Models.Rentals()
            {
                ImageUrl = dto.ImageURL,
                Name = dto.Name,
                AmountAvailable = dto.AmountAvailable,
                Price = dto.Price,
                Type = dto.RentalType.ToString(),
                Description = dto.Description,
                TimeAdded = DateTime.Now
            };

            MovieDirector director;

            if (repo.All<MovieDirector>().Any(x => x.Name == dto.DirectorName))
            {
                director = repo.All<MovieDirector>().First(d => d.Name == dto.DirectorName);
            }
            else
            {
                director = new MovieDirector()
                {
                    Name = dto.DirectorName
                };
            }

            rental.MovieDirector = director;
            rental.MovieDirectorId = director.Id;

            await repo.AddAsync(rental);
            await repo.SaveChangesAsync();

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
                Description = r.Description,
                TimeAdded = r.TimeAdded,
                DirectorName = r.MovieDirector.Name
            });
    }

    public (bool, RentalDTO) FindById(string id)
    {
        var rental = repo.All<Data.Models.Rentals>()
            .SingleOrDefault(d => d.Id == id);
        bool isValid = true;
        RentalDTO dto = null;

        if (rental == null)
        {
            isValid = false;
        }
        else
        {
            Enum.TryParse(rental.Type, true, out RentalType rentalType);

            dto = new RentalDTO()
            {
                Id = rental.Id,
                AmountAvailable = rental.AmountAvailable,
                Description = rental.Description,
                ImageURL = rental.ImageUrl,
                Name = rental.Name,
                Price = rental.Price,
                RentalType = rentalType,
                TimeAdded = rental.TimeAdded,
                DirectorName = rental.MovieDirector.Name
            };
        }

        return (isValid, dto);
    }

    public void RemoveRental(string id)
    {
        repo.RemoveRental(id);
    }

    public async Task EditRental(string id, RentalDTO newData)
    {
        var rental = this.repo.All<Data.Models.Rentals>().Single(r => r.Id == id);
        rental.Name = newData.Name;
        rental.Price = newData.Price;
        rental.TimeAdded = newData.TimeAdded;
        rental.Description = newData.Description;
        rental.ImageUrl = newData.ImageURL;
        rental.AmountAvailable = newData.AmountAvailable;
        rental.MovieDirector.Name = newData.DirectorName;

        await repo.SaveChangesAsync();
    }
}