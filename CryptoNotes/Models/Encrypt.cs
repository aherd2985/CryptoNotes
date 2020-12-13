using System;
namespace CryptoNotes.Models
{
  public class Encrypt
  {
    public Item? privateKey { get; set; }
    public Item? publicKey { get; set; }
    public string Message2Encrypt { get; set; }
    public string SMSNbr { get; set; }
    //public bool? privatePicker { get; set; }
    public bool? publicPicker { get; set; }
  }
}
