using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CryptoNotes.Services;
using CryptoNotes.Views;

namespace CryptoNotes
{
  public partial class App : Application
  {
    static CryptoNotesDatabase database;

    public App()
    {
      InitializeComponent();

      MainPage = new MainPage();
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }

    public static CryptoNotesDatabase Database
    {
      get
      {
        if (database == null)
        {
          database = new CryptoNotesDatabase();
        }
        return database;
      }
    }


  }
}
