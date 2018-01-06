using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Unity
{
    public class UnityContainerHolder
    {
        private static IUnityContainer container;

        public static IUnityContainer GetContainer
        {
            get
            {
                return container;
            }            
        }

        public static void SetContainer(IUnityContainer _container)
        {
            container = _container;
        }

    }
}
