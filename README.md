---
name: Xamarin.Forms - PGP Application
description: 'This sample demonstrates an application for storing and using PGP keys. A side menu drives pages for generating keys, saving public keys, sending public keys, encrypting, and decrypting'
page_type: sample
languages:
- csharp
products:
- xamarin
extensions:
    tags:
    - getstarted
urlFragment: pgp
---
# CryptoNotes
Phone Application providing end-to-end encryption using PGP

## Crypto Tip Jar
<a rel="noreferrer" target="_blank" href="https://commerce.coinbase.com/checkout/e9376310-16a6-4195-acad-69b3da9cd7f4">Donate with Crypto</a>

## Functionality
The app functionality is:

- View a list of generated PGP keys.
- Create a new PGP key set.
- Save public keys.
- Send public keys through the share panel.
- Send an encrypted message through the share panel.
- Decrypt a PGP message.
- Self destruct page to delete all keys.

In all cases the keys are stored in a local SQLite database.

It currently uses PGPCore for PGP functionality.


## TODO:
* Button animations
* Needs more design
