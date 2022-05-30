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
    public class AdminOrdersViewModel : BindableBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public PageService _pageService;


        #region Selected List Product Detail
        private ORDERS _order;
        public ORDERS order
        {
            get { return _order; }
            set
            {
                _order = value;
                OnPropertyChanged(nameof(order));
            }
        }

        private ObservableCollection<ORDERS> _lstorders;
        public ObservableCollection<ORDERS> LstOrders
        {
            get { return _lstorders; }
            set
            {
                _lstorders = value;
                OnPropertyChanged(nameof(LstOrders));
            }
        }

        private ORDERS _selectedOrder;
        public ORDERS SelectedOrder
        {
            get { return _selectedOrder; }

            set
            {
                _selectedOrder = value;
                RaisePropertiesChanged(nameof(SelectedOrder));

            }
        }



        private ORDERS _orderDetail = new ORDERS();
        public ORDERS OrderDetail
        {
            get { return _orderDetail; }

            set
            {
                _orderDetail = value;
                RaisePropertiesChanged(nameof(OrderDetail));

            }
        }
        #endregion


        Shop_NewEntities2 bD_Furniture;
        public Page PageSource { get; set; }
        public AdminOrdersViewModel(PageService pageService)
        {
            bD_Furniture = new Shop_NewEntities2();
            LoadBD_Furniture();
            UpdateCommand = new Command((s) => true, Update);
            DeleteCommand = new Command((s) => true, Delete);
            UpdateOrder = new Command((s) => true, UpdateProductCommand);
            AddOrderCommand = new Command((s) => true, AddProductsCommand);

            ///
            _pageService = pageService;
            _pageService.OnPageChanged += (page) => PageSource = page;
            _pageService.ChangePage(new Page1());

        }
        private void LoadBD_Furniture()// Read
        {
            LstOrders = new ObservableCollection<ORDERS>((IEnumerable<ORDERS>)bD_Furniture.ORDERS);
        }


        private void AddProductsCommand(object obj)
        {
             bD_Furniture.ORDERS.Add(OrderDetail);
             bD_Furniture.SaveChanges();
             LstOrders.Add(OrderDetail);
            OrderDetail = new ORDERS();

        }
        #region Обновить_Команда

        private void UpdateProductCommand(object obj)
        {
            bD_Furniture.SaveChanges();
        }



        private void Update(object obj)
        {
            SelectedOrder = obj as ORDERS;
        }

        #endregion

        #region Команда Удалить 
        private void Delete(object obj)
        {
            var pro = obj as ORDERS;
            bD_Furniture.ORDERS.Remove(pro);
            bD_Furniture.SaveChanges();
            LstOrders.Remove(pro);
        }
        #endregion

        
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateOrder { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand AddOrderCommand { get; set; }


        #region Nav

        Services.IDialogService __dialogService = new DialogService();

        private DelegateCommand _showDialog;

        private DelegateCommand _exit;

        public DelegateCommand Exit_Delegate =>
           _exit ?? (_showDialog = new DelegateCommand(Exit));

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

        //users
        private DelegateCommand _nav_to_users;

        public DelegateCommand Nav_To_USER =>
           _nav_to_users ?? (_showDialog = new DelegateCommand(Navigate_to_Users));

        void Navigate_to_Users()
        {

            _pageService.ChangePage(new Admin_Users());

        }
        #endregion

    }
}
