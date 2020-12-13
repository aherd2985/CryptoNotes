using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CryptoNotes.ViewModels
{
  public class AboutViewModel : BaseViewModel
  {
    public AboutViewModel()
    {
      Title = "About";
      OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://TechnoHerder.com"));
    }

    public ICommand OpenWebCommand { get; }
  }
}