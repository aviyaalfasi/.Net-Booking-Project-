using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        List<Host> AllHosts();
        /// <summary>
        /// add new host to host list
        /// </summary>
        /// <param name="host">to add</param>
        void addHost(Host host);
        /// <summary>
        /// get bank branch details and returns the same bank branch object or 'not exist' exception  
        /// </summary>
        /// <param name="BranchDetails">contains the branch number</param>
        /// <param name="BankDetails">contains the bank name</param>
        /// <returns>BankBrunch object</returns>
        BankBranch FindBranchBank(string BranchDetails, string BankDetails);
        /// <summary>
        /// returns list of all the banks names
        /// </summary>
        /// <returns>list of all the banks names</returns>
        List<string> AllBanksNames();
        /// <summary>
        /// returns list that contains all the branches details
        /// </summary>
        /// <param name="BankDetails"></param>
        /// <returns>list that contains all the branches details</returns>
        List<string> AllBanksBranches(string BankDetails);
        /// <summary>
        /// check hosts identity
        /// </summary>
        /// <param name="id">hosts id</param>
        /// <param name="password">hosts password</param>
        /// <returns>the host, or throw exception</returns>
        Host IsHost(long id, string password);
        /// <summary>
        /// check if a mail adress is valid adress
        /// </summary>
        /// <param name="email">boolean</param>
        /// <returns></returns>
        bool CheckMailAddress(string email);
        /// <summary>
        /// returns true if this is the correct owner details, otherwise false
        /// </summary>
        /// <param name="userName">owners user name</param>
        /// <param name="password"> owners password</param>
        void IsOwner(string userName, string password);
        /// <summary>
        /// returns a list of the hosts hosting units
        /// </summary>
        /// <param name="HostKey"></param>
        /// <returns>list of the hosts hosting units</returns>
        List<HostingUnit> unitsByHost(long HostKey);
        List<HostingUnit> CheckHostingUnits(DateTime EnteryDate, int NumVecationDays);//???
        
        List<Order> OrdersInThePastFewDays(int NumDays);
        List<GuestRequest> SpecificGuestRequests(Predicate<GuestRequest> condition);
        
        void removeHostingUnit( HostingUnit unit);
        void updateOrdersStatus(Order order, Status status);
        void removeGuestRequest(GuestRequest unit);
        
        void CreateGuestRequest(GuestRequest request);
        Order CreateOrder(HostingUnit unit, GuestRequest request);
        void CreateHostingUnit(HostingUnit unit);
        IEnumerable<IGrouping<Area, GuestRequest>> requestsByArea();
        IEnumerable<IGrouping<int, GuestRequest>> requestsByNumberVacationers();
        IEnumerable<IGrouping<int, HostingUnit>> HostsByNumberOfHostingUnits();
        IEnumerable<IGrouping<Area, HostingUnit>> UnitsByArea();
        List<GuestRequest> CreateMatch(HostingUnit unit);
        
        HostingUnit FindHostingUnit(string HostingUnitName);
        void UpdateHostingUnit(HostingUnit unit);
        GuestRequest FindGuestRequest(long GuestRequestKey);
        List<Order> OrdersByHost(long HostKey);
        List<Order> AllOrders();
        List<HostingUnit> AllHostingUnits();
        List<GuestRequest> AllGuestRequest();
        void ChangeGuestRequestStatus(GuestRequest request, GuestRequestStatus status);
        void CheckCollectionClearance(HostingUnit unit, bool PreviousCollectionClearance);
    }
}
