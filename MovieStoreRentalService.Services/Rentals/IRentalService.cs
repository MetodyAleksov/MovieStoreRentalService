﻿using MovieStoreRentalService.DTO;

namespace MovieStoreRentalService.Services.Rentals;

public interface IRentalService
{
    (bool, string) AddRental(RentalDTO dto);
}