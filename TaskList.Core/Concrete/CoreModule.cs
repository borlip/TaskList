

using Autofac;
using Autofac.Integration.Mvc;

namespace TaskList.Core.Concrete
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TaskListDbContext>().InstancePerHttpRequest();
            builder.RegisterType<TaskRepository>().As<ITaskRepository>();
        }
    }
}