

namespace BE
{
    
    public class BankBranch
    {
        public long BankNumber { get; set; }
        public string BankName { get; set; }
        public long BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        
   

        /// <summary>
        /// usu the jeneric function GetPropertyValues to get the result
        /// </summary>
        /// <returns>string with all the deatales about the hosting unit</returns>
        public override string ToString()
        {
            return BE_Tools.GetPropertyValues(this);
        }
    }
}



