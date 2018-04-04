# Input Controlled AVI Capture
This sample shows how to start and stop AVI capture on state change on the General 
Purpose Input (also known as "Trigger" or "GP In") of The Imaging Source cameras.

AVI Capture can be either on
- two input changes (trigger pulses), one for start and one for stop
- or last as long as the Input is to high.

The sample contains
- AVI Codec selection
- AVI Codec property save and restore
- automatic file name generation for video files.
- camera setup
- handling or the GP In property

After an AVI file was captured and closed, the next is prepared. This
reduces the delay between GP Input change and capture start.

The program is as it is. If you find errors, you may fix them let me know about 
that fix.

The program needs
- IC Imaging Control 3.4
- Visual Studio 2015 C++

The compiled program is avaialble a setup in "Input_Controlled_AVI_Capture_setup.exe"
The Inno Setup Script is "setup.iss".

The CIniEx class was Posted by Koray Gecici on February 8th, 2000 at 
https://www.codeguru.com/cpp/data/data-misc/inifiles/article.php/c4561/CIniEx--Dynamic-INI-Class.htm
