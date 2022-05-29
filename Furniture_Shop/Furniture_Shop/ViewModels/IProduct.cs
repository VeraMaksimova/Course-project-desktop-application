using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture_Shop.Models;
using
System.Collections.ObjectModel;

namespace Furniture_Shop.ViewModels
{
   public interface IProduct
    {
        ObservableCollection<PRODUCT> LstProduct { get; set; }
    }
}
