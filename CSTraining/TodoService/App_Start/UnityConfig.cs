using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

namespace TodoService
{
  public static class UnityConfig
  {
      public static IUnityContainer RegisterContainer()
      {
          var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
          IUnityContainer container = new UnityContainer().LoadConfiguration(section);
          UnityContainerHolder.SetContainer(container);
          DependencyResolver.SetResolver(new UnityDependencyResolver(container));
          return container;      
      }     
  }
}