using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        private static long hostingUnitKeys = 10000000;
        public static long HostingUnitKeys
        {
            get { return hostingUnitKeys++; }
            set { hostingUnitKeys = value; }
        }

        private static long guestRequestKeys = 10000000;
        public static long GuestRequestKeys
        {
            get { return guestRequestKeys++; }
            set { guestRequestKeys = value; }
        }
        private static long orderKeys = 10000000;
        public static long OrderKeys
        {
            get { return orderKeys++; }
            set { orderKeys = value; }
        }

        public static float AmountOfCommission = 10; //10 shekel
    }
}
