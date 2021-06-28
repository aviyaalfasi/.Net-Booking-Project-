

namespace BE
{
    public class Host
    {
        public long HostKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string FhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public BankBranch BankBranchDetails { get; set; }
        public long BankAccountNumber { get; set; }
        public double Commission { get; set; }
        public bool CollectionClearance{ get; set; }
        public string Password { get; set; }
        /// <summary>
        /// usu the jeneric function GetPropertyValues to get the result
        /// </summary>
        /// <returns>string with the host details</returns>
        public override string ToString()
        {
            return BE_Tools.GetPropertyValues(this);
        }

    }
}
