using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Furniture_Shop.Views.Pages;
using Furniture_Shop.Views;
using Furniture_Shop.Models;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Data;
using Furniture_Shop.Services;
using System.Windows.Controls;
using Furniture_Shop.Views.Windows;
using System.Windows.Data;

namespace Furniture_Shop.ViewModels
{
    public class AdminUsersViewModel : BindableBase
    {
        #region INotify
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public PageService _pageService;
        #endregion

        #region Selected List Product Detail
        private USER_ _user;
        public USER_ user
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(user));
            }
        }

        private ObservableCollection<USER_> _lstusers;
        public ObservableCollection<USER_> LstUsers
        {
            get { return _lstusers; }
            set
            {
                _lstusers = value;
                OnPropertyChanged(nameof(LstUsers));
            }
        }

        private USER_ _selectedUser;
        public USER_ SelectedUser
        {
            get { return _selectedUser; }

            set
            {
                _selectedUser = value;
                RaisePropertiesChanged(nameof(SelectedUser));

            }
        }



        private USER_ _userDetail = new USER_();
        public USER_ UserDetail
        {
            get { return _userDetail; }

            set
            {
                _userDetail = value;
                RaisePropertiesChanged(nameof(UserDetail));

            }
        }
        #endregion

        Shop_NewEntities2 bD_Furniture;
        public Page PageSource { get; set; }

        public AdminUsersViewModel(PageService pageService)
        {
            bD_Furniture = new Shop_NewEntities2();
            LoadBD_Furniture();
            UpdateCommand = new Command((s) => true, Update);
            DeleteCommand = new Command((s) => true, Delete);
            UpdateUser = new Command((s) => true, UpdateUserCommand);
            AddUserCommand = new Command((s) => true, AddProductsCommand);

            ///
            _pageService = pageService;
            _pageService.OnPageChanged += (page) => PageSource = page;
            _pageService.ChangePage(new Page1());

        }

        private void LoadBD_Furniture()// Read
        {
            LstUsers = new ObservableCollection<USER_>((IEnumerable<USER_>)bD_Furniture.USER_);
        }

        #region Commands
        private void AddProductsCommand(object obj)
        {
            bD_Furniture.USER_.Add(UserDetail);
            bD_Furniture.SaveChanges();
            LstUsers.Add(UserDetail);
            UserDetail = new USER_();
        }

        private void UpdateUserCommand(object obj)
        {
            bD_Furniture.SaveChanges();
        }

        private void Update(object obj)
        {
            SelectedUser = obj as USER_;
        }

        #region Команда Удалить 
        private void Delete(object obj)
        {
            var pro = obj as USER_;
            bD_Furniture.USER_.Remove(pro);
            bD_Furniture.SaveChanges();
            LstUsers.Remove(pro);
        }
        #endregion

        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateUser { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand AddUserCommand { get; set; }
        #endregion


        #region Navigate

        Services.IDialogService __dialogService = new DialogService();

        private DelegateCommand _showDialog;

        private DelegateCommand _exit;

        public DelegateCommand Exit_Delegate =>
           _exit ?? (_showDialog= new DelegateCommand(Exit));

        void Exit()
        {

            _pageService.ChangePage(new Page1());

        }

        // products

        private DelegateCommand _nav_to_products;

        public DelegateCommand Nav_To_PRODUCT =>
           _nav_to_products ?? (_showDialog = new DelegateCommand(Navigate_to_Products));

        void Navigate_to_Products()
        {

            _pageService.ChangePage(new AdminPage());

        }

        //orders
        private DelegateCommand _nav_to_orders;

        public DelegateCommand Nav_To_ORDERS =>
           _nav_to_orders ?? (_showDialog = new DelegateCommand(Navigate_to_Orders));

        void Navigate_to_Orders()
        {

            _pageService.ChangePage(new AdminPageOrders());

        }
        #endregion
    }
}
