# FolderSyncApp

FolderSyncApp is a program that allows you to synchronize two folders. It was built using the Composite Design Pattern.

## Usage

To run the program, you must first build it in release mode. If you're using Visual Studio, you can do this by selecting:
Build > ConfigurationManager > Configuration = Release

The command takes four arguments:
- the path to the source folder
- the path to the destination folder
- the interval (in seconds) between each synchronization
- the path to the log file

Once you have finished building the FolderSyncApp, you can locate the executable file at:
 
.\FolderSyncApp\bin\Release\net6.0

 To run the application, you can use the following command as an example:

FolderSyncApp.exe D:\Source C:\Destination 60 D:\log.txt

This command will initiate the FolderSyncApp and start synchronizing files from the D:\Source folder to the C:\Destination folder every 60 seconds. The log file will be saved at D:\log.txt.

## Block Diagram

![Block Diagram](https://github.com/bogse/FolderSyncApp/blob/main/Resources/BlockDiagram.png)

## Note

This project was developed using the Composite Design Pattern. Specifically, the pattern was used to create a hierarchical structure to represent source files and directories to be synced with a destination folder. By using the Composite Pattern, individual files and directories were treated uniformly, allowing for easier implementation of the syncing algorithm.
