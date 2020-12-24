using System;
using System.Threading.Tasks;
using CryptoNotes.Models;
using System.Collections.Generic;

namespace CryptoNotes.ViewModels
{
  public class DecryptMessageViewModel : BaseViewModel
  {
    public List<Item> PrivateItems { get; set; }

    public DecryptMessageViewModel()
    {
      Title = "Decryptor";
      PrivateItems = new List<Item>();
    }

    public List<Item> GetPrivateItems()
    {
      GetPrivateItemsAsync().ContinueWith(OnMyAsyncMethodFailed, TaskContinuationOptions.OnlyOnFaulted);
      return PrivateItems;
    }

    public async Task<List<Item>> GetPrivateItemsAsync()
    {
      PrivateItems.Clear();
      IEnumerable<Item> items = await Database.GetItemsAsync();
      foreach (Item item in items)
      {
        if (!string.IsNullOrEmpty(item.PrivateKey))
          await Database.SaveItemAsync(item);
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
