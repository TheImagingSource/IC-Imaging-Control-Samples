# Python samples for Windows NET

## Requirements
* IC Imaging Control 3.5
* The Imaging Source camera or video capture device
* Python 3.7
* pyqt5
* pythonnet http://pythonnet.github.io/

These samples show how to use IC Imaging Control 3.5.

The qt5-simple.py sample simply displays the live video in a QT5 widget. For doing so, a frame filter is inserted into the display path of IC Imaging Control. The filter converts the IFrame of IC Imaging control into a QImage and mirros it. 
Then a signal is sent to the main thread.

The qt5-imageprocessing.py sample enhances the qt5-simple.py with a frame callback and some simple image processing. The results are signaled to the main thread again.

In order to run the samples, the IC Imaging Control 3.5 component and its depenedendcies must be copied from

Documents\IC Imaging Control 3.5\redist\dotnet\x64\ for 64 bit

or

Documents\IC Imaging Control 3.5\redist\dotnet\x86\ for 32bit 


These are my first tries with Python NET and IC Imaging Control, but the principle looks promising.
