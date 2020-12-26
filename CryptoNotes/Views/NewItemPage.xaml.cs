using System;
using Xamarin.Forms;
using PgpCore;
using CryptoNotes.Models;
using System.IO;

namespace CryptoNotes.Views
{
  public partial class NewItemPage : ContentPage
  {
    public Item Item { get; set; }

    public NewItemPage()
    {
      InitializeComponent();

      Item = new Item
      {
        Text = "",
        Description = "",
        PasswordKey = "",
        EmailKey = ""
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

    async void GeneratePrivateKey(System.Object sender, System.EventArgs e)
    {
      createKeyBtn.FadeTo(0, 4000);
      using (PGP pgp = new PGP())
      {
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "public.asc");
        string fileName2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "private.asc");
        // Generate keys
        pgp.GenerateKey(fileName, fileName2, Item.EmailKey, Item.PasswordKey);

        Item.PrivateKey = File.ReadAllText(fileName2);
        Item.PublicKey = File.ReadAllText(fileName);

        using (var streamWriter = new StreamWriter(fileName, true))
          streamWriter.WriteLine(DateTime.UtcNow);

        using (var streamWriter = new StreamWriter(fileName2, true))
          streamWriter.WriteLine(DateTime.UtcNow);

        MessagingCenter.Send(this, "AddItem", Item);
        await Navigation.PopModalAsync();
      }
    }



  }
}