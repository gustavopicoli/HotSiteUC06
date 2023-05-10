using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//Primeiro passo: adicionar o uso da extensão no nosso projeto
using Microsoft.Extensions.Logging;

namespace Hotsite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Segundo passo: acrescentar um terceiro parâmetro aqui na nossa
        //configuração utilizando a classe ILoggerFactory
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logFac)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            // Terceiro passo: utilizar o objeto (logFac) que recebemops como parâmetro para criar um novo arquivo de log. A função "AddFile" nos permite criar um arquivo do zero com base nas informações definidas. Nessa função nós precisamos adicionar duas informações: o nome e o tipo do nosso arquivo e o nível em que esses registros serão habilitados

            logFac.AddFile("Logs/log-{Date}.txt", LogLevel.Trace);
            
            // Definimos uma pasta a ser utilizada como padrão (Logs), colocamos um nome e adicionaremos a data atual pela função DATE. No logLevel definimos o tipo de log que vamos registrar
        }
    }
}
