﻿using System;
namespace CryptoNotes.Models
{
  public class Contact
  {
    public string Name { get; set; }
    public string Image { get; set; }
    public string[] Emails { get; set; }
    public string[] PhoneNumbers { get; set; }
  }
}
