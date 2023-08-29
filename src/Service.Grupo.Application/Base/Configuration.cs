
using Service.Grupo.Application.Base.SwaggerConfig;

namespace Service.Grupo.Application.Base
{
    public class Configuration
    {
        public ConnectionString ConnectionString { get; set; }
        
        public string AllowedHosts { get; set; }    

        public Swagger Swagger { get; set; }
    }
}
