using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DS;


namespace DAL
{
    internal class DAL_imp : IDAL
    {//
        /// <summary>
        /// add new client request for vecation
        /// </summary>
        /// <param name="request">Guest Request to add</param>
        public void addClientRequest(GuestRequest request)
        {
            IEnumerable<GuestRequest> SameRequests = from item in DataSource.Requests
                                              where item.GuestRequestKey == request.GuestRequestKey
                                              select item;
            if (SameRequests.FirstOrDefault()!=null)
                updateGuestRequest(request);
            else
            {
                DataSource.Requests.Add(request.Clone());
            } 
        }

        /// <summary>
        /// add new hosting unit 
        /// </summary>
        /// <param name="unit">Hosting Unit to add</param>
        public void addHostingUnit(HostingUnit unit)
        {
            List<HostingUnit> SameUnits = (from item in DataSource.Units
                                     where item.HostingUnitKey == unit.HostingUnitKey
                                     select item).ToList();
            if (SameUnits.FirstOrDefault() != null)
                updateHostingUnit(unit);
            else
            {
                DataSource.Units.Add(unit.Clone());
            }
        }

        /// <summary>
        /// add new order 
        /// </summary>
        /// <param name="order">Order to add</param>
        public void addOrder(Order order)
        {
            IEnumerable<Order> SameUnits = from item in DataSource.Orders
                                                 where item.OrderKey == order.OrderKey
                                                 select item;
            if (SameUnits.FirstOrDefault() != null)
                throw new Exception("Order already exist");
            else
            {
                DataSource.Orders.Add(order.Clone());
            }
        }



        /// <summary>
        /// remove the Hosting units with the specific key
        /// </summary>
        /// <param name="unitKey">the key of the unit to remove</param>
        public void removeHostingUnit(long unitKey)
        {
            int count = DataSource.Units.RemoveAll(x => x.HostingUnitKey == unitKey);
            if (count == 0)
                throw new Exception("Can't delete unexist item");
        }

        /// <summary>
        /// update a hosting unit details with a specific key
        /// </summary>
        /// <param name="unit"></param>
        public void updateHostingUnit(HostingUnit unit)
        {
            int count = DataSource.Units.RemoveAll(x => x.HostingUnitKey == unit.HostingUnitKey);
            if (count == 0)
                throw new Exception("Can't update unexist item");
            DataSource.Units.Add(unit.Clone());

        }

        /// <summary>
        /// update a order details with a specific key
        /// </summary>
        /// <param name="order"></param>
        public void updateOrder(Order order)
        {
            int count = DataSource.Orders.RemoveAll(x => x.OrderKey == order.OrderKey);
            if (count == 0)
                throw new Exception("Can't update unexist item");
            DataSource.Orders.Add(order.Clone());
        }

        /// <summary>
        /// update a guest request details with a specific key
        /// </summary>
        /// <param name="request">void</param>
        public void updateGuestRequest(GuestRequest request)
        {
            int count = DataSource.Requests.RemoveAll(x => x.GuestRequestKey == request.GuestRequestKey);
            if (count == 0)
                throw new Exception("Can't update unexist item");
            DataSource.Requests.Add(request.Clone());
        }
        /// <summary>
        /// returns copy of the hosting units list
        /// </summary>
        /// <returns>hosting unit list</returns>
        public List<HostingUnit> GetHostingUnitsCollection()
        {
            return DataSource.Units.Clone();
        }
        /// <summary>
        /// returns copy of the Guest Requests list
        /// </summary>
        /// <returns>Guest Requests list</returns>
        public List<GuestRequest> GetGuestRequestsCollection()
        {
            return DataSource.Requests.Clone();
        }
        /// <summary>
        /// returns copy of the Orders list
        /// </summary>
        /// <returns>Orders list</returns>
        public List<Order> GetOrdersCollection()
        {
            return DataSource.Orders.Clone();
        }

        /// <summary>
        /// create a list with all the bank branch and return it
        /// </summary>
        /// <returns>Bank Branchs list</returns>
        public List<BankBranch> GetBanckBranchsCollection()
        {
            List<BankBranch> BankAccounts = new List<BankBranch>()
            {
                new BankBranch()
                {
                    BankNumber = 12,
                    BankName = "Apohalim",
                    BranchNumber = 694,
                    BranchAddress = "Shopping center, Ramat Beit Hakerem. Accessoher Street",
                    BranchCity= "Jerusalem"
                },
                new BankBranch()
                {
                    BankNumber = 10,
                    BankName = "Leumi",
                    BranchNumber = 400,
                    BranchAddress = "Herzl 19, Tel Aviv Jaffa",
                    BranchCity= "Tel Aviv"
                },
                new BankBranch()
                {
                    BankNumber = 11,
                    BankName = "Discont",
                    BranchNumber = 76,
                    BranchAddress = "Presidential Avenue 124",
                    BranchCity= "Haifa"
                },
                new BankBranch()
                {
                    BankNumber = 54,
                    BankName = "Bank Yerushalim",
                    BranchNumber = 053,
                    BranchAddress = "Herzl 63",
                    BranchCity= "Rishon Letzion"
                },
                new BankBranch()
                {
                    BankNumber = 31,
                    BankName = "Habein Leumi Harishon",
                    BranchNumber = 517,
                    BranchAddress = "Henrietta Szold 8",
                    BranchCity= "Beer Sheva"
     
                }
            };
            return BankAccounts;
        }




        /// <summary>
        /// remove guest request from the list
        /// </summary>
        /// <param name="requestKey">the key of the request to remove</param>
        public void removeGuestRequest(long requestKey)
        {

            int count = DataSource.Requests.RemoveAll(x => x.GuestRequestKey == requestKey);
            if (count == 0)
                throw new Exception("There is no such request");
        }

        

        /// <summary>
        /// remove an order from list
        /// </summary>
        /// <param name="orderKey">the key of the order to remove</param>
        public void removeOrder(long orderKey)
        {
            int count = DataSource.Orders.RemoveAll(x => x.OrderKey == orderKey);
            if (count == 0)
                throw new Exception("There is no such request");
        }

        public void addHost(Host host)
        {
            List<Host> Hosts = (from item in DataSource.Hosts
                                where item.HostKey == host.HostKey
                                select item).ToList();
            if (Hosts.FirstOrDefault() == null)
                DataSource.Hosts.Add(host.Clone());
            else
                throw new Exception("מארח זהה כבר קיים במערכת");
        }

        public List<Host> GetHostsCollection()
        {
            return DataSource.Hosts.Clone();
        }
    }
}


