using System;

namespace Service.Grupo.API
{
    public static class Ambiente
    {
        public static string GetAmbiente()
        {
            var env = Environment.GetEnvironmentVariable("Environment");

            env = "dev";

            return env;
        }
    }
}
