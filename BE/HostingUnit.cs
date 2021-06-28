using System;
using System.Xml.Serialization;

namespace BE
{
    public class HostingUnit
    {
        public long HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        [XmlIgnore]
        public bool[,] Diary= new bool[12, 31];
        [XmlArray("Diary")]
        public bool[] DiaryDto
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(12); }
        }
        

        public Area Area { get; set; }
        public TypeOfVication Type { get; set; }
        public int maxAdults { get; set; }
        public int maxChildrens { get; set; }
        public bool Pool { get; set; }
        public bool Jacuzzi { get; set; }
        public bool Garden { get; set; }
        public bool ChildrensAttractions { get; set; }
        public double PricePerNight { get; set; }

        
        /// <summary>
        /// usu the jeneric function GetPropertyValues to get the result
        /// </summary>
        /// <returns>string with all the deatales about the hosting unit</returns>
        public override string ToString()
        {
            string result = "Type is:  HostingUnit\n";
            result += string.Format("Hosting Unit key : {0}\n", HostingUnitKey);
            //result += string.Format("Owner : {0}\n", Owner.ToString());
            result += string.Format("Hosting Unit name : {0}\n", HostingUnitName);
            result += string.Format("Area : {0}\n", Area);
            result += string.Format("Type of vecation : {0}\n", Type);
            result += string.Format("Maximum adults : {0}\n", maxAdults);
            result += string.Format("Maximum childrens : {0}\n", maxChildrens);
            result += string.Format("Pool : {0}\n", Pool);
            result += string.Format("Jacuzzi : {0}\n", Jacuzzi);
            result += string.Format("Garden : {0}\n", Garden);
            result += string.Format("ChildrensAttractions : {0}\n", ChildrensAttractions);
            result += string.Format("Price per night : {0}\n", PricePerNight);

            bool flag;
            DateTime date = new DateTime();
            string temp_date = "01/01";
            flag = DateTime.TryParse(temp_date, out date);
            if (flag == true)
            {
                for (int j = date.Month - 1; j < 12; j++)
                {
                    for (int i = date.Day - 1; i < 31; i++)
                    {
                        if (Diary[j, i] == true)//If the start of hosting is detected
                        {
                            temp_date = (i + 1) + "/" + (j + 1);
                            DateTime.TryParse(temp_date, out date);
                            result += string.Format("Entery date: {0}{1}{2}, release date: ", date.Day, "/", date.Month);
                            while (Diary[date.Month - 1, date.Day - 1] == true)//Checks how long the accommodation period lasts
                            {
                                date = date.AddDays(1);
                            }
                            result += string.Format("{0}{1}{2}\n", date.Day, "/", date.Month);
                            i = date.Day - 1;
                            j = date.Month - 1;
                        }
                    }
                }
            }
            return result;
        }
    }
}
