using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Momon.Biju.Web.Helpers;

public static class ModelStateHelper
{
    public static void AddValidationException(this ModelStateDictionary modelState, Exception? exception)
    {
        if (exception is not ValidationException validationException)
        {
            //TODO log
            modelState.AddModelError(string.Empty, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
            return;
        }
        
        modelState.Clear();
        
        foreach (var error in validationException.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
}