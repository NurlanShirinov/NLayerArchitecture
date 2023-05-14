using Autofac;
using NLayer.Repository;
using NLayer.Service.Mapping;
using System.Reflection;
using Module = Autofac.Module;

namespace NLayer.API.Modules
{
    public class RepoServiceModule:Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly,serviceAssembly).Where(x=>x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope().InstancePerMatchingLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")) 
                .AsImplementedInterfaces().InstancePerLifetimeScope().InstancePerMatchingLifetimeScope();


            //InstancePerLifetimeScope => ASP.NET -de Scope uygundur
            //InstancePerDependency => transient - uygun gelir





        }
    }
}
