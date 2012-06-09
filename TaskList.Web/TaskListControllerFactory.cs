using System;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;

namespace TaskList.Web
{
    public class TaskListControllerFactory : DefaultControllerFactory
    {
        private readonly IContainer _container;

        public TaskListControllerFactory(IContainer container)
        {
            _container = container;
        }

        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            var controller = base.GetControllerType(requestContext, controllerName);
            if (controller == null)
            {
                object x;
                if (_container.TryResolveNamed(controllerName, typeof(IController), out x))
                    controller = x.GetType();
            }

            return controller;
        }
    }
}