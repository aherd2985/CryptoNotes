using System;
using System.IO;
using CryptoNotes.ViewModels;
using PgpCore;
using Xamarin.Forms;
using CryptoNotes.Models;

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
      Item privateKey = this.privatePicker.SelectedItem as Item;

      //string publicFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "public.asc");
      string privateFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "private.asc");
      string encryptedMessage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "encrypted.pgp");
      string decryptedMessage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "decrypted.txt");

      using (StreamWriter streamWriter = new StreamWriter(privateFile, true))
        streamWriter.WriteLine(privateKey.PrivateKey);

      using (StreamWriter streamWriter = new StreamWriter(encryptedMessage, true))
        streamWriter.WriteLine(MessageTxt.Text);

      using (PGP pgp = new PGP())
      {
        // Decrypt file and verify
        //await pgp.DecryptFileAndVerifyAsync(encryptedMessage, decryptedMessage, publicFile, privateFile, viewModel.Item.PasswordKey);

        await pgp.DecryptFileAsync(encryptedMessage, decryptedMessage, privateFile, privateKey.PasswordKey);

      }

      string safeMessage = File.ReadAllText(decryptedMessage);

      using (StreamWriter streamWriter = new StreamWriter(decryptedMessage, true))
        streamWriter.WriteLine(DateTime.UtcNow);

      using (StreamWriter streamWriter = new StreamWriter(encryptedMessage, true))
        streamWriter.WriteLine(DateTime.UtcNow);

      using (StreamWriter streamWriter = new StreamWriter(privateFile, true))
        streamWriter.WriteLine(DateTime.UtcNow);

      await DisplayAlert("Alert", safeMessage, "OK");

    }
  }
}
