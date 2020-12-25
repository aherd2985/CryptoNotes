using Xamarin.Forms;
using CryptoNotes.Models;
using CryptoNotes.ViewModels;
using System;
using System.IO;
using PgpCore;
using Xamarin.Essentials;

namespace CryptoNotes.Views
{
  public partial class ItemDetailPage : ContentPage
  {
    ItemDetailViewModel viewModel;

    public ItemDetailPage(ItemDetailViewModel viewModel)
    {
      InitializeComponent();

      BindingContext = this.viewModel = viewModel;
    }

    public ItemDetailPage()
    {
      InitializeComponent();

      Item item = new Item
      {
        Text = "Item 1",
        Description = "This is an item description.",
        Id = 0
      };

      viewModel = new ItemDetailViewModel(item);
      BindingContext = viewModel;
    }

    async void Delete_Clicked(object sender, EventArgs e)
    {
      MessagingCenter.Send(this, "RemoveItem", viewModel.Item);
      await Navigation.PopAsync();
    }

    async void GeneratePrivateKey(System.Object sender, System.EventArgs e)
    {
      using (PGP pgp = new PGP())
      {
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "public.asc");
        string fileName2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "private.asc");
        // Generate keys
        pgp.GenerateKey(fileName, fileName2, viewModel.Item.EmailKey, viewModel.Item.PasswordKey);

        viewModel.Item.PublicKey = File.ReadAllText(fileName);
        viewModel.Item.PrivateKey = File.ReadAllText(fileName2);

        using (var streamWriter = new StreamWriter(fileName, true))
          streamWriter.WriteLine(DateTime.UtcNow);

        using (var streamWriter = new StreamWriter(fileName2, true))
          streamWriter.WriteLine(DateTime.UtcNow);

        await Navigation.PopAsync();
      }
    }

    

  }
}