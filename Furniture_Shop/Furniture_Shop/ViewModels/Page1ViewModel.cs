using DevExpress.Mvvm;
using System;
using System.Windows.Input;
using Furniture_Shop.Events;
using Furniture_Shop.Messages;
using Furniture_Shop.Views.Pages;
using Furniture_Shop.Services;
using System.Windows;
using Furniture_Shop.Views.Windows;

//using Experimental.System.Messaging;
//using Prism.Commands;
//using Prism.Mvvm;

namespace Furniture_Shop.ViewModels
{
    public class Page1ViewModel : BindableBase
    {

        #region Navigate
        private readonly PageService _pageService;
      
        public string LogText { get; set; }

        public Page1ViewModel(PageService pageService)
        {
            _pageService = pageService;
            
        }

        public ICommand ChangePage => new AsyncCommand(async () =>
        {
            _pageService.ChangePage(new LogPage());

            //await _eventBus.Publish(new LeaveFromFirstPageEvent());
        });


        public ICommand ChangePage_MainUser => new AsyncCommand(async () =>
        {
            _pageService.ChangePage(new MainUserPage());


        });

        #endregion

        #region Логин и Пароль
        private string _login = ""; // поле в котором свойство хранит данные
        public string login
        {
            get => _login;  //Возврашщаем само поле
            set
            {
                //Правильно, но длинно
                if (Equals(_login, value)) return;
                _login = value;
                 RaisePropertyChanged(_login);//говно и палки

            }
        }


        private string _password = "";
        public string password
        {
            get => _password;  //Возврашщаем само поле
            set
            {
                //Правильно, но длинно
                if (Equals(_password, value)) return;
                _password = value;
                RaisePropertyChanged(_password);//говно и палки

            }
        }


        private DelegateCommand _Choose_Kitchen;
        public DelegateCommand Chosed_Kitchen =>
          _Choose_Kitchen ?? (_Choose_Kitchen = new DelegateCommand(Chosen_Kitchen));
        void Chosen_Kitchen()
        {
            password = "sectret";

        }
        #endregion

        #region DialogWindow

        Services.IDialogService __dialogService = new DialogService();

        #region Пользователя не существует

        private DelegateCommand _showDialog;

        public DelegateCommand ShowDialog =>
            _showDialog ?? (_showDialog = new DelegateCommand(ExecuteShowDialog));

        void ExecuteShowDialog()
        {
            __dialogService.ShowDialog("Notification");//пользователя не существует
        }
        #endregion

        #region Имя содержит только латинские символы

        private DelegateCommand _showNameError;

        public DelegateCommand showNameError =>
            _showNameError ?? (_showNameError = new DelegateCommand(ExecuteshowNameError));

        void ExecuteshowNameError()
        {
            __dialogService.ShowDialog("Notification_name");//
        }
        #endregion

        #endregion





        #region Проверка вход
        private DelegateCommand _Check;

        public DelegateCommand Check =>
           _Check ?? (_showDialog = new DelegateCommand(ExecuteCheck));

        void ExecuteCheck()
        {
            if((_login == "admin") && (_password == "admin"))
            {
                _pageService.ChangePage(new AdminPage());

            }
            else
            {
                _pageService.ChangePage(new MainUserPage());

                ExecuteshowNameError(); //окно ошибки
            }
        }
        #endregion

    }
}
