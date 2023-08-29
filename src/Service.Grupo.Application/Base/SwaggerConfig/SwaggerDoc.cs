namespace Service.Grupo.Application.Base.SwaggerConfig
{
    public class SwaggerDoc
    {
        public string Name { get; set; }
        public OpenApiInfo OpenApiInfo { get; }
        public SwaggerEndpoint SwaggerEndpoint { get; }

        public SwaggerDoc()
        {
        }

        public SwaggerDoc(string name, OpenApiInfo openApiInfo, SwaggerEndpoint swaggerEndpoint)
        {
            Name = name;
            OpenApiInfo = openApiInfo;
            SwaggerEndpoint = swaggerEndpoint;
        }
    }
}
