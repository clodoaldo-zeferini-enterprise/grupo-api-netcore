namespace Service.Grupo.Infrastructure.Config.SwaggerConfig
{
    public class SwaggerDoc
    {
        public string Name { get; set; }
        public OpenApiInfo OpenApiInfo { get; }
        public SwaggerEndpoint SwaggerEndpoint { get; }

        public SwaggerDoc()
        {
            OpenApiInfo = new OpenApiInfo();
            SwaggerEndpoint = new SwaggerEndpoint();
        }

        public SwaggerDoc(string name, OpenApiInfo openApiInfo, SwaggerEndpoint swaggerEndpoint)
        {
            Name = name;
            OpenApiInfo = openApiInfo;
            SwaggerEndpoint = swaggerEndpoint;
        }
    }
}
