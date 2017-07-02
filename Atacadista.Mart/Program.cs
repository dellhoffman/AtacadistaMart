using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Atacadista.Mart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //var host = new WebHostBuilder()
                //    .UseKestrel()
                //    .UseContentRoot(Directory.GetCurrentDirectory())
                //    .UseStartup<Startup>()
                //    .UseApplicationInsights()
                //    .Build();

                //host.Run();


                var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseKestrel()
                .UseIISIntegration()
                .Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException());
                throw;
            }
        }

        //public static void Main(string[] args)
        //{
        //    var host = new WebHostBuilder()
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .UseStartup<Startup>()
        //        .UseKestrel()
        //        .UseIISIntegration()
        //        .Build();

        //    host.Run();
        //}
    }
}
