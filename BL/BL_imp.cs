using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using System.Text.RegularExpressions;



namespace BL
{
    internal class BL_imp : IBL
    {
        /// <summary>
        /// input: first date of vacation, number of vacation days, returns list with all the available hosting units
        /// </summary>
        /// <param name="EnteryDate">the start day of vecation</param>
        /// <param name="NumVecationDays">the num days for vecation</param>
        /// <returns>list with all the available hosting units </returns>
        public List<HostingUnit> CheckHostingUnits(DateTime EnteryDate, int NumVacationDays)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            if (NumVacationDays < 1)
                throw new Exception("It's impossible to order a vacation with less than 1 days!");
            IEnumerable<HostingUnit> hostingUnitsList = dal.GetHostingUnitsCollection();
            var result = from item in hostingUnitsList
                         where IsHostingUnitAvailable(item, EnteryDate, NumVacationDays)
                         select item;
            return result.ToList<HostingUnit>();
         
        }
        /// <summary>
        /// get key of host and returns all the Hosting units that belongs to this Host
        /// </summary>
        /// <param name="HostKey">hosting unit list</param>
        /// <returns></returns>
        public List<HostingUnit> unitsByHost(long HostKey)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            List<HostingUnit> units = dal.GetHostingUnitsCollection();
            List<HostingUnit> hostingUnitsListByHost = (from unit in units
                                                              where unit.Owner.HostKey == HostKey
                                                              select unit).ToList();
            return hostingUnitsListByHost;
        }
        /// <summary>
        /// check if a mail address is valid
        /// </summary>
        /// <param name="email">to check</param>
        /// <returns></returns>
        public bool CheckMailAddress(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// get guest request and returns the number of orders witch sent to this request 
        /// </summary>
        /// <param name="request">Guest Request</param>
        /// <returns>the number of orders witch sent to this request </returns>
        private int NumOfClientOders(GuestRequest request)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            if (!dal.GetGuestRequestsCollection().Exists(x => x.GuestRequestKey == request.GuestRequestKey))
                throw new Exception("Guest request isn't exist");
            return dal.GetOrdersCollection().Where(x => x.GuestRequestKey == request.GuestRequestKey).Count();
        }

        /// <summary>
        /// function that gets hosting unit and returns the number of orders where closed to this hosting unit
        /// </summary>
        /// <param name="Unit"></hosting unit>
        /// <returns>the number of orders where closed to this hosting unit</returns>
        private int NumOfHostingUnitOders(long HostingUnitKey)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            if (!dal.GetHostingUnitsCollection().Exists(x => x.HostingUnitKey == HostingUnitKey))
                throw new Exception("Hosting unit isn't exist");
            return dal.GetOrdersCollection().Where(x => x.HostingUnitKey == HostingUnitKey && x.status==Status.ClosedClientResponse).Count();

        }

        /// <summary>
        /// get number of days and returns all the orders that have closed at list that days befor
        /// </summary>
        /// <param name="NumDays"></the number of days>
        /// <returns>all the orders that have closed at list that days befor</returns>
        public List<Order> OrdersInThePastFewDays(int NumDays)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            List<Order> AllOrders = dal.GetOrdersCollection();
            IEnumerable<Order> result;
            result = from item in AllOrders
                     let today = DateTime.Today
                     where item.OrderDate <= today.AddDays(-NumDays)
                     select item;
            return result.ToList<Order>();
        }


        /// <summary>
        /// get date and return the diffrence betweem today and this date
        /// </summary>
        /// <param name="first"></param>
        /// <returns>diffrence betweem today and this date</returns>
        private int SumOfDays(DateTime first)
        {
            if (first < DateTime.Today)
            {
                return (DateTime.Today - first).Days;
            }
            else
                throw new Exception("ERROR The date is bigger than today");
        }//V

        /// <summary>
        /// get date and return the diffrence betweem first date and second date
        /// </summary>
        /// <param name="first"></param>
        /// <param name="secound"></param>
        /// <returns></returns>
        private int SumOfDays(DateTime first, DateTime secound)
        {
            if (first < secound)
            {
                return (secound-first).Days;
            }
            else
                throw new Exception("ERROR The secound date is bigger than the first date");
        }

        /// <summary>
        /// check if a hosting unit available in specific dates
        /// </summary>
        /// <param name="unit">the unit to check</"unit">
        /// <param name="EnteryDate">the date of start</"EnteryDate">
        /// <param name="NumDays">the number of the days to check</"NumDays">
        /// <returns></returns>
        private bool IsHostingUnitAvailable(HostingUnit unit, DateTime EntryDate, int NumDays)
        {
            
            if (EntryDate.AddDays(NumDays) >= DateTime.Today.AddMonths(11) || EntryDate < DateTime.Today)
            for (DateTime i = EntryDate; i <= EntryDate.AddDays(NumDays); EntryDate = EntryDate.AddDays(1))
            {
                if (unit.Diary[EntryDate.Month - 1, EntryDate.Day - 1] == true)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// check if its ok, remove hosting unit
        /// </summary>
        /// <param name="unit">to remove</param>
        public void removeHostingUnit(HostingUnit unit)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            List<Order> Orders = dal.GetOrdersCollection();
            foreach (var order in Orders)
            {
                if (order.HostingUnitKey == unit.HostingUnitKey && order.status == Status.SentMail
                                    || order.status == Status.NotYetAddressed)
                    throw new Exception("A hosting unit cannot be deleted because it still has open hosting offers ");
            }
            try
            {
                dal.removeHostingUnit(unit.HostingUnitKey);
            }
            catch (Exception)
            {
                throw new Exception("There is no such hosting unit");
            }
        }

        /// <summary>
        /// get orders and status, check if it possibles according to logic rules and update the order status 
        /// </summary>
        /// <param name="order">to update</param>
        /// <param name="status">to change to</param>
        public void updateOrdersStatus(Order order, Status status)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();

            HostingUnit unit = dal.GetHostingUnitsCollection().Find(x => x.HostingUnitKey == order.HostingUnitKey);
            GuestRequest request = dal.GetGuestRequestsCollection().Find(x => x.GuestRequestKey == order.GuestRequestKey);
            if (unit == null || request == null)
                throw new Exception("אין אפשרות לעדכן הזמנה זו");
            

            if (order.status == Status.ClosedClientResponse || order.status == Status.ClosedNonClientResponse)
                throw new Exception("The orders status has closed, it's impossible to change it now.");
            if(status == Status.SentMail)
            {  
                
                if(!unit.Owner.CollectionClearance)
                    throw new Exception("Bank authorization not yet approved. Unable to send mail");
                order.status = Status.SentMail;
                order.OrderDate = DateTime.Today;
                string mailTextBody = "לקוח יקר שלום" + "\n" + " :  יחידת האירוח" + unit.HostingUnitKey + " יצרה בשבילך הזמנה" + "\n"
                     + " :פרטי יחידת האירוח הם" + unit.ToString() + "\n\n" + "  המשך הקשר שלך יהיה עם המארח בכתובת המייל" + unit.Owner.MailAddress
                     + "נןפש חופש מאחלים לך חופשה נעימה  ";

                BL_Tools.GeneralSendMail(request.MailAddress, "נופש חופש יצר הזמנת חופשה בשבילך", mailTextBody, true);
                   
            }
            else if(status == Status.ClosedClientResponse)
            {
                
                unit.Owner.Commission += SumOfDays(order.EnteryDate, order.ReleaseDate) * Configuration.AmountOfCommission;
                if (!IsHostingUnitAvailable(unit, request.EntryDate, SumOfDays(request.EntryDate, request.ReleaseDate)))
                    throw new Exception("תאריכי בקשת האירוח לא פנויים ביחידת האירוח שבחרת");
                ChangeGuestRequestStatus(request, GuestRequestStatus.DealClosed);
                try
                {
                    order.status = status;
                    dal.updateOrder(order);
                }
                catch
                {
                    throw new Exception("ההזמנה לעדכון לא קיימת במערכת");
                }
                List<Order> orders = dal.GetOrdersCollection();
                List<Order> result = orders.FindAll(delegate (Order item) {
                    return (item.GuestRequestKey == order.GuestRequestKey && item.OrderKey != order.OrderKey
                        && item.EnteryDate == order.EnteryDate && item.ReleaseDate == order.ReleaseDate);
                });                         
                foreach (Order item in result)
                {
                    try
                    {
                        dal.removeOrder(item.OrderKey);
                    }
                    catch
                    {
                        //continu to the next one......
                    }
                }
                
            }
            else if (status == Status.ClosedNonClientResponse)
            {
                try
                {
                    ChangeGuestRequestStatus(request, GuestRequestStatus.ExpiresClose);
                }
                catch {  }
              
            }
            order.status = status;
            try
            {
                dal.updateOrder(order);
            }
            catch
            {
                throw new Exception("ההזמנה לעדכון לא קיימת במערכת");
            }
            
        }

        /// <summary>
        /// remove guest request from data
        /// </summary>
        /// <param name="request">Guest request to remove</param>
        public void removeGuestRequest(GuestRequest request)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            //if (request.Status == GuestRequestStatus.Open)
            //    throw new Exception("אין אפשרות למחוק בקשת אירוח שססטוס ההזמנה שלה פתוח");
            try
            {
                dal.removeGuestRequest(request.GuestRequestKey);
            }
            catch
            {
                throw new Exception("בקשת אירוח לא קיימת במערכת.");
            }
        }

        /// <summary>
        /// create a new client guest request and save it to data
        /// </summary>
        /// <param name="request"></param>
        public void CreateGuestRequest(GuestRequest request)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            //Check request correct
            if (request.ReleaseDate >= DateTime.Today.AddMonths(11) || request.EntryDate < DateTime.Today)
                throw new Exception(" יצירת בקשת אירוח נכשלה עקב בקשת תאריכים לא תקינה, ניתן להזמין אירוח עד 11 מראש");
            if (request.Adults < 1 || request.Children < 0)
                throw new Exception("יצירת בקשת אירוח נכשלה. ניתן להזמין אירוח לכל הפחות למבוגר אחד");
            if( SumOfDays(request.EntryDate, request.ReleaseDate) < 1)
                throw new Exception("יצירת בקשת אירוח נכשלה, ניתן להזמין אירוח לכל הפחות ללילה אחד");
            request.GuestRequestKey = Configuration.GuestRequestKeys++;
            request.Status = GuestRequestStatus.Open;
            request.RegistrationDate = DateTime.Today;
            try
            {
                dal.addClientRequest(request);
            }
            catch
            {
                throw new Exception("Guest request already exist");
            }
        }

        /// <summary>
        /// change guest request status
        /// </summary>
        /// <param name="request">to change</param>
        /// <param name="status"></param>
        public void ChangeGuestRequestStatus(GuestRequest request, GuestRequestStatus status)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            if (status == GuestRequestStatus.DealClosed || status == GuestRequestStatus.ExpiresClose)
            {
                try
                {
                    dal.removeGuestRequest(request.GuestRequestKey);
                }
                catch
                {
                    throw new Exception("Guest request does't exist");
                }
            }
                
            if (status == GuestRequestStatus.Open)
            {
                request.Status = status;
                try
                {
                    dal.updateGuestRequest(request);
                }
                catch
                {
                    throw new Exception("Guest request does't exist");
                }
            }
        }

        /// <summary>
        /// check if the details are correct, create a order and save it in the data
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <param name="guestRequest"></param>
        public Order CreateOrder(HostingUnit hostingUnit, GuestRequest guestRequest)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            if (dal.GetHostingUnitsCollection().Find(x=>x.HostingUnitKey== hostingUnit.HostingUnitKey)==null)
                    throw new Exception("Unexist hosting unit ");
        
            if(  dal.GetGuestRequestsCollection().Find(x=>x.GuestRequestKey==guestRequest.GuestRequestKey)==null) 
                 throw new Exception("Unexist guest request ");
         
            

            List<Order> AllOrders = dal.GetOrdersCollection();
            foreach (Order order in AllOrders)
            {
                if (order.HostKey == hostingUnit.Owner.HostKey && order.HostingUnitKey == hostingUnit.HostingUnitKey
                && guestRequest.GuestRequestKey == order.GuestRequestKey)
                    throw new Exception("an order just like this already exist. its impossible to create the same order");
            }
            //create a order
            Order NewOrder = new Order();
            NewOrder.CreateDate = DateTime.Today;
            NewOrder.GuestRequestKey = guestRequest.GuestRequestKey;
            NewOrder.HostingUnitKey = hostingUnit.HostingUnitKey;
            NewOrder.status = Status.NotYetAddressed;
            NewOrder.TotalPrice = SumOfDays(guestRequest.EntryDate, guestRequest.ReleaseDate) * hostingUnit.PricePerNight;
            NewOrder.EnteryDate = guestRequest.EntryDate;
            NewOrder.ReleaseDate = guestRequest.ReleaseDate;
            NewOrder.HostKey = hostingUnit.Owner.HostKey;
            NewOrder.OrderKey = ++Configuration.OrderKeys;
            try
            {
                dal.addOrder(NewOrder);
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
            try
            {
                updateOrdersStatus(NewOrder, Status.SentMail);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return NewOrder;
        }

        /// <summary>
        /// create a new hosting unit in data
        /// </summary>
        /// <param name="unit">to create</param>
        public void CreateHostingUnit(HostingUnit unit)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            //check if a hosting unit details are valid
            if (unit.maxAdults < 1 || unit.maxChildrens < 0 || unit.PricePerNight <= 0)
                  throw new Exception("פרטי יחידת אירוח לא תקינים");
            List<HostingUnit> units = dal.GetHostingUnitsCollection();
            if(units.FirstOrDefault()!=null)
            {
                foreach (HostingUnit hostingUnit in units)
                {
                    if (hostingUnit.HostingUnitName == unit.HostingUnitName)
                        throw new Exception("השם של יחידת האירוח שבחרת קיים במערכת, בבקשה בחר שם אחר");
                }
            }
            unit.HostingUnitKey = Configuration.HostingUnitKeys;
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 31; j++)
                    unit.Diary[i, j] = false;
            try
            {
                dal.addHostingUnit(unit);
            }
            catch
            {
                throw new Exception("יחידת אירוח זהה כבר קיימת במערכת ");
            }
            
        }

        

        /// <summary>
        /// This function get predicate with condithion and returns all the guest requests thar mutch to this condition
        /// </summary>
        /// <param name="condition">Guest request list</param>
        /// <returns></returns>
        public List<GuestRequest> SpecificGuestRequests( Predicate<GuestRequest> condition)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            List<GuestRequest> requests = dal.GetGuestRequestsCollection();
            IEnumerable<GuestRequest> result = from item in requests
                                               where condition(item)
                                               select item;
            return result.ToList<GuestRequest>();
        }

        /// <summary>
        /// select the requests by area
        /// </summary>
        /// <returns>IGrouping with the Guest requests</returns>
        public IEnumerable<IGrouping<Area, GuestRequest>> requestsByArea()
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            List<GuestRequest> requests = dal.GetGuestRequestsCollection();
            IEnumerable<IGrouping<Area, GuestRequest>> GroupsByArea = from item in requests
                                                                      orderby item.Area
                                                                      group item by item.Area into Group
                                                                      select Group;
            return GroupsByArea;
        }

        /// <summary>
        /// select the requests by num of vacationers
        /// </summary>
        /// <returns>IGrouping with the Guest requests</returns>
        public IEnumerable<IGrouping<int, GuestRequest>> requestsByNumberVacationers()
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            List<GuestRequest> requests = dal.GetGuestRequestsCollection();
            IEnumerable<IGrouping<int, GuestRequest>> GroupsByNumberVacationers = from item in requests
                                                                     let NumberVacationers = item.Adults + item.Children
                                                                     orderby NumberVacationers
                                                                     group item by NumberVacationers into Group
                                                                     select Group;
            return GroupsByNumberVacationers;
        }

        /// <summary>
        /// select the hosts by num of Hosting units
        /// </summary>
        /// <returns>IGrouping with the Hosts</returns>
        public IEnumerable<IGrouping<int, HostingUnit>> HostsByNumberOfHostingUnits()
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            List<HostingUnit> units = dal.GetHostingUnitsCollection();
            IEnumerable<IGrouping<int, HostingUnit>> GroupByNumberOfHostingUnitS =
                from item in units 
                let NumberOfHostingUnits = NumberOfHostsHostingUnits(item.Owner)
                orderby NumberOfHostingUnits
                group item by NumberOfHostingUnits into Group
                select Group;
            return GroupByNumberOfHostingUnitS;
        }

        /// <summary>
        /// get host and return the number of hosting units that belongs to that host
        /// </summary>
        /// <param name="host">int - number of hosts hosting units </param>
        /// <returns></returns>
        private int NumberOfHostsHostingUnits(Host host)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            return dal.GetHostingUnitsCollection().Where(x => x.Owner.HostKey == host.HostKey).Count();
        }

        /// <summary>
        /// select the Hosting units by area
        /// </summary>
        /// <returns>IGrouping with the Hosting units</returns>
        public IEnumerable<IGrouping<Area, HostingUnit>> UnitsByArea()
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            List<HostingUnit> units = dal.GetHostingUnitsCollection();
            IEnumerable<IGrouping<Area, HostingUnit>> GroupsByArea = from item in units
                                                                      orderby item.Area
                                                                      group item by item.Area into Group
                                                                      select Group;
            return GroupsByArea;
        }


        /// <summary>
        /// UIלא הספקתי בסוף להשתמש בה ב
        /// get hosting unit and returns all the guest requestd that match to it
        /// </summary>
        /// <param name = "unit" > Guest request list</param>
        /// <returns></returns>
        public List<GuestRequest> CreateMatch(HostingUnit unit)
        {
            List<GuestRequest> requests = SpecificGuestRequests(delegate (GuestRequest request)
            {
                return (request.Status == GuestRequestStatus.Open && request.Adults < unit.maxAdults && request.Children < unit.maxChildrens && request.Area == unit.Area
                && IsHostingUnitAvailable(unit, request.EntryDate, SumOfDays(request.EntryDate, request.ReleaseDate))
                && (((request.Garden == Garden.Necessary || request.Garden == Garden.possible) && unit.Garden == true)
                || ((request.Garden == Garden.uninterested || request.Garden == Garden.possible) && unit.Garden == false))
                && request.Type == unit.Type
                && (((request.Pool == Pool.Necessary || request.Pool == Pool.possible) && unit.Pool == true)
                || ((request.Pool == Pool.uninterested || request.Pool == Pool.possible) && unit.Pool == false))
                && (((request.Jacuzzi == Jacuzzi.Necessary || request.Jacuzzi == Jacuzzi.possible) && unit.Jacuzzi == true)
                || ((request.Jacuzzi == Jacuzzi.uninterested || request.Jacuzzi == Jacuzzi.possible) && unit.Jacuzzi == false))
                && (((request.ChildrensAttractions == ChildrensAttractions.Necessary || request.ChildrensAttractions == ChildrensAttractions.possible) && unit.ChildrensAttractions == true)
                || ((request.ChildrensAttractions == ChildrensAttractions.uninterested || request.ChildrensAttractions == ChildrensAttractions.possible) && unit.ChildrensAttractions == false)));
            });
            if (requests.Count() != 0)
                return requests;
            throw new Exception("No guest requests matching the hosting unit were found");

        }


        /// <summary>
        /// get Hosting unit name and return the hosting unit
        /// </summary>
        /// <param name="HostingUnitName">to find</param>
        /// <returns>hosting unit</returns>
        public HostingUnit FindHostingUnit(string HostingUnitName)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            HostingUnit Unit = dal.GetHostingUnitsCollection().Find(unit => unit.HostingUnitName == HostingUnitName);
            if (Unit == null)
                throw new Exception("Unit doesn't found ");
            return Unit;
        }

        /// <summary>
        /// check hosting unit details and update
        /// </summary>
        /// <param name="unit">to update</param>
        public void UpdateHostingUnit(HostingUnit unit)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            //check the hosting unit details
            
            if (unit.maxAdults < 1 || unit.maxChildrens < 0 || unit.PricePerNight <= 0)  
                throw new Exception("פרטי יחידת אירוח לא תקינים");
            List<HostingUnit> units = dal.GetHostingUnitsCollection();
            
            try
            {
                dal.updateHostingUnit(unit);
            }
            catch
            {
                throw new Exception("יחידת אירוח לא קיימת");
            }
        }
        /// <summary>
        /// get guest request code and returns the guest request
        /// </summary>
        /// <param name="GuestRequestKey">to find</param>
        /// <returns>guest request</returns>
        public GuestRequest FindGuestRequest(long GuestRequestKey)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            GuestRequest request = dal.GetGuestRequestsCollection().Find(r => r.GuestRequestKey == GuestRequestKey);
            if (request == null)
                throw new Exception("Guest request doesn't found ");
            return request;
        }

        /// <summary>
        /// get host key, returns all the orders have closed with this host
        /// </summary>
        /// <param name="HostKey"></param>
        /// <returns>orders list</returns>
        public List<Order> OrdersByHost(long HostKey)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            List<Order> orders = dal.GetOrdersCollection().FindAll(x => x.HostKey == HostKey);
            if (orders.Count() == 0)
                throw new Exception("Host does'nt have orders at all.");
            return orders;
        }

        /// <summary>
        /// return list with orders from memory
        /// </summary>
        /// <returns>order list</returns>
        public List<Order> AllOrders()
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            return dal.GetOrdersCollection();
        }

        /// <summary>
        /// returns list with hosting units from memory
        /// </summary>
        /// <returns>hosting unit list</returns>
        public List<HostingUnit> AllHostingUnits()
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            return dal.GetHostingUnitsCollection();
        }

        /// <summary>
        /// returns list with guest requests from memory
        /// </summary>
        /// <returns>guest request list</returns>
        public List<GuestRequest> AllGuestRequest()
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            return dal.GetGuestRequestsCollection();
        }

        public void CheckCollectionClearance(HostingUnit unit, bool PreviousCollectionClearance)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            if (PreviousCollectionClearance == true && unit.Owner.CollectionClearance == false &&
                dal.GetOrdersCollection().Exists(x => x.HostKey == unit.Owner.HostKey && (x.status != Status.ClosedClientResponse
               || x.status != Status.ClosedNonClientResponse)))
            {
                unit.Owner.CollectionClearance = true;
                throw new Exception("You have open-ended hosting offers. Bank debit authorization cannot be revoked.");
            }
        }

        /// <summary>
        /// check if the entery owner details are correct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void IsOwner(string userName, string password)
        {
            SiteOwner owner = SiteOwnerDetails.getSiteOwner();
            if (owner.userName != userName || owner.password != password)
                throw new Exception("!הכניסה נדחתה, מספר תעודת זהות או ססמא שגויים");
        }
        /// <summary>
        /// check if a host identity details are correct throw exception if it doesnt
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns>the host</returns>
        public Host IsHost(long id, string password)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            List<Host> AllHosts = dal.GetHostsCollection();
            Host host = AllHosts.Find(x => x.HostKey == id && x.Password == password);
            if (host == null)
                throw new Exception("!שם משתמש או ססמא שגויים");
            return host;   
        }
        /// <summary>
        /// returns list with all the bank names
        /// </summary>
        /// <returns></returns>
        public List<string> AllBanksNames()
        {
            List<BankBranch> banks = DAL.FactoryDal.getInstance().GetBanckBranchsCollection();
            List<string> AllBanksNames = new List<string>();
            foreach(BankBranch bank in banks)
            {
                string bankDetails = bank.BankName;
                if (!AllBanksNames.Exists(x => x == bankDetails))
                    AllBanksNames.Add(bankDetails);
            }
            return AllBanksNames;
        }
        /// <summary>
        /// returns list with all the bank branches numbers
        /// </summary>
        /// <param name="BankName"></param>
        /// <returns></returns>
        public List<string> AllBanksBranches(string BankName)
        {
            
            List<BankBranch> banks = DAL.FactoryDal.getInstance().GetBanckBranchsCollection();
            List<string> BranchNames = new List<string>();
            foreach (BankBranch bank in banks)
            { 
                if (bank.BankName == BankName)
                    BranchNames.Add(bank.BranchNumber.ToString());
            }
            return BranchNames;
        }
        /// <summary>
        /// get bank key details and returns the bank
        /// </summary>
        /// <param name="BranchNumber"></param>
        /// <param name="BankName"></param>
        /// <returns>bankbrunch</returns>
        public BankBranch FindBranchBank(string BranchNumber, string BankName)
        {
            List<BankBranch> banks = DAL.FactoryDal.getInstance().GetBanckBranchsCollection();
            
            
            try
            {
                BankBranch MyBank = banks.First(bank => bank.BankName == BankName && bank.BranchNumber.ToString() == BranchNumber);
                return MyBank;
            }
            catch
            {
                throw new Exception("בנק לא קיים. בחר בנק אחר בבקשה");
            }
            

        }
        /// <summary>
        /// add new host to the system, throw exceptions if this is not valid hosts details
        /// </summary>
        /// <param name="host"></param>
        public void addHost(Host host)
        {
            DAL.IDAL dal = DAL.FactoryDal.getInstance();
            
            if(!CheckMailAddress(host.MailAddress) || !(dal.GetBanckBranchsCollection().Exists(x=>x.BankName==host.BankBranchDetails.BankName
            && x.BankNumber==host.BankBranchDetails.BankNumber && x.BranchNumber==host.BankBranchDetails.BranchNumber)))
                throw new Exception("יצירת חשבון מארח נכשלה. אחד או יותר מהפרטים שגוי");
            try
            {
                dal.addHost(host);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }
        /// <summary>
        /// returns list with all the hosts
        /// </summary>
        /// <returns></returns>
        public List<Host> AllHosts()
        {
            return DAL.FactoryDal.getInstance().GetHostsCollection();
        }
    }
}
