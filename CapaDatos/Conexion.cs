using System.IO;
using Microsoft.Extensions.Configuration;


namespace CapaDatos
{
    public class Conexion
    {
        public static string Con()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            var CN = root.GetConnectionString("DefaultConnection");
            return CN;
        }
    }
}
