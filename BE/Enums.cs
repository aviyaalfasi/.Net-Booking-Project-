

using System.ComponentModel;

namespace BE
{
    public enum Status { NotYetAddressed, SentMail, ClosedNonClientResponse, ClosedClientResponse }
    public enum Area
    {
        [Description("הכל")]
        All,
        [Description("צפון")]
        North,
        [Description("דרום")]
        South,
        [Description("מרכז")]
        Center,
        [Description("ירושלים")]
        Jerusalem
    }
    public enum TypeOfVication
    {
        [Description("צימר")]
        Zimmer,
        [Description("מלון")]
        Hotel,
        [Description("קמפינג")]
        Camping,
        [Description("וילה")]
        Villa,
        [Description("דירת אירוח")]
        HostingApartment
    }
    public enum Pool { Necessary, possible, uninterested }
    public enum Jacuzzi { Necessary, possible, uninterested }
    public enum Garden { Necessary, possible, uninterested }
    public enum ChildrensAttractions { Necessary, possible, uninterested }
    public enum GuestRequestStatus { Open, DealClosed, ExpiresClose }


}
