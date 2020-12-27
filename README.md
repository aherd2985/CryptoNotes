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

The app functionality is:

- View a list of generated PGP keys.
- Create a new PGP key set.
- Save public keys.
- Send public keys through the share panel.
- Send an encrypted message through the share panel.
- Decrypt a PGP message.

In all cases the keys are stored in a local SQLite database.

It currently uses PGPCore for PGP functionality.


## TODO:
* Self destruct and delete all keys
* Button animations
* Needs more design
