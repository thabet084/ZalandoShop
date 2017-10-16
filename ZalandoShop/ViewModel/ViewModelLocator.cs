using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZalandoShop.View;

namespace ZalandoShop.ViewModel
{
    public class ViewModelLocator
    {/// <summary>
     /// Initializes a new instance of the ViewModelLocator class.
     /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new NavigationService();
            nav.Configure(ArticleSearchPageKey, typeof(ArticleSearch));
            nav.Configure(ArticleResultPageKey, typeof(ArticleResult));

            //Register your services used here
            SimpleIoc.Default.Register<INavigationService>(() => nav);
            SimpleIoc.Default.Register<ArticleSearchViewModel>();
            SimpleIoc.Default.Register<ArticleResultViewModel>();
        }

        public ArticleSearchViewModel ArticleSearchVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ArticleSearchViewModel>();
            }
        }

        public ArticleResultViewModel ArticleResultVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ArticleResultViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        public const string ArticleSearchPageKey = "ArticleSearchPage";
        public const string ArticleResultPageKey = "ArticleResultPage";
    }
}
