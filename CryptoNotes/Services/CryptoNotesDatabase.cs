using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoNotes.Models;
using SQLite;

namespace CryptoNotes.Services
{
  public class CryptoNotesDatabase
  {
    static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
    {
      return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
    });

    static SQLiteAsyncConnection Database => lazyInitializer.Value;
    static bool initialized = false;

    public CryptoNotesDatabase()
    {
      InitializeAsync().SafeFireAndForget(false);
    }

    async Task InitializeAsync()
    {
      if (!initialized)
      {
        if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Item).Name))
        {
          await Database.CreateTablesAsync(CreateFlags.None, typeof(Item)).ConfigureAwait(false);
        }
        initialized = true;
      }
    }

    public Task<List<Item>> GetItemsAsync()
    {
      return Database.Table<Item>().ToListAsync();
    }

    public Task<List<Item>> GetPrivateItemsNotSignedAsync()
    {
      // SQL queries are also possible
      return Database.QueryAsync<Item>("SELECT * FROM [Item] WHERE [PasswordKey] IS NULL OR [EmailKey] IS NULL;");
    }

    public Task<Item> GetItemAsync(int id)
    {
      return Database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public Task<List<Item>> GetPrivateItemAsync()
    {
      return Database.Table<Item>().Where(x => x.PrivateKey != null).ToListAsync();
    }

    public Task<int> SaveItemAsync(Item item)
    {
      if (item.Id != 0)
      {
        return Database.UpdateAsync(item);
      }
      else
      {
        return Database.InsertAsync(item);
      }
    }

    public Task<List<Item>> DeleteItemAsync(Item item)
    {
      //Item deletedItem = GetItemsAsync().Result.Where(x => x.Text == item.Text).FirstOrDefault();
      return Database.QueryAsync<Item>("DELETE FROM [Item] WHERE [Text] = '" + item.Text + "';");
      //return Database.DeleteAsync(deletedItem);
    }
  }
}
