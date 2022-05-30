using DevExpress.Mvvm;
using System;
using System.Windows.Input;
using Furniture_Shop.Events;
using Furniture_Shop.Messages;
using Furniture_Shop.Views.Pages;
using Furniture_Shop.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Furniture_Shop.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furniture_Shop.Views;
using System.Windows.Data;
using System.Windows;

namespace Furniture_Shop.ViewModels
{
  

    public class UserMainPageViewModel : BindableBase,   IProduct
    {

        #region RadioButton

     
        #endregion

        private readonly PageService _pageService;

        public UserMainPageViewModel(PageService pageService)
        {
            _pageService = pageService;

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connString))
            {
                if (conn == null)
                {
                    throw new Exception("Error");
                }
                else
                {

                    SqlCommand query = new SqlCommand("SELECT * from PRODUCT ", conn);
                    conn.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        PRODUCT p = new PRODUCT();
                        p.id_product = (int)row["id_product"];
                        p.product_name = row["product_name"].ToString();
                        p.price = (decimal)row["price"];
                        p.amount = (int)row["amount"];
                        p.color = row["color"].ToString();
                        p.product_description = row["product_description"].ToString();
                        p.photo_1 = row["photo_1"].ToString();
                        p.photo_2 = row["photo_2"].ToString();
                        p.photo_3 = row["photo_3"].ToString();

                        LstProduct.Add(p);
                    }

                }

            }
            ProductCollection = CollectionViewSource.GetDefaultView(LstProduct);
            ProductCollection.Filter = Filter;
        }
        private ObservableCollection<PRODUCT> _lstProduct = new ObservableCollection<PRODUCT>();
        public ObservableCollection<PRODUCT> LstProduct
        {
            get { return _lstProduct; }
            set
            {
                this._lstProduct = value;
                RaisePropertyChanged("LstProduct");
            }
        }

        private ICollectionView _productCollection;
        public ICollectionView ProductCollection
        {
            get { return _productCollection; }
            set
            {
                _productCollection = value;
                RaisePropertyChanged("ProductCollection");
            }
        }

        private bool Filter(object emp)
        {
            PRODUCT product = emp as PRODUCT;
            if (!string.IsNullOrEmpty(product_name) && !string.IsNullOrEmpty(color) && !string.IsNullOrEmpty(product_description))
                return product.color.Contains(color) && product.product_name.Contains(product_name) && product.product_description.Contains(product_description);
            else if (string.IsNullOrEmpty(product_name))
                return product.color.Contains(color) && product.product_description.Contains(product_description);
            else if(string.IsNullOrEmpty(color))
                return product.product_description.Contains(product_description) && product.product_name.Contains(product_name);
            else 
                return product.product_description.Contains(product_description) && product.product_name.Contains(product_name) && product.color.Contains(color);
        }


        private DelegateCommand _Choose_Kitchen;
        public DelegateCommand Chosed_Kitchen =>
          _Choose_Kitchen ?? (_Choose_Kitchen = new DelegateCommand(Chosen_Kitchen));
        void Chosen_Kitchen()
        {
            product_description = "Кухня";
            ProductCollection.Refresh();

        }



        #region Filters Color and Catagory

        private DelegateCommand _Choose_All;
        public DelegateCommand Chosed_All =>
          _Choose_All ?? (_Choose_All = new DelegateCommand(Chosen_All));
        void Chosen_All()
        {
            product_description = "";
            ProductCollection.Refresh();

        }


        private DelegateCommand _Choose_Kabinet;
        public DelegateCommand Chosed_Kabinet =>
          _Choose_Kabinet ?? (_Choose_Kabinet = new DelegateCommand(Chosen_Kabinet));
        void Chosen_Kabinet()
        {
            product_description = "Кабинет";
            ProductCollection.Refresh();

        }

        private DelegateCommand _Choose_Spalna;
        public DelegateCommand Chosed_Spalna =>
          _Choose_Spalna ?? (_Choose_Spalna = new DelegateCommand(Chosen_Spalna));
        void Chosen_Spalna()
        {
            product_description = "Спальня";
            ProductCollection.Refresh();

        }

        private DelegateCommand _Choose_Gostinnau;
        public DelegateCommand Chosed_Gostinnau =>
          _Choose_Gostinnau ?? (_Choose_Gostinnau = new DelegateCommand(Chosen_Gostinnau));
        void Chosen_Gostinnau()
        {
            product_description = "Гостинная";
            ProductCollection.Refresh();

        }

        private DelegateCommand _Choose_Red;
        public DelegateCommand Chosed_Red =>
           _Choose_Red ?? (_Choose_Red = new DelegateCommand(Chosen_Red));
        void Chosen_Red()
        {
            color = "Красный";
            ProductCollection.Refresh();

        }

        private DelegateCommand _Choose_Green;
        public DelegateCommand Chosed_Green =>
          _Choose_Green ?? (_Choose_Green = new DelegateCommand(Chosen_Green));
        void Chosen_Green()
        {
            color = "Зеленый";
            ProductCollection.Refresh();

        }


        private DelegateCommand _Choose_Yellow;
        public DelegateCommand Chosed_Yellow =>
          _Choose_Yellow ?? (_Choose_Yellow = new DelegateCommand(Chosen_Yellow));
        void Chosen_Yellow()
        {
            color = "Желтый";
            ProductCollection.Refresh();
        }

        private DelegateCommand _Choose_Orange;
        public DelegateCommand Chosed_Orange =>
          _Choose_Orange ?? (_Choose_Orange = new DelegateCommand(Chosen_Orange));
        void Chosen_Orange()
        {
            color = "Оранжевый";
            ProductCollection.Refresh();
        }

        private DelegateCommand _Choose_Pink;
        public DelegateCommand Chosed_Pink =>
          _Choose_Pink ?? (_Choose_Pink = new DelegateCommand(Chosen_Pink));
        void Chosen_Pink()
        {
            color = "Розовый";
            ProductCollection.Refresh();
        }

        private DelegateCommand _Choose_Purple;
        public DelegateCommand Chosed_Purple =>
          _Choose_Purple ?? (_Choose_Purple = new DelegateCommand(Chosen_Purple));
        void Chosen_Purple()
        {
            color = "Фиолетовый";
            ProductCollection.Refresh();
        }

        private DelegateCommand _Choose_Blue;
        public DelegateCommand Chosed_Blue =>
          _Choose_Blue ?? (_Choose_Blue = new DelegateCommand(Chosen_Blue));
        void Chosen_Blue()
        {
            color = "Синий";
            ProductCollection.Refresh();
        }


        private DelegateCommand _Choose_LightBlue;
        public DelegateCommand Chosed_LightBlue =>
          _Choose_LightBlue ?? (_Choose_LightBlue = new DelegateCommand(Chosen_LightBlue));
        void Chosen_LightBlue()
        {
            color = "Голубой";
            ProductCollection.Refresh();
        }


        private DelegateCommand _Choose_White;
        public DelegateCommand Chosed_White =>
          _Choose_White ?? (_Choose_White = new DelegateCommand(Chosen_White));
        void Chosen_White()
        {
            color = "Белый";
            ProductCollection.Refresh();
        }

        private DelegateCommand _Choose_Brown;
        public DelegateCommand Chosed_Brown =>
          _Choose_Brown ?? (_Choose_Brown = new DelegateCommand(Chosen_Brown));
        void Chosen_Brown()
        {
            color = "Коричневый";
            ProductCollection.Refresh();
        }


        private DelegateCommand _Choose_Black;
        public DelegateCommand Chosed_Black =>
        _Choose_Black ?? (_Choose_Black = new DelegateCommand(Chosen_Black));
        void Chosen_Black()
        {
            color = "Черный";
            ProductCollection.Refresh();
        }

        private DelegateCommand _Choose_None;
        public DelegateCommand Chosed_None =>
        _Choose_None ?? (_Choose_None = new DelegateCommand(Chosen_None));
        void Chosen_None()
        {
            color = "";
            ProductCollection.Refresh();
        }

        #endregion

        private string _product_name = string.Empty;
        public string product_name
        {
            get { return _product_name; }
            set
            {
                if (Equals(_product_name, value)) return;
                _product_name = value;
                OnPropertyChanged(_product_name);
                ProductCollection.Refresh();
            }
        }
        private string _color = string.Empty;
        public string color
        {
            get { return _color; }
            set
            {
                if (Equals(_color, value)) return;
                _color = value;
                OnPropertyChanged(_color);
                ProductCollection.Refresh();
            }
        }

        private string _product_description = string.Empty;
        public string product_description
        {
            get { return _product_description; }
            set
            {
                if (Equals(_product_description, value)) return;
                _product_description = value;
                OnPropertyChanged(_product_description);
                ProductCollection.Refresh();
            }
        }

        private decimal _lower_price;
        public decimal lower_price {
            get { return _lower_price; }
            set
            {
                if (Equals(_lower_price, value)) return;
                _lower_price = value;
                RaisePropertyChanged("_lower_price");
                ProductCollection.Refresh();
            }
        }


        #region Navigation
        public ICommand ChangePage => new AsyncCommand(async () =>
        {
            _pageService.ChangePage(new Page1());
        });


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        #endregion

      
       

    }
}
