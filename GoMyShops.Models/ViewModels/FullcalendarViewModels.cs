using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Syntrino.TableauApps.Models.ViewModels
{
    public class FullcalendarViewModels
    {
        public string id { get; set; }

        public string title { get; set; }

        public bool allDay { get; set; }

       

        public string start { get; set; }

        public string end { get; set; }

        public string url { get; set; }

        public string className { get; set; }

        public string rendering { get; set; }
        public string color { get; set; }
        public string textColor { get; set; }
        public string description { get; set; }
        public string buscode { get; set; }
        public DateTime DepartureTime { get; set; }
        public string sDepartureTime { get; set; }
        public string routecode { get; set; }
        public string routeName { get; set; }
        public string routeschedulecode { get; set; }
        public bool isrepeat { get; set; }
        public bool isdeleted { get; set; }
        public string eventstatus { get; set; } 
    }//end class
}//end namespace
