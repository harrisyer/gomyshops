using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMyShops.Models
{
    public abstract class ErrorsModels
    {
        public bool IsError { get; set; } = true;
        public string ErrorMessages { get; set; } = string.Empty;
    }
}
