using Autofac;
using Autofac.Core;
using webapi.Interfaces;
using webapi.MQTT;

namespace webapi
{
    public class IOCController
    {
         static IContainer _container;

        public static void RegisterImplementations()
        {
            if (_container == null)
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<MQClient>().As<IMQClient>().SingleInstance();
                _container = builder.Build();
            }
        }

        public static T GetResolver<T>()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                return scope.Resolve<T>();
            }
        }
    }
}
