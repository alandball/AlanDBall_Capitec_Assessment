# AlanDBall Capitec Assessment: Twitter Feed Simulator

Welcome to my Twitter Feed Simulator

Built and tested in Visual Studio 2017 Community Edition

To run:
1. Download or clone to desktop.
2. Open the solution in Visual Studio
3. Build the project. It should download all nuget packages

To test in debug mode:
-Hit Start or F5 to run.

To run the application exe as standalone
1. From the root directory, change to \AlanDBall_Capitec_Assessment\bin\debug
2. Double click TwitterFeed.exe
3. This will fail, as no args have been provided. A console window displays the missing args
4. Right click TwitterFeed.exe and select "Create Shortcut". For me there was no dialog, it created the shortcut immediately in the same directory
5. Right click the shortcut, and in the "Shortcut" tab, add to the target " user.txt tweet.txt", so it looks like this:
<omitted>\AlanDBall_Capitec_Assessment\AlanDBall_Capitec_Assessment\bin\Debug\TwitterFeed.exe user.txt tweet.txt

