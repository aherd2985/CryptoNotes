using System;
using Xamarin.Forms;
using CryptoNotes.Models;
using CryptoNotes.ViewModels;

namespace CryptoNotes.Views
{
  public partial class ItemsPage : ContentPage
  {
    ItemsViewModel viewModel;

    public ItemsPage()
    {
      InitializeComponent();
    }

    async void OnItemSelected(object sender, EventArgs args)
    {
      var layout = (BindableObject)sender;
      var item = (Item)layout.BindingContext;
      await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
    }

    async void AddItem_Clicked(object sender, EventArgs e)
    {
      await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();

      BindingContext = viewModel = new ItemsViewModel();

      if (viewModel.Items.Count == 0)
        viewModel.IsBusy = true;
    }
  }
}