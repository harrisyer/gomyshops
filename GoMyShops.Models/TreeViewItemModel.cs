using System;
using System.Collections.Generic;
using System.Linq;

namespace GoMyShops.Models
{
    public class TreeViewItemModel
    {
        public string text { get; set; }
        public string nodeId { get; set; }
        public string nodeCode { get; set; }
        public string parentId { get; set; }
        public string parentCode { get; set; }
        public string type { get; set; }
        public string href { get; set; }
        public string color { get; set; }
        public string backColor { get; set; }
        public string selectedBackColor { get; set; }
        public string icon { get; set; }
        public string path { get; set; }
        public string[] tags { get; set; }
        public bool selectable { get; set; }
        public bool checkedByDefault { get; set; }
        public List<TreeViewItemModel> nodes { get; set; }
        public List<string> state { get; set; }
    }//end class

    public class TreeViewSelectItemsModel
    {
        public string userName { get; set; }
        public List<TreeViewSelectItemModel> nodes { get; set; }
    }

    public class TreeViewSelectItemModel
    {
        public string nodeCode { get; set; }
        public string nodeType { get; set; }
        public string nodeparentId { get; set; }
        public string nodeparentCode { get; set; }
        public bool nodeStatus { get; set; }
    }

    public class TreeViewStateModel
    {
        public bool check { get; set; }
      
}

    //public class TreeViewTagModel
    //{
    //    public TreeViewTagModel(int tag)
    //    {
    //        tags = tag;
    //    }

    //    public int tags { get; set; }
    //}
}//end namespace