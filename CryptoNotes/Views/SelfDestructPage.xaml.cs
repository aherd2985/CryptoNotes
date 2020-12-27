using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CryptoNotes.Views
{
  public partial class SelfDestructPage : ContentPage
  {
    public SelfDestructPage()
    {
      InitializeComponent();
    }

    void SelfDestructClicked(System.Object sender, System.EventArgs e)
    {
      DeleteBtn.Opacity = 0;
      App.Database.DeleteAllItemsAsync();
      DeleteBtn.FadeTo(1, 400);
    }
  }
}
