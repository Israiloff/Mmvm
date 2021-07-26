using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Core.Resolving.Pipeline;
using Israiloff.Mmvm.Net.Core.Services.Serializer;

namespace Israiloff.Mmvm.Net.Container.Impl.Module.Nlog
{
    public abstract class LogModule<TLogger> : Autofac.Module
    {
        protected abstract TLogger CreateLoggerFor(Type type);

        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistryBuilder,
            IComponentRegistration registration)
        {
            componentRegistryBuilder.Registered += InjectLoggerViaProperty;
            componentRegistryBuilder.Registered += InjectLoggerViaConstructor;
        }

        private void InjectLoggerViaConstructor(object sender, ComponentRegisteredEventArgs e)
        {
            e.ComponentRegistration.PipelineBuilding += (o, builder) =>
            {
                builder.Use(PipelinePhase.ParameterSelection, MiddlewareInsertionMode.StartOfPhase,
                    (context, action) =>
                    {
                        action(context);
                        var type = context.Instance?.GetType();
                        context.ChangeParameters(
                            new[]
                            {
                                new ResolvedParameter((info, componentContext) =>
                                        info.ParameterType == typeof(TLogger),
                                    (p, i) =>
                                        CreateLoggerFor(type)),
                                new ResolvedParameter((info, componentContext) =>
                                        info.ParameterType == typeof(ISerializer),
                                    (info, componentContext) =>
                                        componentContext.Resolve<ISerializer>()),
                            });
                    });
            };
        }

        private void InjectLoggerViaProperty(object sender, ComponentRegisteredEventArgs e)
        {
            e.ComponentRegistration.PipelineBuilding += (o, builder) =>
            {
                builder.Use(PipelinePhase.Activation, MiddlewareInsertionMode.EndOfPhase, (context, action) =>
                {
                    action(context);
                    var type = context.Instance?.GetType();
                    var propertyInfo = type?.GetProperties()
                        .First(x => x.CanWrite && x.PropertyType == typeof(TLogger));
                    propertyInfo?.SetValue(context.Instance, CreateLoggerFor(type), null);
                });
            };
        }

        private bool HasPropertyDependencyOnLogger(Type type)
        {
            return type.GetProperties().Any(property => property.CanWrite && property.PropertyType == typeof(TLogger));
        }

        private bool HasConstructorDependencyOnLogger(Type type)
        {
            return type.GetConstructors()
                .SelectMany(constructor => constructor.GetParameters()
                    .Where(parameter => parameter.ParameterType == typeof(TLogger)))
                .Any();
        }
    }
}