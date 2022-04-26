# -*- coding: utf-8 -*-

# Imports
import smtplib
import pandas as pd

# Secret
import secret as sec

TEST = True

# opening files
with open("mail_fra.txt") as mailfra:
    message_fra = mailfra.read()

with open("mail_eng.txt") as maileng:
    message_eng = maileng.read()

mailDf = pd.read_csv('mail_list.csv')

# SMTP stuff
# creates SMTP session
s = smtplib.SMTP('smtp.gmail.com', 587)

# start TLS for security
s.starttls()

# Authentication
s.login(sec.sender_email, sec.password)


UNLOKED = False
for idx, row in mailDf.iterrows():
    if row['Language'] == "eng":
        message = message_eng
    else:
        message = message_fra

    message = message.replace("{{nom}}", str(
        row['Name'])).replace("{{id}}", str(row['Id'])).replace("{{link_fra}}",sec.link_fra).replace("{{link_eng}}", sec.link_eng).replace("{{sender_mail}}",sec.sender_email)

    print(row['email'])

    if TEST:
        print(message)
    else:
        if not UNLOKED:
            print('''Sending Mail ? type "I'm sure" : ''')
            unlockString = input()
            if unlockString == "I'm sure":
                UNLOKED = True
            else:
                quit()

        # sending the mail
        s.sendmail(sec.sender_email, row['email'], message.encode("utf8"))


# # terminating the session
s.quit()
