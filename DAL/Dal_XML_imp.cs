using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Reflection;
using System.Net;
using System.Xml;
using System.ComponentModel;
using System.Threading;
using System.Data;
using System.IO;

namespace DAL
{
    class Dal_XML_imp : IDAL
    {
        public static List<HostingUnit> hostingUnitList;
        XElement orderRoot;
        XElement GuestRequestRoot;
        XElement configRoot;
        XElement hostingUnitRoot;
        XElement hostRoot;

        static bool bankDownloaded = false;
        readonly string hostRootPath = @"HostsXML.xml";
        readonly string guestRequestPath = @"GuestRequestsXML.xml";
        readonly string bankPath = @"atm.xml";
        readonly string orderPath = @"OrdersXML.xml";
        readonly string HostingUnitPath = @"HostingUnitsXML.xml";
        readonly string configPath = @"ConfigurationXML.xml";
        
       
        BackgroundWorker worker;
        /// <summary>
        /// constructor 
        /// create the files if it necessary and create threeds
        /// </summary>
        public Dal_XML_imp()
        {

            try
            {


                if (!File.Exists(configPath))
                    CreateFilesCode();
                LoadDataCode();

                if (!File.Exists(orderPath))
                {
                    orderRoot = new XElement("Orders");
                    orderRoot.Save(orderPath);
                }
                else
                    LoadDataOrder();
                if (!File.Exists(guestRequestPath))
                {
                    GuestRequestRoot = new XElement("guestRequests");
                    GuestRequestRoot.Save(guestRequestPath);
                }
                else
                    LoadDataGuestRequest();

                if (!File.Exists(HostingUnitPath))
                {
                    hostingUnitRoot = new XElement("hostingUnits");
                    hostingUnitRoot.Save(HostingUnitPath);
                    SaveToXML(new List<HostingUnit>(), HostingUnitPath);
                }
                else
                    LoadDataHostingUnit();
                if (!File.Exists(hostRootPath))
                {
                    hostRoot = new XElement("hosts");
                    hostRoot.Save(hostRootPath);
                }
                else
                    LoadDataHost();
               
                if (!File.Exists(bankPath))
                {
                    worker = new BackgroundWorker();
                    worker.DoWork += Worker_DoWork;
                    worker.RunWorkerAsync();
                }


                hostingUnitList = LoadFromXML<List<HostingUnit>>(HostingUnitPath);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// distructor
        /// save the serial numbers to xml before 
        /// </summary>
        ~Dal_XML_imp()
        {
            try
            {
                XElement order_Key = new XElement("Orders", Configuration.OrderKeys);
                XElement guestRequest_Key = new XElement("RequestKey", Configuration.GuestRequestKeys);
                XElement hostingUnit_Key = new XElement("UnitKey", Configuration.HostingUnitKeys);
                XElement comission = new XElement("comission", 10);
                configRoot = new XElement("Config", guestRequest_Key, hostingUnit_Key, order_Key, comission);
                configRoot.Save(configPath);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        

        /// <summary>
        /// load hosting units data from xml file
        /// </summary>
        private void LoadDataHostingUnit()
        {
            try
            {
                hostingUnitRoot = XElement.Load(HostingUnitPath);

            }
            catch
            {
                throw new InvalidOperationException("problem uploading file");
            }
        }
        /// <summary>
        /// load data to xelemnt from xml
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            T result = (T)xmlSer.Deserialize(file);
            file.Close();
            return result;
        }

        /// <summary>
        /// load the config serial numbers from memory
        /// </summary>
        private void LoadDataCode()
        {
            try
            {
                configRoot = XElement.Load(configPath);
                Configuration.AmountOfCommission = Int64.Parse(configRoot.Element("comission").Value);
                Configuration.GuestRequestKeys = Int64.Parse(configRoot.Element("RequestKey").Value);
                Configuration.HostingUnitKeys = Int64.Parse(configRoot.Element("UnitKey").Value);
                Configuration.OrderKeys = Int64.Parse(configRoot.Element("Orders").Value);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        /// <summary>
        /// save data to xml using serialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="path"></param>
        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSer = new XmlSerializer(source.GetType());
            xmlSer.Serialize(file, source);
            file.Close();
        }
        private void LoadDataOrder()
        {
            try
            {
                orderRoot = XElement.Load(orderPath);

            }
            catch
            {
                throw new InvalidOperationException("בעיה בטעינת הקובץ");
            }
        }
        /// <summary>
        /// load the data from the hosts xml file
        /// </summary>
        private void LoadDataHost()
        {
            try
            {
                hostRoot = XElement.Load(hostRootPath);
            }
            catch
            {
                throw new InvalidOperationException("problem uploading file");
            }
        }
        /// <summary>
        /// create the confug file and save the serial keys
        /// </summary>
        private void CreateFilesCode()
        {
            try
            {
                XElement guestRequest_Key = new XElement("RequestKey", Configuration.GuestRequestKeys);
                XElement hostingUnit_Key = new XElement("UnitKey", Configuration.HostingUnitKeys);
                XElement OrderKeys = new XElement("Orders", Configuration.OrderKeys);
                XElement AmountOfCommission = new XElement("commission", Configuration.AmountOfCommission);
                configRoot = new XElement("Config", guestRequest_Key, hostingUnit_Key, OrderKeys, AmountOfCommission);
                configRoot.Save(configPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// load the guest request data from the xml file
        /// </summary>
        private void LoadDataGuestRequest()
        {
            try
            {
                GuestRequestRoot = XElement.Load(guestRequestPath);

            }
            catch
            {
                throw new Exception("בעיה בטעינת הקובץ");
            }
        }

        /// <summary>
        /// add new host to memory after checking if this host doesnt exist
        /// </summary>
        /// <param name="host"></param>
        public void addHost(Host host)
        {
            XElement SameHost = (from h in hostRoot.Elements()
                                 where Int64.Parse(h.Element("HostKey").Value) == host.HostKey
                                 select h).FirstOrDefault();
            if (SameHost != null)
            {
                throw new Exception("מארח כבר קיים במערכת");
            }
            hostRoot.Add(new XElement("Hosts",
                             new XElement("HostKey", host.HostKey),
                             new XElement("PrivateName", host.PrivateName),
                             new XElement("FamilyName", host.FamilyName),
                             new XElement("FhoneNumber", host.FhoneNumber),
                             new XElement("MailAddress", host.MailAddress),
                             new XElement("BankBranchDetails",
                                           new XElement("BankNumber", host.BankBranchDetails.BankNumber),
                                           new XElement("BankName", host.BankBranchDetails.BankName),
                                           new XElement("BranchNumber", host.BankBranchDetails.BranchNumber),
                                           new XElement("BranchAddress", host.BankBranchDetails.BranchAddress),
                                           new XElement("BranchCity", host.BankBranchDetails.BranchCity)
                                           ),
                             new XElement("BankAccountNumber", host.BankAccountNumber),
                             new XElement("Commission", host.Commission),
                             new XElement("CollectionClearance", host.CollectionClearance),
                             new XElement("password", host.Password)
                              )
                            );
            hostRoot.Save(hostRootPath);
        }

        /// <summary>
        /// returns list with all the hosts in system
        /// </summary>
        /// <returns></returns>
        public List<Host> GetHostsCollection()
        {
            LoadDataHost();
            List<Host> Hosts = (from h in hostRoot.Elements()
                                select new Host()
                                {
                                    HostKey = Int64.Parse(h.Element("HostKey").Value),
                                    PrivateName = h.Element("PrivateName").Value,
                                    FamilyName = h.Element("FamilyName").Value,
                                    FhoneNumber = h.Element("FhoneNumber").Value,
                                    MailAddress = h.Element("MailAddress").Value,
                                    BankBranchDetails = new BankBranch()
                                    {
                                        BankNumber = Int64.Parse(h.Element("BankBranchDetails").Element("BankNumber").Value),
                                        BankName = h.Element("BankBranchDetails").Element("BankName").Value,
                                        BranchNumber = Int64.Parse(h.Element("BankBranchDetails").Element("BranchNumber").Value),
                                        BranchAddress = h.Element("BankBranchDetails").Element("BranchAddress").Value,
                                        BranchCity = h.Element("BankBranchDetails").Element("BranchCity").Value

                                    },
                                    BankAccountNumber = Int64.Parse(h.Element("BankAccountNumber").Value),
                                    Commission = double.Parse(h.Element("Commission").Value),
                                    CollectionClearance = Boolean.Parse(h.Element("CollectionClearance").Value),
                                    Password = h.Element("password").Value
                                }
                             ).ToList();
            return Hosts;
        }
        /// <summary>
        /// add new guest request to system after checking that its not exist
        /// </summary>
        /// <param name="request"></param>
        public void addClientRequest(GuestRequest request)
        {
            XElement guestRequest;

            guestRequest = (from r in GuestRequestRoot.Elements()
                            where Int64.Parse(r.Element("GuestRequestKey").Value) == request.GuestRequestKey
                            select r).FirstOrDefault();


            if (guestRequest != null)
            {

                updateGuestRequest(request);

            }
            GuestRequestRoot.Add(new XElement("GuestRequest",
                                      new XElement("GuestRequestKey", request.GuestRequestKey),
                                      new XElement("PrivateName", request.PrivateName),
                                      new XElement("FamilyName", request.FamilyName),
                                      new XElement("MailAddress", request.MailAddress),
                                      new XElement("Status", request.Status),
                                      new XElement("RegistrationDate", request.RegistrationDate),
                                      new XElement("EntryDate", request.EntryDate),
                                      new XElement("ReleaseDate", request.ReleaseDate),
                                      new XElement("Area", request.Area),
                                      new XElement("Type", request.Type),
                                      new XElement("Adults", request.Adults),
                                      new XElement("Children", request.Children),
                                      new XElement("Pool", request.Pool),
                                      new XElement("Jacuzzi", request.Jacuzzi),
                                      new XElement("Garden", request.Garden),
                                      new XElement("ChildrensAttractions", request.ChildrensAttractions)
                                    )
                );
            GuestRequestRoot.Save(guestRequestPath);
        }

        /// <summary>
        /// update guest request details. throw exception if the request to update doesnt exist
        /// </summary>
        /// <param name="request"></param>
        public void updateGuestRequest(GuestRequest request)
        {

            XElement guestRequest = (from req in GuestRequestRoot.Elements()
                                     where Int64.Parse(req.Element("GuestRequestKey").Value) == request.GuestRequestKey
                                     select req).FirstOrDefault();
            if (guestRequest == null)
            {
                throw new Exception("אין אפשרות לעדכן עצם שלא קיים");
            }
            guestRequest.Element("GuestRequestKey").Value = request.GuestRequestKey.ToString();
            guestRequest.Element("PrivateName").Value = request.PrivateName;
            guestRequest.Element("FamilyName").Value = request.FamilyName;
            guestRequest.Element("MailAddress").Value = request.MailAddress;
            guestRequest.Element("Status").Value = request.Status.ToString();
            guestRequest.Element("RegistrationDate").Value = request.RegistrationDate.ToString();
            guestRequest.Element("EntryDate").Value = request.EntryDate.ToString();
            guestRequest.Element("ReleaseDate").Value = request.ReleaseDate.ToString();
            guestRequest.Element("Area").Value = request.Area.ToString();
            guestRequest.Element("Type").Value = request.Type.ToString();
            guestRequest.Element("Adults").Value = request.Adults.ToString();
            guestRequest.Element("Children").Value = request.Children.ToString();
            guestRequest.Element("Pool").Value = request.Pool.ToString();
            guestRequest.Element("Jacuzzi").Value = request.Jacuzzi.ToString();
            guestRequest.Element("Garden").Value = request.Garden.ToString();
            guestRequest.Element("ChildrensAttractions").Value = request.ChildrensAttractions.ToString();
            GuestRequestRoot.Save(guestRequestPath);
        }

        /// <summary>
        /// add new hosting unit to system after cheking that its not exist
        /// </summary>
        /// <param name="unit"></param>
        public void addHostingUnit(HostingUnit unit)
        {
            var SameUnit = from units in hostingUnitRoot.Elements()
                           let newUnit = units
                           where Int64.Parse(units.Element("HostingUnitKey").Value) == unit.HostingUnitKey 
                           select units;
            if (SameUnit.FirstOrDefault() == null)
            {
                hostingUnitList.Add(unit);
                SaveToXML(hostingUnitList, HostingUnitPath);
            }
            else
            {
                updateHostingUnit(unit);
            }
            
        }

        /// <summary>
        /// update hosting unit, throw exception if it doesnt exist
        /// </summary>
        /// <param name="unit"></param>
        public void updateHostingUnit(HostingUnit unit)
        {
            
            HostingUnit hostingUnit = new HostingUnit();
            foreach (HostingUnit u in hostingUnitList)
            {
                if (u.HostingUnitKey == unit.HostingUnitKey)
                {
                    hostingUnit = u;
                    hostingUnit.Owner = unit.Owner;
                    hostingUnit.HostingUnitName = unit.HostingUnitName;
                    hostingUnit.Diary = unit.Diary;
                    hostingUnit.Area = unit.Area;
                    hostingUnit.Type = unit.Type;
                    hostingUnit.maxAdults = unit.maxAdults;
                    hostingUnit.maxChildrens = unit.maxChildrens;
                    hostingUnit.Pool = unit.Pool;
                    hostingUnit.Jacuzzi = unit.Jacuzzi;
                    hostingUnit.Garden = unit.Garden;
                    hostingUnit.ChildrensAttractions = unit.ChildrensAttractions;
                    hostingUnit.PricePerNight = unit.PricePerNight;

                    SaveToXML(hostingUnitList, HostingUnitPath);
                    return;
                }
            }
            throw new Exception("לא יכול לעדכן יחידת אירוח שלא קיימת");
        }

        /// <summary>
        /// add new order to the system after checking if it doesnt exist
        /// </summary>
        /// <param name="order"></param>
        public void addOrder(Order order)
        {
            XElement SameOrder = (from o in orderRoot.Elements()
                                  where Int64.Parse(o.Element("OrderKey").Value) == order.OrderKey
                                  select o).FirstOrDefault();
            if (SameOrder != null)
            {
                throw new Exception("הזמנה כבר קיימת במערכת");
            }
            orderRoot.Add(new XElement("Order",
                                            new XElement("OrderKey", order.OrderKey),
                                            new XElement("HostKey", order.HostKey),
                                            new XElement("HostingUnitKey", order.HostingUnitKey),
                                            new XElement("GuestRequestKey", order.GuestRequestKey),
                                            new XElement("status", order.status),
                                            new XElement("CreateDate", order.CreateDate),
                                            new XElement("OrderDate", order.OrderDate),
                                            new XElement("EnteryDate", order.EnteryDate),
                                            new XElement("ReleaseDate", order.ReleaseDate),
                                            new XElement("TotalPrice", order.TotalPrice)
                                            )
                                           );
            orderRoot.Save(orderPath);
        }

        /// <summary>
        /// update order details, throw exception if it doesnt exist
        /// </summary>
        /// <param name="order"></param>
        public void updateOrder(Order order)
        {
            XElement SameOrder = (from o in orderRoot.Elements()
                                  where Int64.Parse(o.Element("OrderKey").Value) == order.OrderKey
                                  select o).FirstOrDefault();

            
            if (SameOrder == null)
            {
                throw new KeyNotFoundException("ההזמנה אינה קיימת");
            }
            SameOrder.Element("OrderKey").Value = order.OrderKey.ToString();
            SameOrder.Element("HostKey").Value = order.HostKey.ToString();
            SameOrder.Element("HostingUnitKey").Value = order.HostingUnitKey.ToString();
            SameOrder.Element("GuestRequestKey").Value = order.GuestRequestKey.ToString();
            SameOrder.Element("status").Value = order.status.ToString();
            SameOrder.Element("CreateDate").Value = order.CreateDate.ToString();
            SameOrder.Element("OrderDate").Value = order.OrderDate.ToString();
            SameOrder.Element("EnteryDate").Value = order.EnteryDate.ToString();
            SameOrder.Element("ReleaseDate").Value = order.ReleaseDate.ToString();
            SameOrder.Element("TotalPrice").Value = order.TotalPrice.ToString();

            orderRoot.Save(orderPath);
        }
        /// <summary>
        /// returns list with all the hosting units in system
        /// </summary>
        /// <returns></returns>
        public List<HostingUnit> GetHostingUnitsCollection()
        {
            try
            {
                LoadDataHostingUnit();
                var it = (from item in hostingUnitRoot.Elements()
                          select ConvertHostingUnit(item).Clone()).ToList();
                return it;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
         /// <summary>
         /// returns list with all the guest requests in system
         /// </summary>
         /// <returns></returns>
        public List<GuestRequest> GetGuestRequestsCollection()
        {
            LoadDataGuestRequest();
            var it = (from item in GuestRequestRoot.Elements()
                      select ConvertGuestRequest(item).Clone()).ToList();
            return it;
        }

        /// <summary>
        /// returns list with all the orders in system
        /// </summary>
        /// <returns></returns>
        public List<Order> GetOrdersCollection()
        {
            try
            {
                LoadDataOrder();
                var it = (from item in orderRoot.Elements()
                          select ConvertOrder(item).Clone()).ToList();
                return it;
            }
            catch
            {
                throw new Exception("problem loading order list");
            }
        }
        /// <summary>
        /// convert the banks details from the xml file to list
        /// </summary>
        /// <returns></returns>
        private List<BankBranch> ListOfBanks()
        {
            List<BankBranch> banks = new List<BankBranch>();
            XmlDocument doc = new XmlDocument();
            doc.Load(bankPath);
            XmlNode rootNode = doc.DocumentElement;
           

            XmlNodeList children = rootNode.ChildNodes;
            foreach (XmlNode child in children)
            {
                BankBranch b = GetBranchByXmlNode(child);
                if (b != null)
                {
                    banks.Add(b);
                }
            }

            return banks;
        }
        /// <summary>
        /// convert bank (xml child) to BranchBank type
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static BankBranch GetBranchByXmlNode(XmlNode node)
        {
            if (node.Name != "BRANCH") return null;
            BankBranch branch = new BankBranch();
           

            XmlNodeList children = node.ChildNodes;

            foreach (XmlNode child in children)
            {
                switch (child.Name)
                {
                    case "Bank_Code":
                        branch.BankNumber = int.Parse(child.InnerText);
                        break;
                    case "Bank_Name":
                        branch.BankName = child.InnerText;
                        break;
                    case "Branch_Code":
                        branch.BranchNumber = int.Parse(child.InnerText);
                        break;
                    case "Address":
                        branch.BranchAddress = child.InnerText;
                        break;
                    case "City":
                        branch.BranchCity = child.InnerText;
                        break;

                }

            }

            if (branch.BranchNumber > 0)
                return branch;

            return null;

        }
        /// <summary>
        /// download banks details from unternet
        /// </summary>
        private void DownloadBank()
        {
            #region downloadBank
            string xmlLocalPath = bankPath;
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath =
               @"https://www.boi.org.il/en/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/snifim_en.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
                bankDownloaded = true;
            }
            catch
            {

                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
                bankDownloaded = true;

            }
            finally
            {
                wc.Dispose();
            }

            #endregion

        }
        /// <summary>
        /// keep calling to DownloadBank function until the bank details downloaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {

            object ob = e.Argument;
            while (bankDownloaded == false)//continues until it downloads
            {
                try
                {
                    DownloadBank();
                    Thread.Sleep(2000);//sleeps before trying
                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// returns list with all the branch banks details
        /// </summary>
        /// <returns></returns>
        public List<BankBranch> GetBanckBranchsCollection()
        { 
            List<BankBranch> banks = ListOfBanks().ToList();
            return banks;
           
        }
        /// <summary>
        /// remove hosting unit from system, throw exception uf it doesnt exist
        /// </summary>
        /// <param name="unitKey"></param>
        public void removeHostingUnit(long unitKey)
        {
            try
            {
                hostingUnitList.RemoveAll(x => x.HostingUnitKey == unitKey);
                LoadDataHostingUnit();
                var hostingUnit = (from item in hostingUnitRoot.Elements()
                                   where Int64.Parse(item.Element("HostingUnitKey").Value) == unitKey
                                   select item).FirstOrDefault();
                hostingUnit.Remove();
                hostingUnitRoot.Save(HostingUnitPath);
            }
            catch
            {
                throw new Exception("מחיקת יחידת אירוח נכשלה");
            }
        }
        /// <summary>
        ///  remove guest request from system, throw exception uf it doesnt exist
        /// </summary>
        /// <param name="requestKey"></param>
        public void removeGuestRequest(long requestKey)
        {
            var request = (from r in GuestRequestRoot.Elements()
                           where Int64.Parse(r.Element("GuestRequestKey").Value) == requestKey
                           select r);
            request.Remove();
            GuestRequestRoot.Save(guestRequestPath);
        }
        /// <summary>
        ///  remove order from system, throw exception uf it doesnt exist
        /// </summary>
        /// <param name="orderKey"></param>
        public void removeOrder(long orderKey)
        {
            var order = (from r in orderRoot.Elements()
                         where Int64.Parse(r.Element("OrderKey").Value) == orderKey
                         select r);
            order.Remove();
            orderRoot.Save(orderPath);
        }
        /// <summary>
        /// convert hosting unit from xelemnt to HostingUnit type
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private HostingUnit ConvertHostingUnit(XElement element)
        {
            try
            {
                HostingUnit unit = new HostingUnit();
                var ser = new XmlSerializer(typeof(HostingUnit));
                return (HostingUnit)ser.Deserialize(element.CreateReader());

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// convert guest request from xelemnt to GuestRequest type
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        GuestRequest ConvertGuestRequest(XElement element)
        {
            try
            {
                GuestRequest request = new GuestRequest
                {
                    GuestRequestKey = Int64.Parse(element.Element("GuestRequestKey").Value),
                    PrivateName = element.Element("PrivateName").Value,
                    FamilyName = element.Element("FamilyName").Value,
                    MailAddress = element.Element("MailAddress").Value,
                    Status = (GuestRequestStatus)Enum.Parse(typeof(GuestRequestStatus), element.Element("Status").Value),
                    RegistrationDate = DateTime.Parse(element.Element("RegistrationDate").Value),
                    EntryDate = DateTime.Parse(element.Element("EntryDate").Value),
                    ReleaseDate = DateTime.Parse(element.Element("ReleaseDate").Value),
                    Area = (Area)Enum.Parse(typeof(Area), element.Element("Area").Value),
                    Type = (TypeOfVication)Enum.Parse(typeof(TypeOfVication), element.Element("Type").Value),
                    Adults = int.Parse(element.Element("Adults").Value),
                    Children = int.Parse(element.Element("Children").Value),
                    Pool = (Pool)Enum.Parse(typeof(Pool), element.Element("Pool").Value),
                    Jacuzzi = (Jacuzzi)Enum.Parse(typeof(Jacuzzi), element.Element("Jacuzzi").Value),
                    Garden = (Garden)Enum.Parse(typeof(Garden), element.Element("Garden").Value),
                    ChildrensAttractions = (ChildrensAttractions)Enum.Parse(typeof(ChildrensAttractions), element.Element("ChildrensAttractions").Value)
                };
                return request;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// convert order from xelemnt to Order type
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private Order ConvertOrder(XElement element)
        {
            try
            {
                Order order = new Order
                {
                    OrderKey = Int64.Parse(element.Element("OrderKey").Value),
                    HostKey = Int64.Parse(element.Element("HostKey").Value),
                    GuestRequestKey = Int64.Parse((element.Element("GuestRequestKey").Value)),
                    HostingUnitKey = Int64.Parse((element.Element("HostingUnitKey").Value)),
                    status = (Status)Enum.Parse(typeof(Status), element.Element("status").Value),
                    OrderDate = Convert.ToDateTime((element.Element("OrderDate").Value)),
                    CreateDate = Convert.ToDateTime((element.Element("CreateDate").Value)),
                    EnteryDate = Convert.ToDateTime(element.Element("EnteryDate").Value),
                    ReleaseDate = Convert.ToDateTime(element.Element("ReleaseDate").Value),
                    TotalPrice = double.Parse(element.Element("TotalPrice").Value)
                };
                return order;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       
    }
}



