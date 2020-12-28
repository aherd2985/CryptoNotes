﻿namespace CryptoNotes.Models
{
  public enum MenuItemType
  {
    Browse,
    About,
    PublicKeys,
    EncryptMessage,
    DecryptMessage,
    Invite,
    SelfDestruct
  }
  public class HomeMenuItem
  {
    public MenuItemType Id { get; set; }

    public string Title { get; set; }
    public string Icon { get; set; }
  }
}
