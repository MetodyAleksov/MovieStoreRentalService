using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MovieStoreRentalService.ModelBinders;

public class DoubleModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ValueProviderResult valueResult = bindingContext
            .ValueProvider
            .GetValue(bindingContext.ModelName);

        if (valueResult != ValueProviderResult.None || !String.IsNullOrEmpty(valueResult.FirstValue))
        {
            double actualValue = 0;
            bool wasSuccessful = false;

            try
            {
                string value = valueResult.FirstValue;
                value = value
                    .Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                value = value
                    .Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                actualValue = Convert.ToDouble(value, CultureInfo.CurrentCulture);
                wasSuccessful = true;
            }
            catch (FormatException ex)
            {
                bindingContext
                    .ModelState
                    .AddModelError(bindingContext.ModelName, ex, bindingContext.ModelMetadata);
            }

            if (wasSuccessful)
            {
                bindingContext.Result = ModelBindingResult.Success(actualValue);
            }
        }

        return Task.CompletedTask;
    }
}