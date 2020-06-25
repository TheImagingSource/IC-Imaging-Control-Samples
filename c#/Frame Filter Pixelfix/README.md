# Hot Pixel Correction
This sample shows how to implement a simple Hot Pixel Correction in a C# program. The basic idea is using a frame filter, that loads a file with hot pixel coordinates and then overwrites these hot pixels with the neighbor pixel values.
The smaple consists of two programs:

* Build Pixelfix

  This Program creates a file for a camera with the coordinates of the hot pixels

* FrameFilter Pixelfix
  
  This Program shows, how to use the Pixefix-Framefilter in an own program with the above created list of hot pixels.
