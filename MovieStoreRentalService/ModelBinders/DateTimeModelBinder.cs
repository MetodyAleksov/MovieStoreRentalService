using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MovieStoreRentalService.ModelBinders;

public class DateTimeModelBinder : IModelBinder
{
    private readonly string customDateFormat = "";

    public DateTimeModelBinder(string dateFormat)
    {
        customDateFormat = dateFormat;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ValueProviderResult valueResult = bindingContext
            .ValueProvider
            .GetValue(bindingContext.ModelName);

        if (valueResult != ValueProviderResult.None || !String.IsNullOrEmpty(valueResult.FirstValue))
        {
            DateTime actualValue = DateTime.MinValue;
            bool wasSuccessful = false;

            string dateValue = valueResult.FirstValue;

            try
            {
                actualValue = DateTime
                    .ParseExact(
                        dateValue, customDateFormat, CultureInfo.InvariantCulture);

                wasSuccessful = true;
            }
            catch (FormatException ex)
            {
                try
                {
                    actualValue = DateTime.Parse(dateValue, new CultureInfo("bg-bg"));
                }
                catch (Exception e)
                {
                    bindingContext
                        .ModelState
                        .AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                }
            }
            catch (Exception e)
            {
                bindingContext
                    .ModelState
                    .AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
            }

            if (wasSuccessful)
            {
                bindingContext.Result = ModelBindingResult.Success(actualValue);
            }
        }

        return Task.CompletedTask;
    }
}