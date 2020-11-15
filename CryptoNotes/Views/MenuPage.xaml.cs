using CryptoNotes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoNotes.Views
{
  public partial class MenuPage : ContentPage
  {
    MainPage RootPage { get => Application.Current.MainPage as MainPage; }
    List<HomeMenuItem> menuItems;
    public MenuPage()
    {
      InitializeComponent();

      menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About" },
                //manage, generate, private keys name them
                new HomeMenuItem {Id = MenuItemType.Browse, Title="Private Keys" },
                //manage name sort by contact/keyname
                new HomeMenuItem {Id = MenuItemType.Browse, Title="Keychain" },
                //send public key
                new HomeMenuItem {Id = MenuItemType.About, Title="Invite" },
                //links to report crimes, help lines, etc
                new HomeMenuItem {Id = MenuItemType.About, Title="Report" },
                //dropdown pick private key, write message, hit encrypt, copy output
                new HomeMenuItem {Id = MenuItemType.About, Title="Encrypt" },
                //dropdown pick keychain, hit decrypt, display message
                new HomeMenuItem {Id = MenuItemType.About, Title="Decrpyt" }
            };

      ListViewMenu.ItemsSource = menuItems;

      ListViewMenu.SelectedItem = menuItems[0];
      ListViewMenu.ItemSelected += async (sender, e) =>
      {
        if (e.SelectedItem == null)
          return;

        var id = (int)((HomeMenuItem)e.SelectedItem).Id;
        await RootPage.NavigateFromMenu(id);
      };
    }
  }
}