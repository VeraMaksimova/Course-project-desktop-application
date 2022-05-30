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
    public class AdminViewModel : BindableBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public PageService _pageService;


        #region Selected List Product Detail
        private PRODUCT _product;
        public PRODUCT product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged(nameof(product));
            }
        }
  
        private ObservableCollection<PRODUCT> _lstproduct;
        public ObservableCollection<PRODUCT> Lstproduct
        {
            get { return _lstproduct; }
            set
            {
                _lstproduct = value;
                OnPropertyChanged(nameof(Lstproduct));
            }
        }

        private PRODUCT _selectedProduct;
        public PRODUCT SelectedProduct
        {
            get { return _selectedProduct; }

            set {
                _selectedProduct = value;
               RaisePropertiesChanged(nameof(SelectedProduct));
                    
            }
        }

        

        private PRODUCT _productDetail = new PRODUCT();
        public PRODUCT ProductDetail
        {
            get { return _productDetail; }

            set
            {
                _productDetail = value;
                RaisePropertiesChanged(nameof(ProductDetail));

            }
        }
        #endregion

        Shop_NewEntities2 bD_Furniture;
        public Page PageSource { get; set; }
        public AdminViewModel(PageService pageService)
        {
            bD_Furniture = new Shop_NewEntities2();
            LoadBD_Furniture();
            UpdateCommand = new Command((s) => true, Update);
            DeleteCommand = new Command((s) => true, Delete);
            UpdateProduct = new Command((s) => true, UpdateProductCommand);
            AddProductCommand = new Command((s) => true, AddProductsCommand);

            ///
            _pageService = pageService;


            _pageService.OnPageChanged += (page) => PageSource = page;
            _pageService.ChangePage(new Page1());

        }
        private void LoadBD_Furniture()// Read
        {
            Lstproduct = new ObservableCollection<PRODUCT>((IEnumerable<PRODUCT>)bD_Furniture.PRODUCT);
        }

        #region Навигация
        private DelegateCommand _exit;

        public DelegateCommand Exit_Delegate =>
           _exit ?? (_showDialog = new DelegateCommand(Exit));

        void Exit()
        {
            
                _pageService.ChangePage(new Page1());

        }

        //Orders
        private DelegateCommand _ChangeToOrders;

        public DelegateCommand Change_Delegate =>
           _ChangeToOrders ?? (_showDialog = new DelegateCommand(Change_To_Orders));

        void Change_To_Orders()
        {

            _pageService.ChangePage(new AdminPageOrders());

        }

        //users
        private DelegateCommand _ChangeToUsers;

        public DelegateCommand Change_Users =>
            _ChangeToUsers ?? (_showDialog = new DelegateCommand(Change_To_Users));

        void Change_To_Users()
        {

            _pageService.ChangePage(new Admin_Users());

        }
        #endregion

        #region Сервисы
        Services.IDialogService __dialogService = new DialogService();

        private DelegateCommand _showDialog;

        public DelegateCommand ShowDialog =>
            _showDialog ?? (_showDialog = new DelegateCommand(ExecuteShowDialog));

        void ExecuteShowDialog()
        {
            __dialogService.ShowDialog("FieldEmpty");//Все поля должны быть заполнены
        }
        #endregion

        #region Добавление
        private void AddProductsCommand(object obj)
        {
           
            if(String.IsNullOrEmpty(ProductDetail.product_name) || String.IsNullOrEmpty(ProductDetail.color) 
                || String.IsNullOrEmpty(ProductDetail.product_description) 
                || String.IsNullOrEmpty(ProductDetail.photo_1) || String.IsNullOrEmpty(ProductDetail.photo_2) || String.IsNullOrEmpty(ProductDetail.photo_3))
            {
                ExecuteShowDialog();
            }
            else
            {
                //bD_Furniture.PRODUCT.Add(ProductDetail);
                bD_Furniture.PRODUCT.Add(ProductDetail);
                bD_Furniture.SaveChanges();
                Lstproduct.Add(ProductDetail);
                ProductDetail = new PRODUCT();
            }
           
        }
        #endregion

        #region Обновить_Команда

        private void UpdateProductCommand(object obj)
        {
            bD_Furniture.SaveChanges();
        }



        private void Update(object obj)
        {
            SelectedProduct = obj as PRODUCT;
        }

        #endregion

        #region Команда Удалить 
        private void Delete(object obj)
        {
            var pro = obj as PRODUCT;
            bD_Furniture.PRODUCT.Remove(pro);
            bD_Furniture.SaveChanges();
            Lstproduct.Remove(pro);
        }
    #endregion

       

        #region
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateProduct { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        #endregion


    }


    public class Command : ICommand
    {
        public Command(Func<object,bool> methodCanExecute, Action<object> methodExecute)
        {
            MethodExecute = methodExecute;
            MethodCanExecute = methodCanExecute;
        }
        private Action<object> MethodExecute { get; set; }
        private Func<object,bool> MethodCanExecute { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parametr)
        {
            return MethodExecute != null && MethodCanExecute.Invoke(parametr);
        }
        public void Execute(object parametr)
        {
            MethodExecute(parametr);
        }
    }
}


