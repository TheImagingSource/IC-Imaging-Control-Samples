% Use the following code to see the image acquisition objects 
% and to access the source properties for 64 bit or 32 bit plugins
% from The Imaging Source (TIS)
% This was written and tested in Windows 10
% The full download for the plugin is available from TIS here
% https://www.theimagingsource.com/support/downloads-for-windows/extensions/icmatlabr2013b/
% This code below has now been tested in Matlab R2019a - not 2013a as 
% stated on the TIS download site

% Be sure to install the correct driver for your camera first, then connect
% the camera and run the following code. Semicolons are left off to display
% the information obtained by the function calls

clc

% adaptor = 'tisimaq_r2013_32'; % this is for the 23 bit plugin

adaptor = 'tisimaq_r2013_64'; % this is for the 64 bit plugin

obj = imaqhwinfo(adaptor)

% to show the connected device
obj.DeviceInfo
% to show the available formats
obj.DeviceInfo.SupportedFormats

% choose the first available format from the device 1
vid = videoinput(adaptor, 1, obj.DeviceInfo.SupportedFormats{1})
src = getselectedsource(vid)

% get the available frame rates
available_Frame_rates = set(src, 'FrameRate')

% set the first available frame rate
src.FrameRate = available_Frame_rates{1}

vid.FramesPerTrigger = Inf;

% this will show the live image
preview(vid);