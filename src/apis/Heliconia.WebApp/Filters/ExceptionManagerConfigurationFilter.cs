using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heliconia.WebApp.Filters
{
    public class ExceptionManagerConfigurationFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public ExceptionManagerConfigurationFilter(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is not null)
            {
                context.Result = new JsonResult(new ExceptionDTO
                {
                    Mensaje = context.Exception.Message,
                    Tipo = context.Exception.GetType().ToString(),
                    Aplicacion = this.webHostEnvironment.ApplicationName
                });
            }
        }
    }
}
