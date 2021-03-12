using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using TaskWebJob.Data;
using TaskWebJob.Repositories;

namespace TaskWebJob
{
    class Program
    {
        static void Main(string[] args)
        {
            String cnn =
                "Data Source=servidorprofe.database.windows.net;Initial Catalog=AZURETAJAMAR;Persist Security Info=True;User ID=adminsql;Password=Admin123";
            var provider =
                new ServiceCollection()
                .AddTransient<RepositoryWebJob>()
                .AddDbContext<WebJobContext>(options =>
                options.UseSqlServer(cnn)).BuildServiceProvider();
            RepositoryWebJob repo =
                provider.GetService<RepositoryWebJob>();
            //Console.WriteLine("PULSE UNA TECLA PARA INICIAR");
            //Console.ReadLine();
            repo.PopulateDataWebJob();
            //Console.WriteLine("Proceso terminado. Pulse ENTER para finalizar");
            //Console.ReadLine();
        }
    }
}
