using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
[assembly:OwinStartup(typeof(GestionFormation.Services.MessagerieSignalR.StartUp))]
namespace GestionFormation.Services.MessagerieSignalR
{
    public class StartUp
    {
        public void Configuration (IAppBuilder app)
        {
            // Any connextion or hub wire up and config should go here
            app.MapSignalR();
        }
    }
}