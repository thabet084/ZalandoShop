using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using ZalandoShop.Helpers;
using ZalandoShop.Models;

namespace ZalandoShop.ViewModel
{
    public class ArticleResultViewModel : ViewModelBase
    {
        ArticleSearch articleSearch;
        SvcHelper svcHelper;
        ResourceLoader resourceLoader;
        int page;
        private INavigationService _navigationService;


        public ArticleResultViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            svcHelper = new SvcHelper();

            Messenger.Default.Register<NotificationMessage>(this, async m =>
            {
                switch (m.Notification)
                {
                    case "LoadMore":
                        IsBusy = true;
                        await GetArticles();
                        IsBusy = false;
                        break;

                }
            });
            page = 1;
            Articles = new ObservableRangeCollection<Article>();
            resourceLoader = new ResourceLoader();
           
        }


        #region Properties
        public ArticleSearch ArticleSearch
        {
            get
            {
                return articleSearch;
            }
            set
            {
                articleSearch = value;
                if (articleSearch != null)
                {

                    GetArticles();

                }
            }

        }
        bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                _IsBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        public ObservableRangeCollection<Article> Articles { get; set; }
        #endregion


        #region Methods
        public async Task GetArticles()
        {
            IsBusy = true;

            List<Article> lstArticles = new List<Article>();

            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            var size = new Size(bounds.Width * scaleFactor, bounds.Height * scaleFactor);

            int itemsPerPage = Convert.ToInt32(Math.Ceiling(size.Width / 350));
            itemsPerPage = itemsPerPage * Convert.ToInt32(Math.Ceiling(size.Height / 350));

            lstArticles = await svcHelper.GetArticles(ArticleSearch.Keyword, ArticleSearch.Gender, page, itemsPerPage);

            Articles.AddRange(new ObservableRangeCollection<Article>(lstArticles));

            page++;

            IsBusy = false;

            if (Articles.Count == 0)
            {

                string Error_ArticlesNotFound = resourceLoader.GetString("Error_ArticlesNotFound");
                MessageDialog dialog = new MessageDialog(Error_ArticlesNotFound);
                await dialog.ShowAsync();
                _navigationService.NavigateTo("ArticleSearchPage");

            }
        }
        #endregion





    }
}

