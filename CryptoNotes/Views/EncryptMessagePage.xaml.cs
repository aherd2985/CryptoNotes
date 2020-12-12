using CryptoNotes.Models;
using CryptoNotes.ViewModels;
using Xamarin.Forms;

namespace CryptoNotes.Views
{
  public partial class EncryptMessagePage : ContentPage
  {
    EncryptMessageViewModel viewModel;
    public EncryptMessagePage()
    {
      InitializeComponent();

      viewModel = new EncryptMessageViewModel();
      viewModel.GetPublicItems();
      viewModel.GetPrivateItems();

      privatePicker.SetBinding(Picker.ItemsSourceProperty, "Item");
      privatePicker.ItemDisplayBinding = new Binding("Text");
      privatePicker.ItemsSource = viewModel.PrivateItems;

      publicPicker.SetBinding(Picker.ItemsSourceProperty, "Item");
      publicPicker.ItemDisplayBinding = new Binding("Text");
      publicPicker.ItemsSource = viewModel.PublicItems;


    }


    async void EncryptMessageClicked(System.Object sender, System.EventArgs e)
    {
      Item test = privatePicker.SelectedItem as Item;
      string derp = test.PrivateKey;
      /*
       try
      {
        string publicFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "public.asc");
        string privateFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "private.asc");
        string encryptedMessage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "encryptedAndSigned.pgp");
        string messageContent = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "content.txt");

        using (var streamWriter = new StreamWriter(publicFile, true))
          streamWriter.WriteLine(viewModel.Item.PublicKey);

        using (var streamWriter = new StreamWriter(privateFile, true))
          streamWriter.WriteLine(viewModel.Item.PrivateKey);

        using (var streamWriter = new StreamWriter(messageContent, true))
          streamWriter.WriteLine(viewModel.Item.SafeMessage);


        using (PGP pgp = new PGP())
        {
          // Encrypt file and sign
          await pgp.EncryptFileAndSignAsync(messageContent, encryptedMessage, publicFile, privateFile, viewModel.Item.PasswordKey, true, true);
        }



        // string encryptedMessage = 
        var message = new SmsMessage(File.ReadAllText(encryptedMessage), new[] { "19182600911" });

        using (var streamWriter = new StreamWriter(messageContent, true))
          streamWriter.WriteLine(DateTime.UtcNow);

        using (var streamWriter = new StreamWriter(encryptedMessage, true))
          streamWriter.WriteLine(DateTime.UtcNow);

        using (var streamWriter = new StreamWriter(publicFile, true))
          streamWriter.WriteLine(DateTime.UtcNow);

        using (var streamWriter = new StreamWriter(privateFile, true))
          streamWriter.WriteLine(DateTime.UtcNow);


        await Sms.ComposeAsync(message);
      }
      catch (FeatureNotSupportedException ex)
      {
        // Sms is not supported on this device.
      }
      catch (Exception ex)
      {
        // Other error has occurred.
      }
      await Navigation.PopAsync();
      */
    }




  }
}
