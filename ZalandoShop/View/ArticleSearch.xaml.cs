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
using ZalandoShop.Models;
using ZalandoShop.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ZalandoShop.View
{
    /// <summary>
    /// Article search page that can be used to build your search criteria 
    /// </summary>
    public sealed partial class ArticleSearch : Page
    {
        public ArticleSearch()
        {
           
            this.InitializeComponent();
        }

        private async void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                SvcHelper svcHelper = new SvcHelper();
                Gender currentGender;

                if (rBtnIsMenChecked.IsChecked.Value)
                    currentGender = Gender.Male;
                else
                    currentGender = Gender.Female;

                var articles= await svcHelper.GetArticles(sender.Text, currentGender, 1, 5);
                
                sender.ItemsSource = articles.Select(a=>a.Name);
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
              
                Models.ArticleSearch articleSearch = new Models.ArticleSearch();

                articleSearch.Keyword = sender.Text;
                if (rBtnIsMenChecked.IsChecked.Value)
                    articleSearch.Gender = Gender.Male;
                else
                    articleSearch.Gender = Gender.Female;
           
                ((ArticleSearchViewModel)this.DataContext)._navigationService.NavigateTo("ArticleResultPage", articleSearch);
            }
            
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            

        }
    }
}
