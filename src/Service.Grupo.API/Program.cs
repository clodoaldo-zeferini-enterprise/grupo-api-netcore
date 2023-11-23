namespace Service.Grupo.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            Console.ReadKey();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var env = Ambiente.GetAmbiente();

                    webBuilder
                    .UseStartup<Startup>()
                    .UseEnvironment(env)
                    ;
                    
                });
    }
}
