using System;
using System.Windows.Controls;
using Furniture_Shop.Views.Pages;

namespace Furniture_Shop.Services
{
    public class PageService
    {

        public event Action<Page> OnPageChanged;
        public void ChangePage(Page page) => OnPageChanged?.Invoke(page);
    }
}
