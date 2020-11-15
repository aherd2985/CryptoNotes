using Xamarin.Forms;
using CryptoNotes.Models;
using CryptoNotes.ViewModels;

namespace CryptoNotes.Views
{
  public partial class ItemDetailPage : ContentPage
  {
    ItemDetailViewModel viewModel;

    public ItemDetailPage(ItemDetailViewModel viewModel)
    {
      InitializeComponent();

      BindingContext = this.viewModel = viewModel;
    }

    public ItemDetailPage()
    {
      InitializeComponent();

      var item = new Item
      {
        Text = "Item 1",
        Description = "This is an item description.",
        CusCode = "Poop"
      };

      viewModel = new ItemDetailViewModel(item);
      BindingContext = viewModel;
    }
  }
}