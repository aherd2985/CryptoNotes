using System;
using System.IO;
using CryptoNotes.ViewModels;
using PgpCore;
using Xamarin.Forms;
using CryptoNotes.Models;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace CryptoNotes.Views
{
  public partial class InvitePage : ContentPage
  {
    public InvitePage()
    {
      InitializeComponent();

      publicPicker.SetBinding(Picker.ItemsSourceProperty, "Item");
      publicPicker.ItemDisplayBinding = new Binding("Text");
      publicPicker.ItemsSource = App.Database.GetPrivateItemAsync().Result;
    }

    async void InviteMessageClicked(System.Object sender, System.EventArgs e)
    {
      try
      {
        Item publicKey = this.publicPicker.SelectedItem as Item;

        await Share.RequestAsync(new ShareTextRequest
        {
          Text = publicKey.PublicKey,
          Title = "Public Key",
          Subject = "Public Key"
        });

        /*
        var contact = await Contacts.PickContactAsync();

        if (contact == null)
          return;

        var id = contact.Id;
        var namePrefix = contact.NamePrefix;
        var givenName = contact.GivenName;
        var middleName = contact.MiddleName;
        var familyName = contact.FamilyName;
        var nameSuffix = contact.NameSuffix;
        var displayName = contact.DisplayName;
        var phones = contact.Phones; // List of phone numbers
        var emails = contact.Emails; // List of email addresses
        */
      }
      catch (Exception ex)
      {
        // Handle exception here.
      }
    }

  }
}
