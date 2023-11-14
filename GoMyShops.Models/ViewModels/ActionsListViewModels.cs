using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models.ViewModels
{
    public class ActionsListViewModels
    {
        public string IdKey { get; set; } = string.Empty;
        public bool checkBoxKey { get; set; }
        public ActionsListDetails? DetailJson { get; set; }
        public ActionsListDetails? EditJson { get; set; }
        public string ActionListUserType { get; set; } = string.Empty;

        public string ModifiedBy { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;



        //public List<string> ActionLinks { get; set; }
    }//end class

    public class ActionsListDetails
    {
        public string id { get; set; } = string.Empty;
        public string id2 { get; set; } = string.Empty;
        public string id3 { get; set; } = string.Empty;
        public string id4 { get; set; } = string.Empty;
        public string id5 { get; set; } = string.Empty;
        public string id6 { get; set; } = string.Empty;
        public string id7 { get; set; } = string.Empty;
        public string id8 { get; set; } = string.Empty;
        public string id9 { get; set; } = string.Empty;
        public string id10 { get; set; } = string.Empty;
        public string id11 { get; set; } = string.Empty;
        public string id12 { get; set; } = string.Empty;
        public string id13 { get; set; } = string.Empty;
        public string id14 { get; set; } = string.Empty;
        public string id15 { get; set; } = string.Empty;

        public ActionsListDetails(string Id, string Id2, string Id3, string Id4, string Id5)
        {
            id = Id;
            id2 = Id2;
            id3 = Id3;
            id4 = Id4;
            id5 = Id5;        
        }
        public ActionsListDetails(string Id, string Id2, string Id3, string Id4, string Id5, string Id6 , string Id7 , string Id8 , string Id9 ,
             string Id10 , string Id11 , string Id12 , string Id13 , string Id14 , string Id15 )
        {
            id = Id;
            id2 = Id2;
            id3 = Id3;
            id4 = Id4;
            id5 = Id5;
            id6 = Id6;
            id7 = Id7;
            id8 = Id8;
            id9 = Id9;
            id10 = Id10;
            id11 = Id11;
            id12 = Id12;
            id13 = Id13;
            id14 = Id14;
            id15 = Id15;
        }

        //Empty constructor for refund purpose
        public ActionsListDetails()
        {
            id = String.Empty;
            id2 = String.Empty;
            id3 = String.Empty;
            id4 = String.Empty;
            id5 = String.Empty;
            id6 = String.Empty;
            id7 = String.Empty;
            id8 = String.Empty;
            id9 = String.Empty;
            id10 = String.Empty;
            id11 = String.Empty;
            id12 = String.Empty;
            id13 = String.Empty;
            id14 = String.Empty;
            id15 = String.Empty;
        }
    }//end class


}//end namespace