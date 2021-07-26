using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Core.Resolving.Pipeline;
using Israiloff.Mmvm.Net.Core.Impl.Services.Logger;
using Israiloff.Mmvm.Net.Core.Services.Logger;

namespace Israiloff.Mmvm.Net.Container.Impl.Module.Nlog.ConcreteLogger
{
    public class NlogModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NLogLogger>().As<ILogger>();
        }

        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry,
            IComponentRegistration registration)
        {
            var type = registration.Activator.LimitType;

            registration.PipelineBuilding += (sender, builder) =>
            {
                builder.Use(PipelinePhase.ParameterSelection, MiddlewareInsertionMode.StartOfPhase,
                    (context, action) =>
                    {
                        var logParameter = new ResolvedParameter(
                            (p, c) => p.ParameterType == typeof(ILogger),
                            (p, c) => c.Resolve<ILogger>(TypedParameter.From(type)));
                        context.ChangeParameters(context.Parameters.Union(new[] {logParameter}).ToList());
                        action(context);
                    });
            };
        }
    }
}