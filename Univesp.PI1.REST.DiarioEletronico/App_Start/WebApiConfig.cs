using System.Web.Http;

namespace Univesp.PI1.REST.DiarioEletronico
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "BimestreTurma",
                routeTemplate: "api/{controller}/{bimestre}/{id}",
                defaults: new { bimestre = RouteParameter.Optional, id = RouteParameter.Optional });
        }
    }
}
