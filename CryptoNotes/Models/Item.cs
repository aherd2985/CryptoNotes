using SQLite;

namespace CryptoNotes.Models
{
  public class Item
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Text { get; set; }
    public string Description { get; set; }
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }
    public string SafeMessage { get; set; }
    public string EmailKey { get; set; }
    public string PasswordKey { get; set; }
  }
}