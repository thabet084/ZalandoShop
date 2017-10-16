using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ZalandoShop.Helpers;
using ZalandoShop.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ZalandoShop.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArticleResult : Page
    {
        public ArticleResult()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ZalandoShop.Models.ArticleSearch articleSearch = e.Parameter as ZalandoShop.Models.ArticleSearch;
            ((ArticleResultViewModel)this.DataContext).ArticleSearch = articleSearch;
            base.OnNavigatedTo(e);
        }

        private void ArticleResult_OnLoaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = lstArticles.GetFirstDescendantOfType<ScrollViewer>();
            var scrollbars = scrollViewer.GetDescendantsOfType<ScrollBar>().ToList();
            var verticalBar = scrollbars.FirstOrDefault(x => x.Orientation == Orientation.Vertical);
            if (verticalBar != null)
                verticalBar.Scroll += BarScroll;
        }
        /// <summary>
        /// Send messge to ViewModel to load more articles as you scrolled down 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void BarScroll(object sender, ScrollEventArgs e)
        {
            var bar = sender as ScrollBar;
            if (bar == null)
                return;
            if (e.NewValue >= bar.Maximum)
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("LoadMore"));
              
            }
        }
    }
}
