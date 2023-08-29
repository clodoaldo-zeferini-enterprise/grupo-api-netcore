namespace Service.Grupo.Application.Base.SwaggerConfig
{
    public class OpenApiInfo
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public OpenApiInfo()
        {
        }
        public OpenApiInfo(string title, string version)
        {
            Title = title;
            Version = version;
        }
    }
}
