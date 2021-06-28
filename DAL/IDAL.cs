
using System.Collections.Generic;
using BE;

namespace DAL
{

    public interface IDAL
    {
        void addHost(Host host);
        List<Host> GetHostsCollection();
        void addClientRequest(GuestRequest request);
        void updateGuestRequest(GuestRequest request);
        void addHostingUnit(HostingUnit unit);
        void updateHostingUnit(HostingUnit unit);
        void addOrder(Order order);
        void updateOrder(Order order);
        List<HostingUnit> GetHostingUnitsCollection();
        List<GuestRequest> GetGuestRequestsCollection();
        List<Order> GetOrdersCollection();
        List<BankBranch> GetBanckBranchsCollection();
        void removeHostingUnit(long unitKey);
        void removeGuestRequest(long requestKey);
        void removeOrder(long orderKey);
    }
}
