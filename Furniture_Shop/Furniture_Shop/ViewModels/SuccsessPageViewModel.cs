using DevExpress.Mvvm;
using System;
using System.Windows.Input;
using Furniture_Shop.Events;
using Furniture_Shop.Messages;
using Furniture_Shop.Views.Pages;
using Furniture_Shop.Services;

namespace Furniture_Shop.ViewModels
{
   public  class SuccsessPageViewModel :  BindableBase
   {

        private readonly PageService _pageService;
        private readonly EventBus _eventBus;
        private readonly MessageBus _messageBus;

        public string LogText { get; set; }

        public SuccsessPageViewModel(PageService pageService, EventBus eventBus, MessageBus messageBus)
        {
            _pageService = pageService;

        }

        public ICommand ChangePage => new AsyncCommand(async () =>
        {
            _pageService.ChangePage(new Page1());

            //await _eventBus.Publish(new LeaveFromFirstPageEvent());
        });



        //private readonly PageService _pageService;
      
        
        //public ICommand ChangePage => new DelegateCommand(() =>
        //{
        //    _pageService.ChangePage(new Page1());
        //});

    }
}
