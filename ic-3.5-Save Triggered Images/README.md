# Save Images on Trigger Events
## Requisites
* IC Imaging Control 3.5
* C# or C++
* The Imaging Source camera 

## Description
These very simple examples demonstrate, how to save an image when the camera was triggered. They use the `FrameQueueSink`'s callback functions, in which the images saving takes place.

Please keep in mind, this can not be done with any speed and amount of images, because windows will flush its disc cache at some time. When the disc cache is flushed, no fames can be saved and maybe frames are lost. However, allocating enough buffers in the `FrameQueueSink` constructor can overcome this issue. 