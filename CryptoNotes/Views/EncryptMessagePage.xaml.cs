using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CryptoNotes.Models;
using CryptoNotes.ViewModels;
using PgpCore;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CryptoNotes.Views
{
  public partial class EncryptMessagePage : ContentPage
  {
    public EncryptMessagePage()
    {
      InitializeComponent();

      privatePicker.SetBinding(Picker.ItemsSourceProperty, "Item");
      privatePicker.ItemDisplayBinding = new Binding("Text");
      privatePicker.ItemsSource = App.Database.GetPrivateItemAsync().Result.Where(x => x.PasswordKey != null && x.EmailKey != null).ToList();

      publicPicker.SetBinding(Picker.ItemsSourceProperty, "Item");
      publicPicker.ItemDisplayBinding = new Binding("Text");
      publicPicker.ItemsSource = App.Database.GetItemsAsync().Result;


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
      Item publicKey = this.publicPicker.SelectedItem as Item;
      
      try
      {
        string publicFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "public.asc");
        string encryptedMessage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "encryptedAndSigned.pgp");
        string messageContent = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "content.txt");

        using (var streamWriter = new StreamWriter(publicFile, true))
          streamWriter.WriteLine(publicKey.PublicKey);

        using (var streamWriter = new StreamWriter(messageContent, true))
          streamWriter.WriteLine(MessageTxt.Text);


        using (PGP pgp = new PGP())
        {
          // Encrypt file and sign

          if (SignedF.IsToggled)
          {
            Item privateKey = privatePicker.SelectedItem as Item;
            if (string.IsNullOrEmpty(privateKey.EmailKey))
              privateKey.EmailKey = "";
            if (string.IsNullOrEmpty(privateKey.PasswordKey))
              privateKey.PasswordKey = "";
            string privateFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "private.asc");
            using (var streamWriter = new StreamWriter(privateFile, true))
              streamWriter.WriteLine(privateKey.PrivateKey);
            await pgp.EncryptFileAndSignAsync(messageContent, encryptedMessage, publicFile, privateFile, privateKey.PasswordKey, true, true);

            using (StreamWriter streamWriter = new StreamWriter(privateFile, true))
              streamWriter.WriteLine(DateTime.UtcNow);
          }
          else
            await pgp.EncryptFileAsync(messageContent, encryptedMessage, publicFile, true, true);


          /*
           * 
           * byte[] byteArray = Encoding.ASCII.GetBytes(privateKey.PrivateKey);
          Stream privateKeyStream = new MemoryStream(byteArray, true);


          byte[] publicByteArray = Encoding.ASCII.GetBytes(publicKey.PublicKey);
          Stream publicKeyStream = new MemoryStream(publicByteArray, true);

          byte[] inputByteArray = Encoding.ASCII.GetBytes(MessageTxt.Text);
          Stream inputFileStream = new MemoryStream(inputByteArray, true);

          System.IO.Stream outputFileStream = new MemoryStream();



          // Encrypt and sign stream
          await pgp.EncryptStreamAndSignAsync(inputFileStream, outputFileStream, publicKeyStream
                                            , privateKeyStream, privateKey.PasswordKey, true, true);

          // Encrypt and sign stream
          using (FileStream inputFileStream = new FileStream(@"C:\TEMP\Content\content.txt", FileMode.Open))
          using (Stream outputFileStream = File.Create(@"C:\TEMP\Content\encryptedAndSigned.pgp"))
          using (Stream publicKeyStream = new FileStream(@"C:\TEMP\Keys\public.asc", FileMode.Open))
          using (Stream privateKeyStream = new FileStream(@"C:\TEMP\Keys\private.asc", FileMode.Open))
            await pgp.EncryptStreamAndSignAsync(inputFileStream, outputFileStream, publicKeyStream, privateKeyStream, "password", true, true);
          */

        }

        await Share.RequestAsync(new ShareTextRequest
        {
          Text = File.ReadAllText(encryptedMessage),
          Title = "PGP Message",
          Subject = "PGP Message"
        });

        using (StreamWriter streamWriter = new StreamWriter(messageContent, true))
          streamWriter.WriteLine(DateTime.UtcNow);

        using (StreamWriter streamWriter = new StreamWriter(encryptedMessage, true))
          streamWriter.WriteLine(DateTime.UtcNow);

        using (StreamWriter streamWriter = new StreamWriter(publicFile, true))
          streamWriter.WriteLine(DateTime.UtcNow);


        //await Sms.ComposeAsync(message);
      }
      catch (FeatureNotSupportedException ex)
      {
        // Sms is not supported on this device.
      }
      catch (Exception ex)
      {
        // Other error has occurred. b
      }

      MessageTxt.Text = string.Empty;
      privatePicker.SelectedIndex = -1;
      publicPicker.SelectedIndex = -1;

    }

    



  }
}
