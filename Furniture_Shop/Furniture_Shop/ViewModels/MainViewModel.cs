using DevExpress.Mvvm;
using System.Windows.Controls;
using Furniture_Shop.Views.Pages;
using Furniture_Shop.Views.Windows;
using Furniture_Shop.Services;

namespace Furniture_Shop.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly PageService _pageService;

        public Page PageSource { get; set; }

        public MainViewModel(PageService pageService)
        {
            _pageService = pageService;


            _pageService.OnPageChanged += (page) => PageSource = page;
            _pageService.ChangePage(new Page1());
        }
    }
}
