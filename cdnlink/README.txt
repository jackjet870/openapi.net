Overview
========
CdnLink provides a link between your application and Car Delivery Network (CDN) by way of database tables, the CDN API and FTP.

CdnLink can be run in two modes, Send and Receive.

In Send mode, CdnLink extracts job data from the CdnLink �Send� tables and sends them to CDN through its API.

As jobs progress in the field, and vehicles are picked up and delivered, updates are sent from CDN to your FTP server.

Running CdnLink in �Receive� mode downloads these updates from your FTP server and inserts the job data into the CdnLink �Receive� tables, where it can be queried by you application.

As with the rest of the CDN OpenApi clients, CdnLink is released under the MIT licence.

Format
======
CdnLink exists as an executable file that can be called from you application

Prerequsites
============
- Windows XP/Server 2003
- .NET Framework 4
- SQL Server 2005 or later (earlier versions and other databases me work but are untested)

Getting the software
====================
Download the latest stable build from:
http://build.cardeliverynetwork.com:8080/guestAuth/repository/downloadAll/bt20/.lastPinned/artifacts.zip

Download the latest development build from:
http://build.cardeliverynetwork.com:8080/guestAuth/repository/downloadAll/bt20/.lastSuccessful/artifacts.zip

Installation
============
- Download the CdnLink from one of the links above.
- Extract the whole archive and place contents into the location you would like to run it from

Database setup
==============
Run create.sql from the �db� directory in the extracted archive.

Configuration
=============
All CdnLink configuration is contained in the file, CdnLink.exe.config

The following fields are user configurable:

CdnLink.Settings.CDNLINK_CONNECTIONSTRING - Connection string for access to CdnSend... and CdnReceive... tables
CDNLINK_API_URL - URL of the Car Delivery Network API
CDNLINK_API_KEY - Your API key
CDNLINK_FTP_HOST - The FTP host CdnLink should connect to for job updates.
CDNLINK_FTP_ROOT (optional) - The directory CdnLink should look in to find its updates
CDNLINK_FTP_USER - FTP Username
CDNLINK_FTP_PASS - FTP Password

Be sure to set the �value� field and leave the �name� field as it is above.  For the connection string, set the �connectionString� field.

CdnLink: Send
=============
The calling application should write data to the CdnSendLoads and CdnSendVehicles tables.. It should then write to the CdnSends table (Status=SendStatus.Queued (10)) and call CdnLink:

    >cdnlink.exe /send

CdnLink will connect to the db and read CdnSends, to get the first record at SendStatus.Queued (10) out of the table and then get the data from CdnSendLoads.

- If it gets a record it will then change the Status=SendStatus.Processing (20) and send it to the CDN API. 
- If the API gets a successful result it will change the status to Status=SendStatus.Sent (30)
- If the API gets a error it will update the status to Status=SendStatus.Error (40)

The exe will then check the CdnSends table and if there is another record in the table it will send it otherwise it will close with a success code, non success errors could be:
- Invalid database connect string
- Invalid CDN API URL or key

CdnLink: Receive
================
Calling application calls CdnLink in receive mode:

    >cdnlink.exe /receive

CdnLink will firstly connect to the FTP server then to the database to ensure both are available.

CdnLink will then get the first ftp file and:
- Place the full JSON message into the CdnReceivedFtpFiles
- Remove the file from the FTP directory 
- Add a record to the CdnReceives table Status=ReceiveStatus.Processing (50)

CdnLink will then process the JSON message placing the data into the CdnReceivedLoad, CdnReceivedVehicles, CdnReceivedDocuments, CdnReceivedDamage tables
- If the exe gets a successful result it will update CdnReceives to the Status=ReceiveStatus.Queued (60)
- Iif the exe gets a error it will update CdnReceives to the Status=ReceiveStatus.Error (70)

The exe will then check the FTP directory again and if there is another record in the table it will get it otherwise it will close with a success code.  Non success errors could be:
- Invalid database connect string
- Invalid database user
- Invalid FTP server
- Invalid FTP user
- Invalid directory

Database Tables
===============
Send to CDN
-----------
CdnSends - table that calling application writes to and the exe reads from to trigger outbound data and track the status and delivery of the data
CdnSendLoads - table containing outbound load data, calling application should tidy as appropriate
CdnSendVehicles - table of vehicles attached to the Load, a load can have 1 or more vehicles, calling application should tidy as appropriate

Get from CDN
------------
CdnReceives - table contains the status of FTP files from CDN
CdnReceivedFtpFiles - table contains original ftp file from CDN
CdnReceivedLoads - table containing load data extracted from JSON file
CdnReceivedVehicles - table of vehicles attached to the Load, a load can have 1 or more vehicles
CdnReceivedDocuments - table of documents attached to the Load, a load can have 1 or more documents
CdnReceivedDamage - table of damage codes attached to the Load, a load can have 1 or more damage items