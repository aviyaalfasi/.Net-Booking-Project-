using System;



namespace BE
{
    public class GuestRequest
    {
        public long GuestRequestKey{ get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress { get; set; }
        public GuestRequestStatus Status { get; set; }
        public DateTime RegistrationDate{get;set;}
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Area Area { get; set; }
        public TypeOfVication Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public Pool Pool { get; set; }
        public Jacuzzi Jacuzzi { get; set; }
        public Garden Garden { get; set; }
        public ChildrensAttractions ChildrensAttractions { get; set; }
        
        public override string ToString()
        {
            return BE_Tools.GetPropertyValues(this);
        } 
    }
}
