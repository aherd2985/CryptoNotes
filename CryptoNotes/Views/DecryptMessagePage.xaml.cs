using System;
using System.IO;
using CryptoNotes.ViewModels;
using PgpCore;
using Xamarin.Forms;

using Xamarin.Forms;

namespace CryptoNotes.Views
{
  public partial class DecryptMessagePage : ContentPage
  {
    DecryptMessageViewModel viewModel;
    public DecryptMessagePage()
    {
      InitializeComponent();

      viewModel = new DecryptMessageViewModel();
      viewModel.GetPrivateItems();

      privatePicker.SetBinding(Picker.ItemsSourceProperty, "Item");
      privatePicker.ItemDisplayBinding = new Binding("Text");
      privatePicker.ItemsSource = viewModel.PrivateItems;
    }

    async void DecryptMessageClicked(System.Object sender, System.EventArgs e)
    {
      string publicFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "public.asc");
      string privateFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "private.asc");
      string encryptedMessage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "encrypted.pgp");
      string decryptedMessage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "decrypted.txt");

      using (PGP pgp = new PGP())
      {
        // Decrypt file and verify
        //await pgp.DecryptFileAndVerifyAsync(encryptedMessage, decryptedMessage, publicFile, privateFile, viewModel.Item.PasswordKey);
      }
    }
  }
}
