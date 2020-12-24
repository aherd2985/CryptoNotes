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

    public Task<Item> GetItemAsync(string id)
    {
      return Database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public Task<int> SaveItemAsync(Item item)
    {
      if (string.IsNullOrEmpty(item.Id))
      {
        return Database.UpdateAsync(item);
      }
      else
      {
        return Database.InsertAsync(item);
      }
    }

    public Task<int> DeleteItemAsync(Item item)
    {
      return Database.DeleteAsync(item);
    }
  }
}
