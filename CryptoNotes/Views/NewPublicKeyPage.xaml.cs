using System;
using Xamarin.Forms;
using CryptoNotes.Models;

namespace CryptoNotes.Views
{
  public partial class NewPublicKeyPage : ContentPage
  {
    public Item Item { get; set; }

    public NewPublicKeyPage()
    {
      InitializeComponent();

      Item = new Item
      {
        Text = "Item name",
        Description = "This is an item description."
      };

      BindingContext = this;
    }

    async void Save_Clicked(object sender, EventArgs e)
    {
      MessagingCenter.Send(this, "AddItem", Item);
      await Navigation.PopModalAsync();
    }

    async void Cancel_Clicked(object sender, EventArgs e)
    {
      await Navigation.PopModalAsync();
    }

  }
}