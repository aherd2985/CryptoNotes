using Xamarin.Forms;
using CryptoNotes.Models;
using CryptoNotes.ViewModels;
using System;
using System.IO;
using PgpCore;
using Xamarin.Essentials;

namespace CryptoNotes.Views
{
  public partial class PublicKeyChainDetailPage : ContentPage
  {
    PublicKeyChainDetailViewModel viewModel;

    public PublicKeyChainDetailPage(PublicKeyChainDetailViewModel viewModel)
    {
      InitializeComponent();

      BindingContext = this.viewModel = viewModel;
    }

    public PublicKeyChainDetailPage()
    {
      InitializeComponent();

      var item = new Item
      {
        Text = "",
        Description = ""
      };

      viewModel = new PublicKeyChainDetailViewModel(item);
      BindingContext = viewModel;
    }

    async void Delete_Clicked(object sender, EventArgs e)
    {
      MessagingCenter.Send(this, "RemoveItem", viewModel.Item);
      await Navigation.PopAsync();
    }

  }
}