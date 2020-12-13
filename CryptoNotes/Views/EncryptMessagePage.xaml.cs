using System;
using System.IO;
using System.Text;
using CryptoNotes.Models;
using CryptoNotes.ViewModels;
using PgpCore;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CryptoNotes.Views
{
  public partial class EncryptMessagePage : ContentPage
  {
    EncryptMessageViewModel viewModel;
    public Encrypt EncryptData { get; set; }

    public EncryptMessagePage()
    {
      InitializeComponent();

      viewModel = new EncryptMessageViewModel();
      viewModel.GetPublicItems();
      viewModel.GetPrivateItems();

      //MessageTxt = "test";
      //PhoneTxt

      privatePicker.SetBinding(Picker.ItemsSourceProperty, "Item");
      privatePicker.ItemDisplayBinding = new Binding("Text");
      privatePicker.ItemsSource = viewModel.PrivateItems;

      publicPicker.SetBinding(Picker.ItemsSourceProperty, "Item");
      publicPicker.ItemDisplayBinding = new Binding("Text");
      publicPicker.ItemsSource = viewModel.PublicItems;

      int i = 1;
    }

    void OnToggled(object sender, ToggledEventArgs e)
    {
      // Perform an action after examining e.Value
      if (e.Value)
        privatePicker.IsVisible = true;
      else
        privatePicker.IsVisible = false;
    }


    async void EncryptMessageClicked(System.Object sender, System.EventArgs e)
    {
      //if(SignedF.IsToggled)
      Item privateKey = privatePicker.SelectedItem as Item;
      Item publicKey = this.privatePicker.SelectedItem as Item;
      
      int i = 1;
      bool dup = SignedF.IsToggled;

      //string herp = MessageTxt.
      
       try
      {
        using (PGP pgp = new PGP())
        {
          byte[] byteArray = Encoding.ASCII.GetBytes(privateKey.PrivateKey);
          Stream privateKeyStream = new MemoryStream(byteArray);

          byte[] publicByteArray = Encoding.ASCII.GetBytes(publicKey.PublicKey);
          Stream publicKeyStream = new MemoryStream(publicByteArray);

          byte[] inputByteArray = Encoding.ASCII.GetBytes(MessageTxt.Text);
          Stream inputFileStream = new MemoryStream(inputByteArray);

          System.IO.Stream outputFileStream = new MemoryStream();



          // Encrypt and sign stream
          await pgp.EncryptStreamAndSignAsync(inputFileStream, outputFileStream, publicKeyStream
                                            , privateKeyStream, privateKey.PasswordKey, true, true);



          StreamReader reader = new StreamReader(outputFileStream);
         string encryptedMessage = await reader.ReadToEndAsync();

          privateKeyStream.Dispose();
          publicKeyStream.Dispose();
          inputFileStream.Dispose();
          outputFileStream.Dispose();
          reader.Dispose();

          // string encryptedMessage = 
          var message = new SmsMessage(encryptedMessage, new[] { PhoneTxt.Text });

          await Sms.ComposeAsync(message);

        }

      }
      catch (FeatureNotSupportedException ex)
      {
        // Sms is not supported on this device.
      }
      catch (Exception ex)
      {
        // Other error has occurred.
      }

      MessageTxt.Text = string.Empty;
      PhoneTxt.Text = string.Empty;
      privatePicker.SelectedIndex = -1;
      publicPicker.SelectedIndex = -1;

    }




  }
}
