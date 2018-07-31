# AlanDBall Capitec Assessment: Twitter Feed Simulator

Welcome to my Twitter Feed Simulator

Built and tested in Visual Studio 2017 Community Edition

To run:
1. Download or clone to desktop.
2. Extract the folder AlanDBall_Capitec_Assessment-master to a folder of your choice
3. Open the folder AlanDBall_Capitec_Assessment-master
4. Open the solution in Visual Studio
5. Build the project. It should restore all nuget packages

To test in debug mode:
1. Hit Start or F5 to run.
2. Console window will display an error about missing the argument.
3. In the solution explorer right click the project AlanDBall_Capitec_Assessment(not the solution), and open "Properties".
4. In the left pane select the "Debug" tab
5. In the command line arguments add "user.txt tweet.txt", without the quotes
6. Hit Start or F5 to run.
7. A console window displays that the files user.txt and tweet.txt don't exist.
8. Please copy user.txt and tweet.txt to "\AlanDBall_Capitec_Assessment\bin\debug" as this is where the exe is created. These files need to be in the same directory as the exe.
9. Once these files have been supplied, hit Start or F5 to run.

To run the application exe as standalone
1. From the root directory, change to "\AlanDBall_Capitec_Assessment\bin\debug"
2. Double click TwitterFeed.exe
3. This will fail, as no args have been provided. A console window displays the missing args
4. Right click TwitterFeed.exe and select "Create Shortcut". For me there was no dialog, it created the shortcut immediately in the same directory
5. Right click the shortcut, and in the "Shortcut" tab, add to the target " user.txt tweet.txt", so it looks like this:
"<omitted>\AlanDBall_Capitec_Assessment\AlanDBall_Capitec_Assessment\bin\Debug\TwitterFeed.exe user.txt tweet.txt"
