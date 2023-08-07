using Autofac;
using Autofac.Core;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using webapi.Interfaces;
using webapi.MQTT;

namespace webapi
{
    public class IOCController
    {
        private static volatile IOCController _instance;
        //for the sake of unit testing
        public static IContainer Container { get; set; }


        public static IOCController Instance 
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                if(_instance == null)
                {
                    _instance = new IOCController();
                }
                return _instance;
            }
        }
        public void RegisterImplementations()
        {
            if (Container == null)
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<MQClient>().As<IMQClient>().SingleInstance();
                Container = builder.Build();
            }
        }

        public T GetResolver<T>()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                return scope.Resolve<T>();
            }
        }

    }
}
