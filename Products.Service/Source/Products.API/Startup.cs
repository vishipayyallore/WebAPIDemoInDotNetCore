using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Core;
using Products.Data;
using Products.Domain;
using System.Collections.Generic;

namespace Products.API
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

            services.AddMvc();

            services.AddScoped<IProductsContext, ProductsContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            AddTestDataForInMemory();

        }

        private void AddTestDataForInMemory()
        {
            var productsContext = new ProductsContext();
            productsContext.AddRangeAsync(
             new[]
            {
                new Product {  Name = "Tomato Soup", Category = "Groceries", Price = 1 },
                new Product {  Name = "Yo-yo", Category = "Toys", Price = 3.75M },
                new Product {  Name = "Hammer", Category = "Hardware", Price = 16.99M }
            });

            productsContext.SaveChangesAsync(); 
        }


    }
}
