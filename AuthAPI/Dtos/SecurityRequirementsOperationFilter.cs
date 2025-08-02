using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AuthAPI.Dtos
{

    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Verifica si el endpoint ya tiene un requisito de seguridad
            var hasAllowAnonymous = context.ApiDescription.ActionDescriptor.EndpointMetadata
                .Any(x => x is IAllowAnonymous);

            if (hasAllowAnonymous)
            {
                return;
            }

            // Si no tiene [AllowAnonymous], agrega el requisito de seguridad.
            operation.Security = new List<OpenApiSecurityRequirement>
        {
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            }
        };
        }
    }
}
