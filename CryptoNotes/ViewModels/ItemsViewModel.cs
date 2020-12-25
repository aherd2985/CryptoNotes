using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using CryptoNotes.Models;
using CryptoNotes.Views;

namespace CryptoNotes.ViewModels
{
  public class ItemsViewModel : BaseViewModel
  {
    public ObservableCollection<Item> Items { get; set; }
    public Command LoadItemsCommand { get; set; }

    public ItemsViewModel()
    {
      Title = "Private Keys";
      Items = new ObservableCollection<Item>();
      LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

      MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
      {
        Item newItem = item as Item;
        await App.Database.SaveItemAsync(newItem);
      });

      MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "RemoveItem", async (obj, item) =>
      {
        await App.Database.DeleteItemAsync(item);
      });
    }

    async Task ExecuteLoadItemsCommand()
    {
      IsBusy = true;

      try
      {
        Items.Clear();
        var items = await App.Database.GetItemsAsync();
        foreach (var item in items)
        {
          if(!string.IsNullOrEmpty(item.PrivateKey))
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