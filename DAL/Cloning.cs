using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Reflection;
using System.Runtime.Serialization;

namespace DAL
{
    public static class Cloning
    {
        /// <summary>
        /// returns a copy of the original Guest request
        /// </summary>
        /// <param name="original">to copy</param>
        /// <returns>copy of originals</returns>
        public static GuestRequest Clone(this GuestRequest original)
        {
            GuestRequest target = new GuestRequest();
            target.GuestRequestKey = original.GuestRequestKey;
            target.PrivateName = original.PrivateName;
            target.FamilyName = original.FamilyName;
            target.MailAddress = original.MailAddress;
            target.Status = original.Status;
            target.RegistrationDate = original.RegistrationDate;
            target.EntryDate = original.EntryDate;
            target.ReleaseDate = original.ReleaseDate;
            target.Area = original.Area;
            target.Type = original.Type;
            target.Adults = original.Adults;
            target.Children = original.Children;
            target.Pool = original.Pool;
            target.Jacuzzi = original.Jacuzzi;
            target.Garden = original.Garden;
            target.ChildrensAttractions = original.ChildrensAttractions;
            
            return target;
        }
        /// <summary>
        /// returns a copy of the original Order
        /// </summary>
        /// <param name="original">to copy</param>
        /// <returns>copy of originals</returns>
        public static Order Clone(this Order original)
        {
            Order target = new Order();
            target.HostingUnitKey = original.HostingUnitKey;
            target.GuestRequestKey = original.GuestRequestKey;
            target.OrderKey = original.OrderKey;
            target.status = original.status;
            target.CreateDate = original.CreateDate;
            target.OrderDate = original.OrderDate;
            target.EnteryDate = original.EnteryDate;
            target.ReleaseDate = original.ReleaseDate;
            target.TotalPrice = original.TotalPrice;
            target.HostKey = original.HostKey;
            return target;

        }
        /// <summary>
        /// returns a copy of the original Bank Branch
        /// </summary>
        /// <param name="original">to copy</param>
        /// <returns>copy of originals</returns>
        public static BankBranch Clone(this BankBranch original)
        {
            BankBranch target = new BankBranch();
            target.BankNumber = original.BankNumber;
            target.BankName = original.BankName;
            target.BranchNumber = original.BranchNumber;
            target.BranchAddress = original.BranchAddress;
            target.BranchCity = original.BranchCity;
            return target;
        }
        /// <summary>
        /// returns a copy of the original Host
        /// </summary>
        /// <param name="original">to copy</param>
        /// <returns>copy of originals</returns>
        public static Host Clone(this Host original)
        {
            Host target = new Host();
            target.HostKey = original.HostKey;
            target.PrivateName = original.PrivateName;
            target.FamilyName = original.FamilyName;
            target.FhoneNumber = original.FhoneNumber;
            target.MailAddress = original.MailAddress;
            target.BankBranchDetails = original.BankBranchDetails.Clone();
            target.BankAccountNumber = original.BankAccountNumber;
            target.Commission = original.Commission;
            target.CollectionClearance = original.CollectionClearance;
            target.Password = original.Password;
            
            return target;
        }
        /// <summary>
        /// returns a copy of the original Hosting Unit
        /// </summary>
        /// <param name="original">to copy</param>
        /// <returns>copy of originals</returns>
        public static HostingUnit Clone(this HostingUnit original)
        {
            HostingUnit target = new HostingUnit();
            target.HostingUnitKey = original.HostingUnitKey;
            target.Owner = original.Owner.Clone();
            target.HostingUnitName = original.HostingUnitName;
            target.Area = original.Area;
            target.Type = original.Type;
            target.maxAdults = original.maxAdults;
            target.maxChildrens = original.maxChildrens;
            target.Pool = original.Pool;
            target.Jacuzzi = original.Jacuzzi;
            target.Garden = original.Garden;
            target.ChildrensAttractions = original.ChildrensAttractions;
            target.PricePerNight = original.PricePerNight;
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 31; j++)
                    target.Diary[i, j] = original.Diary[i, j];
            
            return target;
        }
        /// <summary>
        /// returns a copy of the original Hosting unit list
        /// </summary>
        /// <param name="original">to copy</param>
        /// <returns>copy of originals</returns>
        public static List<HostingUnit> Clone(this List<HostingUnit> original)
        {
            List<HostingUnit> target = new List<HostingUnit>();
                foreach (HostingUnit item in original)
                {
                    if (item != null)
                        target.Add(item.Clone());
                }
           
            
            return target;
        }
        /// <summary>
        /// returns a copy of the original Guest request list
        /// </summary>
        /// <param name="original">to copy</param>
        /// <returns>copy of originals</returns>
        public static List<GuestRequest> Clone(this List<GuestRequest> original)
        {
            List<GuestRequest> target = new List<GuestRequest>();
            
         
                foreach (GuestRequest item in original)
                {
                    if (item != null)
                        target.Add(item.Clone());
                }
          
            
            return target;
        }
        /// <summary>
        /// returns a copy of the original Orders list
        /// </summary>
        /// <param name="original">to copy</param>
        /// <returns>copy of originals</returns>
        public static List<Order> Clone(this List<Order> original)
        {
            
            List<Order> target = new List<Order>();
            foreach (Order item in original)
            {
                if (item != null)
                    target.Add(item.Clone());
            }
            
            
            return target;
        }
        public static List<Host> Clone(this List<Host> original)
        {

            List<Host> target = new List<Host>();
            foreach (Host item in original)
            {
                if (item != null)
                    target.Add(item.Clone());
            }


            return target;
        }
    }
}




