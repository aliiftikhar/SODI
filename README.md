# SODI
SODI (StackOverflowDataImport) is a simple Windows Executable program (.NET Console .exe) that imports StackOverflow data dump (.xml) into your choice of Database. At the moment it supports MS SQL Server, but it is designed for extension. Please feel free to add more Database support (e.g. MySql, Redis etc) all you will need to do is to implement IDatabaseRepository and configure DatabaseRepositoryStrategy to return your newly added Repository when requested.

# How to use SODI:

Step 1: Get StackOverflow database files:
-----------------------------------------

Download StackOverflow database files using this torrent link: https://archive.org/download/stackexchange/stackexchange_archive.torrent

You may use BitTorrent: http://www.bittorrent.com/ to download files from the above torrent link.

Unzip the files so you end up with: Posts.xml, Comments.xml, Votes.xml, Badges.xml, Users.xml and Tags.xml

Step 2: Download SODI from Github
---------------------------------

Step 3: Configuring SODI
------------------------

In the SODI.Console project set the following appSettings in App.config

XmlSourceFolderPathIncludingLastSlash = [Folder location of your downloaded xml files]

DatabaseType = SqlServer

DatabaseServerName = [Your Database Server name or IP address]

DatabaseName = [Name of the new Database that will be created as StackOverflow database]

DatabaseUsername = [Your database server username]

DatabasePassword = [Your database server password]

Step 5: Run the Executable
--------------------------

Build the solution and Run SODI.Console.

Expected Result:

The console app will create the new Database by the name you specified in appSettings’ DatabaseName

A table for each xml file will be created with correct columns and data types

Each table will have a Primary Key: an ID column.

Depending on the size of XML files, this can be a very quick process (in case of StackExchange.Programmers xml files) or can be a long process (in case of StackOverflow xml files)

Console will keep updating as soon as each xml file is processed. The files will get processed in this order: Tags, Users, Badges, Votes, Comments and Posts

# Contributing to SODI: Adding more Database Server support

SODI is designed to be extensible .To add support for other Database Servers, e.g. MySQL you need to provide an implementation of SODI.Contracts.Repositories.IDatabaseRepository

The existing SQL Server implementation is located at SODI.Repositories.DatabaseRepositories.SqlServer

If the new implementation is for MySQL, I would expect that to be located at: SODI.Repositories.DatabaseRepositories.MySql

A new entry for MySQL will be added in SODI.Models.DatabaseType.cs which is an enum of database types.

The method GetSupportedDatabaseTypeRepositories() in SODI.Repositories.DatabaseRepositories.DatabaseRepositoryStrategy.cs will have to be modified to add newly created MySqlRepository into the dictionary of databaseTypeRepositories

Finally in the appSettings of App.Config the DatabaseType will have to match the name specified in the enum: SODI.Models.DatabaseType.cs
