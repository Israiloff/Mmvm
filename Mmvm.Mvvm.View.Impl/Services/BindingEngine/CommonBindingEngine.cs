using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Israiloff.Mmvm.Net.Container.Attributes;
using Israiloff.Mmvm.Net.Core.Exceptions;
using Israiloff.Mmvm.Net.Core.Model.NavigationEngine;
using Israiloff.Mmvm.Net.Core.Services.Logger;
using Israiloff.Mmvm.Net.Mvvm.Core.Attributes;
using Israiloff.Mmvm.Net.Mvvm.View.Services.BindingEngine;
using Israiloff.Mmvm.Net.Mvvm.View.Services.BindingEngine.Dtos;

namespace Israiloff.Mmvm.Net.Mvvm.View.Impl.Services.BindingEngine
{
    [Service(Name = nameof(CommonBindingEngine))]
    public class CommonBindingEngine : IBindingEngine
    {
        #region Constructors

        public CommonBindingEngine(ILogger logger)
        {
            Logger = logger;
        }

        #endregion

        #region Private properties

        private ILogger Logger { get; }

        private IEnumerable<string> VmPostfixes => new List<string> {"Vm", "ViewModel"};

        #endregion

        #region IBindingEngine impl

        public ICollection<SimpleVmBindingDto> GetSimpleVmBindings(ICollection<Type> types)
        {
            Logger.Info("GetViewToVmBindings started");

            if (types == null)
            {
                Logger.Error("Types collection is null");
                throw new CommonException("Types collection can\'t be null in get view binding operation");
            }

            return GetSimpleVmViewBindings(types);
        }

        public ResourceDictionary GetVmBindingDictionary(ICollection<Type> types)
        {
            Logger.Info("GetComplexVmBindings started");

            var simpleBindings = GetSimpleVmBindings(types);
            var dictionary = new ResourceDictionary();

            simpleBindings
                .ToList()
                .ForEach(simpleBinding =>
                {
                    var complexVmBinding = GetComplexVmBinding(simpleBinding);
                    dictionary.Add(complexVmBinding.Key, complexVmBinding.Value);
                });

            Logger.Debug("GetComplexVmBindings successfully ended");
            return dictionary;
        }

        #endregion

        #region Private methods

        private ComplexVmBindingDto GetComplexVmBinding(SimpleVmBindingDto simpleVmBinding)
        {
            Logger.Debug("GetComplexVmBinding started");

            var template = new DataTemplate(simpleVmBinding.ViewModel)
            {
                VisualTree = new FrameworkElementFactory(simpleVmBinding.View)
            };

            return new ComplexVmBindingDto(new DataTemplateKey(simpleVmBinding.ViewModel), template);
        }

        private ICollection<SimpleVmBindingDto> GetSimpleVmViewBindings(ICollection<Type> types)
        {
            Logger.Debug("GetVmViewBindings started");
            return types
                .Where(type =>
                    IsImplementsInterface(type, typeof(INavigationNode))
                    || IsAttributed(type, typeof(ViewModelBindingAttribute)))
                .Select(type => CreateBindingObject(ResolveView(types, type), type))
                .Where(dto => dto != null)
                .ToList();
        }

        private Type GetTypeByRelatedTypeName(ICollection<Type> types, Type relatedType)
        {
            Logger.Debug("GetTypeByRelatedTypeName started for related type with name : {0}", relatedType.FullName);
            var name = GetPostfixFreeTypeName(relatedType, GetPostfixLength(relatedType));
            return GetTypeByName(types, name);
        }

        private Type GetTypeByName(ICollection<Type> types, string typeName)
        {
            Logger.Trace("GetTypeByName started for type with name : {0}", typeName);
            return !string.IsNullOrEmpty(typeName) ? types.FirstOrDefault(type => type.Name == typeName) : null;
        }

        private string GetPostfixFreeTypeName(Type relatedType, int postfixLength)
        {
            Logger.Trace("GetPostfixFreeTypeName started with postfix length : {0}", postfixLength);

            if (postfixLength == 0)
            {
                Logger.Warn("Postfix length value is : 0. Returning empty string");
                return string.Empty;
            }

            return relatedType.Name.Substring(0, relatedType.Name.Length - postfixLength);
        }

        private int GetPostfixLength(Type relatedType)
        {
            Logger.Trace("GetPostfixLength started");
            return VmPostfixes
                       .FirstOrDefault(postfix => IsTypeNamePostfixEquals(relatedType, postfix))
                       ?.Length
                   ?? 0;
        }

        private bool IsTypeNamePostfixEquals(Type type, string postfix)
        {
            Logger.Trace("IsTypeNamePostfixEquals started for postfix : {0}", postfix);
            return string.Equals(type.Name.Substring(type.Name.Length - postfix.Length), postfix,
                StringComparison.CurrentCultureIgnoreCase);
        }

        private Type ResolveView(ICollection<Type> types, Type relatedType)
        {
            Logger.Debug("ResolveView started for type : {0}", relatedType.FullName);
            return IsAttributed(relatedType, typeof(ViewModelBindingAttribute))
                ? GetTypeByAttribute(types, GetAttribute(relatedType))
                : GetTypeByRelatedTypeName(types, relatedType);
        }

        private Type GetTypeByAttribute(ICollection<Type> types, ViewModelBindingAttribute bindingAttribute)
        {
            Logger.Debug("GetTypeByAttribute started for type : {0}", bindingAttribute.PageName);
            return types.FirstOrDefault(view => view.Name == bindingAttribute.PageName);
        }

        private ViewModelBindingAttribute GetAttribute(Type type)
        {
            Logger.Debug("GetAttribute started for type : {0}", type.FullName);
            return type.GetCustomAttributes(typeof(ViewModelBindingAttribute)).FirstOrDefault()
                       as ViewModelBindingAttribute
                   ?? throw new CommonException(
                       "View to view model binding get error. Empty or null ViewModelBindingAttribute");
        }

        private SimpleVmBindingDto CreateBindingObject(Type view, Type viewModel)
        {
            Logger.Debug("CreateBindingObject started for view : {0}, and view-model : {1}",
                view?.Name, viewModel?.Name);
            return view != null && viewModel != null ? new SimpleVmBindingDto(view, viewModel) : null;
        }

        private bool IsAttributed(Type targetType, Type attributeType)
        {
            Logger.Trace("IsAttributed started for type : {0}, and attribute : {1}",
                targetType.Name, attributeType.Name);

            var result = targetType.GetCustomAttributes(attributeType).FirstOrDefault() != null;
            Logger.Trace("IsAttributed result is : {0}", result);

            return result;
        }

        private bool IsImplementsInterface(Type targetType, Type interfaceType)
        {
            Logger.Trace("IsImplementInterface started for type : {0}, and interface : {1}",
                targetType.Name, interfaceType.Name);

            var result = targetType.GetInterfaces().Any(parentInterface => parentInterface == interfaceType);
            Logger.Trace("IsImplementInterface result is : {0}", result);

            return result;
        }

        #endregion
    }
}