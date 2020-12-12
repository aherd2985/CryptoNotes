using CryptoNotes.Models;
using CryptoNotes.Views;
using Xamarin.Forms;

namespace CryptoNotes.ViewModels
{
  public class PublicKeyChainDetailViewModel : BaseViewModel
  {
    public Item Item { get; set; }
    public PublicKeyChainDetailViewModel(Item item = null)
    {
      Title = item?.Text;
      Item = item;
    }
  }
}
