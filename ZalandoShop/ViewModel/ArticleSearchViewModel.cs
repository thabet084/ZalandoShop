using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;
using Windows.Networking.Connectivity;
using ZalandoShop.Models;

namespace ZalandoShop.ViewModel
{
    public class ArticleSearchViewModel : ViewModelBase
    {
        public INavigationService _navigationService;
        private ResourceLoader resourceLoader;

        public ArticleSearchViewModel(INavigationService navigationService)
        {  
            SearchCommand = new RelayCommand(SearchCommandAction);
            _navigationService = navigationService;
            IsMenChecked = true;
            IsErrorShowen = false;
             resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
        }

        #region Properties

        string keyword;
        public string Keyword
        {
            get { return keyword; }
            set
            {
                keyword = value;
                RaisePropertyChanged("Keyword");
            }
        }

        public bool IsMenChecked { get; set; }
        public bool IsWomenChecked { get; set; }

        bool _IsErrorShowen;
        public bool IsErrorShowen
        {
            get { return _IsErrorShowen; }
            set
            {
                _IsErrorShowen = value;
                RaisePropertyChanged("IsErrorShowen");
            }
        }

        string _ErrorMessage;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                _ErrorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }
        #endregion


        #region Commands
        public RelayCommand SearchCommand
        {
            get; set;
        }
        #endregion


        #region Methods
        private void SearchCommandAction()
        {
            if (!ContainErrors())
            {
                ArticleSearch articleSearch = new ArticleSearch();

                articleSearch.Keyword = keyword;
                if (IsMenChecked)
                    articleSearch.Gender = Gender.Male;
                else
                    articleSearch.Gender = Gender.Female;

                //Messenger.Default.Send(articleSearch);
                _navigationService.NavigateTo("ArticleResultPage", articleSearch);
            }


        }
        public bool ContainErrors()
        {
            IsErrorShowen = false;

            if (!IsInternetConnected())
            {
                IsErrorShowen = true;

                ErrorMessage = resourceLoader.GetString("Error_InternetDisConnect");
            }

            if (string.IsNullOrEmpty(keyword))
            {
                IsErrorShowen = true;
                ErrorMessage = resourceLoader.GetString("Error_KeywordBlank");
            }

            return IsErrorShowen;

        }
        public static bool IsInternetConnected()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = (connections != null) &&
                (connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
            return internet;
        }
        #endregion



    }
}
