using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class FactoriaIBL
    {
        private static IBL instance = null;

        public static IBL getInstance()
        {
            if (instance == null)
            {
                instance = new BL_imp();
            }
            return instance;
        }
    }
}
