using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PeterO.JsCloudPower.Startup))]
namespace PeterO.JsCloudPower
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
