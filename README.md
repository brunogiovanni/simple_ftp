# SimpleFTP App

Just a simple console app to send a file through a SFTP connection.

Create a ftp.env file on executable directory and specify the following environment variables:

- HOST => your SFTP IP
- PORT => your SFTP port connection (default 22)
- USERNAME => your SFTP username
- PASSWORD => your SFTP password
- LOCAL_PATH => yours computer local path (remember to add an slash "/" on the end. eg: "/home/users/User/Documents/")
- SERVER_PATH => your server path (eg: /home/user/public_html/test/)
- FILENAME => the file you wish to synchronize (eg: my_text_file.txt)
