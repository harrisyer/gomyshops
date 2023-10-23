using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models
{
    public class ActionsListViewModels
    {
        public string IdKey { get; set; } = "";
        public bool checkBoxKey { get; set; }
        public ActionsListDetails DetailJson { get; set; }
        public ActionsListDetails EditJson { get; set; }
        public string ActionListUserType { get; set; } = "";

        public string ModifiedBy { get; set; } = "";
        public string CreatedBy { get; set; } = "";



        //public List<string> ActionLinks { get; set; }
    }//end class

    public class ActionsListDetails
    {
        public string id { get; set; } = "";
        public string id2 { get; set; } = "";
        public string id3 { get; set; } = "";
        public string id4 { get; set; } = "";
        public string id5 { get; set; } = "";

        public ActionsListDetails(string Id, string Id2, string Id3, string Id4, string Id5)
        {
            id = Id;
            id2 = Id2;
            id3 = Id3;
            id4 = Id4;
            id5 = Id5;
           
        }

        //Empty constructor for refund purpose
        public ActionsListDetails()
        {
            id = String.Empty;
            id2 = String.Empty;
            id3 = String.Empty;
            id4 = String.Empty;
            id5 = String.Empty;          
        }
    }//end class


}//end namespace