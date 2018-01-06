using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Unity;
using Contracts.DAL;
using Contracts.BLL;
using DAL;
using BusinessLogic;
using BusinessLogic.BLL;
using Entities;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.Web;

namespace MvcApp
{
  public static class UnityConfig
  {
      public static IUnityContainer RegisterContainer(HttpApplicationState app, bool fromConfig = false)
      {
          IUnityContainer container;
          if (fromConfig)
          {
              var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
              container = new UnityContainer().LoadConfiguration(section);
          }
          else
          {
              container = new UnityContainer();
              RegisterTypes(container);              
          }
          UnityContainerHolder.SetContainer(container);
          DependencyResolver.SetResolver(new UnityDependencyResolver(container));
          app.Add("Unity", container);
          return container;
      }     

      private static void RegisterTypes(IUnityContainer container)
      {          
              container.RegisterType<MyContext>("Context", new ContainerControlledLifetimeManager());
              container.RegisterType(typeof(IRepository<>), typeof(DataRepository<>), new InjectionConstructor(new ResolvedParameter<MyContext>("Context")));

              container.RegisterType<ICustomerBL, CustomerBL>(new InjectionConstructor(new ResolvedParameter<IRepository<Customer>>()));
              container.RegisterType<IOrderBL, OrderBL>(new InjectionConstructor(new ResolvedParameter<IRepository<Order>>()));
              container.RegisterType<IProductBL, ProductBL>(new InjectionConstructor(new ResolvedParameter<IRepository<Product>>()));
              container.RegisterType<ISupplierBL, SupplierBL>(new InjectionConstructor(new ResolvedParameter<IRepository<Supplier>>()));

              container.RegisterType<BusinessLogicLayer>();

      }
  }
}