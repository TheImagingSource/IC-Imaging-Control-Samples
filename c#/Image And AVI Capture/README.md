# Image and Video Capture

This C# programming example demonstrates how to capture a video file and images in parallel.

## Introduction
If a [MediastreamSink](https://www.theimagingsource.com/support/documentation/ic-imaging-control-net/MediaStreamSink.htm) is in use, it is not possible to have a callback for image saving in parallel.
Therefore, for image saving a [FrameFilter](https://www.theimagingsource.com/support/documentation/ic-imaging-control-net/FrameFilter.htm) is used.
This sample shows, how to do that.

## Requisites
* IC Imaging Control 3.5
