using MediatR;
using MeetBriefly.Application.Audios.Commands;
using MeetBriefly.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeetBriefly.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(UploadAudioCommand).Assembly));
            return services;
        }
    }
}
