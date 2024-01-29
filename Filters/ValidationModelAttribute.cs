using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Filters
{
    public class ValidationModelAttribute : ActionFilterAttribute
    {

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var apiError = new ErrorResponse();
                apiError.StatusCode = 400;
                apiError.StatusPhrase = "Bad Request";
                apiError.Timestamp = DateTime.Now;
                var errors = context.ModelState.AsEnumerable();
                foreach (var error in errors)
                {
                    foreach (var subError in error.Value!.Errors)
                    {
                        apiError.Errors.Add(subError.ErrorMessage);
                    }
                }

                context.Result = new BadRequestObjectResult(apiError);
            }
        }
     

    }
}