using Microsoft.Extensions.Configuration;

namespace Service.Grupo.Application.Base.SwaggerConfig
{
    public class Swagger
    {
        public OpenApiInfo openApiInfo { get;set; }
        public SwaggerDoc SwaggerDoc { get; set; }
        public SwaggerEndpoint SwaggerEndpoint { get; set; }

        public Swagger()
        {}
    }
}
