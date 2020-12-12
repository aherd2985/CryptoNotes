using System;
using Xamarin.Forms;
using CryptoNotes.Models;
using CryptoNotes.ViewModels;

namespace CryptoNotes.Views
{
  public partial class PublicKeyChainPage : ContentPage
  {
    PublicKeyChainViewModel viewModel;

    public PublicKeyChainPage()
    {
      InitializeComponent();
    }

    async void OnItemSelected(object sender, EventArgs args)
    {
      var layout = (BindableObject)sender;
      var item = (Item)layout.BindingContext;
      await Navigation.PushAsync(new PublicKeyChainDetailPage(new PublicKeyChainDetailViewModel(item)));
    }

    async void AddItem_Clicked(object sender, EventArgs e)
    {
      await Navigation.PushModalAsync(new NavigationPage(new NewPublicKeyPage()));
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();

      BindingContext = viewModel = new PublicKeyChainViewModel();

      if (viewModel.Items.Count == 0)
        viewModel.IsBusy = true;
    }
  }
}
