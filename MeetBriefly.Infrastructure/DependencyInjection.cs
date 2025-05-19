using MeetBriefly.Core.Interfaces;
using MeetBriefly.Infrastructure.Converters;
using MeetBriefly.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetBriefly.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAudioService, AudioService>();
            services.AddScoped<ITextSumarizationService, TextSumarizationService>();
            services.AddScoped<IAudioConverter, AudioConverter>();
            return services;
        }
    }
}
