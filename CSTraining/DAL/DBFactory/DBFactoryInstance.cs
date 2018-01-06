using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DBFactoryInstance
    {
        private static AbstractDBFactory factory;        
        public static AbstractDBFactory GetFactoryInstance
        { 
            get 
            {
                if (factory == null)
                    factory = new MyDBFactory();
                return factory; 
            }
        }
    }
}
