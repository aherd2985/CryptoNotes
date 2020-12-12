using System;
using System.Threading.Tasks;
using CryptoNotes.Models;
using System.Collections.Generic;

namespace CryptoNotes.ViewModels
{
  public class EncryptMessageViewModel : BaseViewModel
  {
    public List<Item> PublicItems { get; set; }
    public List<Item> PrivateItems { get; set; }
    public string MessageTxt { get; set; }

    public EncryptMessageViewModel()
    {
      Title = "Encryptor";
      PrivateItems = new List<Item>();
      PublicItems = new List<Item>();
      MessageTxt = "";
    }

    public List<Item> GetPublicItems()
    {
      GetPublicItemsAsync().ContinueWith(OnMyAsyncMethodFailed, TaskContinuationOptions.OnlyOnFaulted);
      return PublicItems;
    }

    public async Task<List<Item>> GetPublicItemsAsync()
    {
      PublicItems.Clear();
      IEnumerable<Item> items = await DataStore.GetItemsAsync(true);
      foreach (Item item in items)
      {
        if (string.IsNullOrEmpty(item.PrivateKey))
          PublicItems.Add(item);
      }
      return PublicItems;
    }

    public List<Item> GetPrivateItems()
    {
      GetPrivateItemsAsync().ContinueWith(OnMyAsyncMethodFailed, TaskContinuationOptions.OnlyOnFaulted);
      return PrivateItems;
    }

    public async Task<List<Item>> GetPrivateItemsAsync()
    {
      PrivateItems.Clear();
      IEnumerable<Item> items = await DataStore.GetItemsAsync(true);
      foreach (Item item in items)
      {
        if (!string.IsNullOrEmpty(item.PrivateKey))
          PrivateItems.Add(item);
      }
      return PrivateItems;
    }

    public static void OnMyAsyncMethodFailed(Task task)
    {
      Exception ex = task.Exception;
      // Deal with exceptions here however you want
    }
  }
}
