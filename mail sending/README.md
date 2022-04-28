# Python script to send automatic email

The script takes a dataframe of the participants and send them an email according to their language where the personal info are replaced in the text.

## Files

```
.
├── mail_eng.txt      # message template in english
├── mail_fra.txt      # message template in french
├── mail_list.csv     # csv files with info about the participants (email, name, Id, language)
└── mail_sender.py    # python script
```

## Dependencies

- `smtplib` : default python library to send email with the smtp protocol
- `pandas` : python library to handle dataframes

## How to run

Add a file `secret.py`containing :

- `sender_email` : your email adress
- `password` : password of your adress
- `link_fra` : link for the questionnaire in french
- `link_eng` : link for the questionnaire in english



