using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoNotes.Models;

namespace CryptoNotes.Services
{
  public class MockDataStore : IDataStore<Item>
  {
    readonly List<Item> items;

    public MockDataStore()
    {
      items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Private", Description="This is a private key." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Public", Description="This is a public key." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third", Description="Whatever" }
            };
    }

    public async Task<bool> AddItemAsync(Item item)
    {
      IEnumerable<Item> test = items.Where(x => x.Text.Equals(item.Text));
      if(test.Count() == 0)
        items.Add(item);

      return await Task.FromResult(true);
    }

    public async Task<bool> UpdateItemAsync(Item item)
    {
      IEnumerable<Item> oldItem = items.Where((Item arg) => arg.Id.Equals(item.Id));
      if (oldItem.Count() == 0)
        items.Remove(oldItem.First());
      items.Add(item);

      return await Task.FromResult(true);
    }

    public async Task<bool> DeleteItemAsync(string id)
    {
      IEnumerable<Item> oldItem = items.Where((Item arg) => arg.Id.Equals(id));
      if (oldItem.Count() == 1)
        items.Remove(oldItem.First());
      return await Task.FromResult(true);
    }

    public async Task<Item> GetItemAsync(string id)
    {
      return await Task.FromResult(items.FirstOrDefault(s => s.Id.Equals(id)));
    }

    public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
    {
      return await Task.FromResult(items);
    }
  }
}