# FolderSyncApp

FolderSyncApp is a program that synchronizes files between two directories and can be run from the command-line.
## Usage

1. Build the application in Visual Studio using the Release mode.

2. Locate the application at the following location after the build is complete:
FolderSyncApp\FolderSyncApp\bin\Release\net6.0

3. Start the application using command line arguments in the following format:
<program.exe> <sourcePath> <replicaPath> <syncIntervalInSeconds> <logFilePath>

    For example: FolderSyncApp.exe D:\Source C:\Destination 60 D:\log.txt


## Block Diagram

The following block diagram illustrates the architecture of the FolderSyncApp code:
![FolderSyncApp block diagram](https://github.com/bogse/FolderSyncApp/blob/main/Resources/BlockDiagram.png)

## Notes

This program utilizes the Composite Pattern, a design pattern that enables hierarchical structures of objects to be created. By using this pattern, the program can efficiently manage a file system with folders and files, allowing for the creation of a flexible and extensible system.

The Composite Pattern is useful for dealing with complex structures as it simplifies code, making it easier to understand, modify, and maintain. By utilizing this pattern, the program can easily manage a hierarchy of folders and files, allowing for a more organized and structured file system.
