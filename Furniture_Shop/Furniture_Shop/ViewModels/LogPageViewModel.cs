using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Furniture_Shop.Events;
using Furniture_Shop.Messages;
using Furniture_Shop.Views.Pages;
using Furniture_Shop.Views.Windows;
using Furniture_Shop.Services;

namespace Furniture_Shop.ViewModels
{
    public class LogPageViewModel : BindableBase
    {
        private readonly PageService _pageService;

        public LogPageViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        #region Change_Page
        public ICommand ChangePage => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Page1());
        });


        public ICommand ChangePage_Reg => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new PageRegistartionSuccsess());
        });

        #endregion

        #region Пользователя не существует

        Services.IDialogService __dialogService = new DialogService();

        private DelegateCommand _showDialog;

        public DelegateCommand ShowDialog =>
            _showDialog ?? (_showDialog = new DelegateCommand(ExecuteShowDialog));

        void ExecuteShowDialog()
        {
            __dialogService.ShowDialog("Notification_name");//логин
        }


        #endregion

        #region Длина логина
        private DelegateCommand _EmptyField;

        public DelegateCommand EmptyField =>
            _EmptyField ?? (_EmptyField = new DelegateCommand(Notification_Lenght));

        void Notification_Lenght()
        {
            __dialogService.ShowDialog("Notification_Length");//длина пароля
        }

        #endregion

        #region Длина адреса
        private DelegateCommand _Length_of_Adress;

        public DelegateCommand Length_of_Adress =>
            _Length_of_Adress ?? (_Length_of_Adress = new DelegateCommand(Length_Adress_));

        void Length_Adress_()
        {
            __dialogService.ShowDialog("Length_Adress");//поле пустое
        }
        #endregion
        #region Длинна пароля
        private DelegateCommand _LengthPassword;

        public DelegateCommand LengthPassword =>
            _LengthPassword ?? (_LengthPassword = new DelegateCommand(Notification_Lenght_Password));

        void Notification_Lenght_Password()
        {
            __dialogService.ShowDialog("Password_Length");//длина пароля
        }
        #endregion

        #region IncorrectText
        private DelegateCommand _InCorrectText;

        public DelegateCommand CorrectText =>
            _InCorrectText ?? (_InCorrectText = new DelegateCommand(ExecuteInCorrectText));

        void ExecuteInCorrectText()
        {
            __dialogService.ShowDialog("IncorrectText");//поле пустое
        }

        #endregion

        #region CorrectAGE
        private DelegateCommand _CorrectAge;

        public DelegateCommand CorrectAge =>
            _CorrectAge ?? (_CorrectAge = new DelegateCommand(ExecuteCorrectAge));

        void ExecuteCorrectAge()
        {
            __dialogService.ShowDialog("Age");//поле пустое
        }

        #endregion

        #region UNCorrectAGE
        private DelegateCommand _UnCorrectAge;

        public DelegateCommand UnCorrectAge =>
            _UnCorrectAge ?? (_UnCorrectAge = new DelegateCommand(ExecuteUnCorrectAge));

        void ExecuteUnCorrectAge()
        {
            __dialogService.ShowDialog("UnCorrectAge");//поле пустое
        }

        #endregion

        #region Length
        private DelegateCommand _Length_of_Login_Password;

        public DelegateCommand Length_of_Login_Password =>
            _Length_of_Login_Password ?? (_Length_of_Login_Password = new DelegateCommand(Length_Dialog));

        void Length_Dialog()
        {
            __dialogService.ShowDialog("Notification_Length");//поле пустое
        }


        #endregion

        #region Логин 
     

        private string _login = " ";
        public string Login
        {
            get => _login;
            set
            {
                if (Equals(_login, value)) return;
                _login = value;
                RaisePropertyChanged(_login);
            }
        }
        #endregion

        #region Password
        private string _password = "";
        public string Password
        {
            get => _password;
            set
            {
                if (Equals(_password, value)) return;
                _password = value;
                RaisePropertyChanged(_login);
            }
        }

        #endregion

        #region Name
        private string _name = " ";
        public string Name
        {
            get => _name;
            set
            {
                if (Equals(_name, value)) return;
                _name = value;
                RaisePropertyChanged(_name);
            }
        }
        #endregion

        #region Surname
        private string _surname = " ";
        public string Surname
        {
            get => _surname;
            set
            {
                if (Equals(_surname, value)) return;
                _surname = value;
                RaisePropertyChanged(_surname);
            }
        }
        #endregion

        #region Age
        private string _age = "";
        public string Age
        {
            get { return _age; }
            set
            {
                if (Equals(_age, value)) return;
                _age = value;
                RaisePropertyChanged("_age");
            }

        }
        #endregion

        #region Adress
        private string _adress = "";
        public string Adress
        {
            get { return _adress; }
            set
            {
                if (Equals(_adress, value)) return;
                _adress = value;
                RaisePropertyChanged(_adress);
            }
        }
        #endregion

        #region Check

        private DelegateCommand _Check;
        public DelegateCommand Check =>
           _Check ?? (_showDialog = new DelegateCommand(ExecuteCheck));

        public string StringCheck_For_First_Symbol_Of_Login = "12345678 90-=+.,:;'/?[]{}()><|\"";
        public string Check_For_All_Login = "-=+.,:;'/?[*!@#$№:;~`]{}( )><|ЙЕНГШЩЗХЪЁФЫВАПРОЛДЖЭЯЧСМИТЬБЮ.ЕНГШЩЗХЗГЪХЗЩШГНЕКУЦйцукенгшщзхъёфывапролджэячсмитьбю.";
        public string Check_For_Name_Surname = "1234567890-= _-= +.,:;'/?[*!@#$№:;~`]{}( )><";
        public string Check_ForAge = "-=~`~ йцукенщзхъфывапролдэячсмитьбю.qwertyuiop[]asdfghjkl;'zxcvbnm,.//.,,<></ | QWRTYUOP[[]ASDFGHJKL;'ZXCVBNM,./ЙЦУКЕНГШЩЗХЪ@#№^%ФЫВАПРОЛДЖЭЯЧСМИТЬБЮ.";
        public string Check_Adress = "@^;:<>?/,$#№!~`}{)(][|/";
        public int Flag_Check_For_All_Login = 0;
        public int Flag_StringCheck_For_First_Symbol_Of_Login = 0;
        public int Flag_Lenght_Login = 0;
        public int Flag_Length_Password = 0;
        public int Flag_Name_SurName = 0;
        public int Flag_Age = 0;
        public int PsevdoAge = 0;
        public int Flag_PsevdoAge = 0;
        public int Flag_Adress = 0;
        void ExecuteCheck()
        {
            Flag_Name_SurName = 0;
            Flag_Lenght_Login = 0;
            Flag_Check_For_All_Login = 0;
            Flag_StringCheck_For_First_Symbol_Of_Login = 0;
            Flag_Length_Password = 0;
            Flag_Name_SurName = 0;
            Flag_Age = 0;
            Flag_PsevdoAge = 0;
            Flag_Adress = 0;

            for (int i = 0; i < _login.Length; i++)
            {
                for (int j = 0; j < Check_For_All_Login.Length; j++)
                {
                    if (Check_For_All_Login[j] ==_login[i])
                    {
                        Flag_Check_For_All_Login = 1;
                     
                    }
                   
                }
            }
            for (int i = 0; i < StringCheck_For_First_Symbol_Of_Login.Length; i++)
            {
                if (_login[0] == StringCheck_For_First_Symbol_Of_Login[i])
                {
                    Flag_StringCheck_For_First_Symbol_Of_Login = 1;

                }
            }
            if(_login.Length < 8 || _login.Length > 12)
            {
                Flag_Lenght_Login = 1;
            }

            if (_password.Length < 6 || _password.Length > 12)
            {
                Flag_Length_Password  = 1;
            }

            for (int i = 0; i < _name.Length; i++)
            {
                for (int j = 0; j < Check_For_Name_Surname.Length; j++)
                {
                    if (Check_For_Name_Surname[j] == _name[i])
                    {
                        Flag_Name_SurName = 1;

                    }

                }
            }
            for (int i = 0; i < _surname.Length; i++)
            {
                for (int j = 0; j < Check_For_Name_Surname.Length; j++)
                {
                    if (Check_For_Name_Surname[j] == _surname[i])
                    {
                        Flag_Name_SurName = 1;

                    }
                }
            }


            for (int i = 0; i < _age.Length; i++)
            {
                for (int j = 0; j < Check_ForAge.Length; j++)
                {
                    if (Check_ForAge[j] == _age[i])
                    {
                        Flag_Age = 1;

                    }
                }
            }
            ///

            for (int i = 0; i < _adress.Length; i++)
            {
                for (int j = 0; j < Check_Adress.Length; j++)
                {
                    if (Check_Adress[j] == _adress[i])
                    {
                        Flag_Adress = 1;

                    }
                }
            }



            if(_adress.Length < 15 || _adress.Length > 20)
            {

                Flag_Adress = 1;
                Length_Adress_();
            }

            if (Flag_Adress == 1)
            {
                Length_Adress_();
            }


            if(_age == "")
            {
                Flag_Age = 1;
            }

            //если возраст введен корректно
            if (Flag_Age == 0)
            {
                PsevdoAge = Convert.ToInt32(_age);
                if(PsevdoAge < 16 || PsevdoAge > 100)
                {
                    Flag_PsevdoAge = 1;
                    ExecuteCorrectAge();

                }
            }


            //если возраст введен некорректно
            if (Flag_Age == 1)
            {
                ExecuteUnCorrectAge();
            }

            //если имя или фамилия Введенно некорректно
            if (Flag_Name_SurName == 1)
            {
                ExecuteInCorrectText();
            }

            //если длина пароля меньше 6 или больше 12
            if (Flag_Length_Password == 1)
            {
                Notification_Lenght_Password();
            }

            //если длина логина меньше 8 или больше 12
            if (Flag_Lenght_Login == 1)
            {
                Notification_Lenght();
            }
            //Если 1 символ логина не соответсвует условиям 
            if (Flag_StringCheck_For_First_Symbol_Of_Login == 1)
            {
                ExecuteShowDialog();
            }
            //Если в логине есть нессответсвующие символы
            if (Flag_Check_For_All_Login == 1)
            {
                ExecuteShowDialog();
            }
            //Переход на следующую страницу
            if (Flag_Check_For_All_Login == 0 && Flag_StringCheck_For_First_Symbol_Of_Login == 0 && Flag_Lenght_Login == 0 && Flag_Length_Password == 0 && Flag_Name_SurName == 0 
                && Flag_Age == 0 && Flag_PsevdoAge == 0 && Flag_Adress == 0)
            {
                _pageService.ChangePage(new PageRegistartionSuccsess());
            }
        }
        #endregion
    }
}
