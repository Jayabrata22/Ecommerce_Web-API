using AutoMapper;
using Business.Interfaces;
using Business.Services;
using Ecommerce.Business.Serviceinterface;
using Ecommerce.Business.ServiceRepository;
using Ecommerce.DataAccess;
using Ecommerce.DataAccess.Implentation;
using Ecommerce.DataAccess.Implentation.Customer;
using Ecommerce.DataAccess.Implentation.Seller;
using Ecommerce.DataAccess.Interface;
using Ecommerce.DataAccess.Interface.Customer;
using Ecommerce.DataAccess.Interface.Seller;
using Ecommerce.Models.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Seeder;

namespace Ecommerce.WebAPI
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
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IUserInterface, UserAuthRepository>();
            services.AddTransient<IAuthInterfaceService, AuthServiceRepository>();
            services.AddTransient<IProductInterface, SellerProductRepository>();
            services.AddTransient<IProductServiceInterface, ProductServiceRepository>();
            services.AddTransient<IProductCusInterface, CustomerProductRepository>();
            services.AddTransient<IOrderInterface, orderRepository>();
            services.AddTransient<IOrderServiceInterface, OrderServiceRepository>();
            services.AddTransient<ICartInterface, CartRepository>();
            services.AddTransient<ICartServiceInterface, CartServiceRepository>();
            services.AddTransient<ICustomerOrderInterface, CustomerOrderRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
            });
            // ✅ JWT Authentication Setup
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                };
            });

            // ✅ Session Support
            services.AddSession();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce.WebAPI", Version = "v1" });
            });
            services.AddDistributedMemoryCache(); // Required for session

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce.WebAPI v1"));
            }
              
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
