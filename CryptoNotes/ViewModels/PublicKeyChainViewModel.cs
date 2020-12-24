using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using CryptoNotes.Models;
using CryptoNotes.Views;

namespace CryptoNotes.ViewModels
{
  public class PublicKeyChainViewModel : BaseViewModel
  {
    public ObservableCollection<Item> Items { get; set; }
    public Command LoadItemsCommand { get; set; }

    public PublicKeyChainViewModel()
    {
      Title = "Public Keys";
      Items = new ObservableCollection<Item>();
      LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

      MessagingCenter.Subscribe<NewPublicKeyPage, Item>(this, "AddItem", async (obj, item) =>
      {
        var newItem = item as Item;
        Items.Add(newItem);
        await Database.SaveItemAsync(newItem);
      });

      MessagingCenter.Subscribe<PublicKeyChainDetailPage, Item>(this, "RemoveItem", async (obj, item) =>
      {
        await Database.DeleteItemAsync(item);
      });
    }

    async Task ExecuteLoadItemsCommand()
    {
      IsBusy = true;

      try
      {
        Items.Clear();
        var items = await Database.GetItemsAsync();
        foreach (var item in items)
        {
          if (string.IsNullOrEmpty(item.PrivateKey))
            Items.Add(item);
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
      }
      finally
      {
        IsBusy = false;
      }
    }
  }
}
