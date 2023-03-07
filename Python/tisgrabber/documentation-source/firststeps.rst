===========
First Steps
===========

Importing tisgrabber dll
------------------------

Python *ctypes* is used for importing DLLs into a Python script. For use of *tisgrabber* some basic lines of code must be done in any project:

.. code-block:: python 

   import ctypes 

   ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")

Many functions of the *tisgrabber.dll* have "complex" parameters and and return values. Therefore, these functions must be 
declared. This is done in the file *tisgrabber.py*. This *tisgrabber.py* must be imported:

.. code-block:: python 

   import tisgrabber as tis

The declaration of the functions is done by a call to 

.. code-block:: python 

   tis.declareFunctions(ic)

Thus, a Python script that uses the *tisgrabber.dll* needs always following lines at the start:

.. code-block:: python 

   import ctypes 
   import tisgrabber as tis

   ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
   tis.declareFunctions(ic)

   ic.IC_InitLibrary(0)

The next line, which is always necessary is the ``ic.IC_InitLibrary(0)`` call. 


All functions of the tisgrabber.dll are now in the "ic" namespace.

HGRABBER
--------
The class ``HGRABBER`` mirrors a structure of tisgrabber dll, which contains a pointer to the internal grabber object. The grabber object communicates with the camera. This object is passed as pointer to all functions of the DLL, so the functions know, which camera to communicate with.
It is defined in the *tisgrabber.py* as follows:

.. code-block:: python 

   class HGRABBER(ctypes.Structure):
      '''
      This class is used to handle the pointer to the internal 
      Grabber class, which contains the camera. 
      A pointer to this class is used by tisgrabber DLL.
      '''
      _fields_ = [('unused', ctypes.c_int)]

For each camera to be used in the Python script an own HGRABBER object must be created.


Strings
-------

The *tisgrabber dll* handles strings as simple *char** only. If a Python string is passed to any function, it must be encoded as utf-8. For example:

.. code-block:: python 

   hello = "Hello World"
   ic.IC_MsgBox( hello.encode("utf-8"),"Greeting".encode("utf-8"))

For convenience the *tisgrabber.py* contains the function "T" for encode and "D" for encode:

.. code-block:: python 

   hello = "Hello World"
   ic.IC_MsgBox( tis.t(hello),tis.T("Greeting"))

Simple Live Video
-----------------

Code : 01-Live.py

The first sample is the *01-Live.py*. It shows how to select a video capture device with the built in device selection dialog and how to start and stop the live stream.

.. code-block:: python

   hGrabber = ic.IC_ShowDeviceSelectionDialog(None)

The ``IC_ShowDeviceSelectionDialog()`` shows a device selection dialog. It returns a grabber object handle, therefore the C like name `hGrabber`

``IC_IsDevValid`` checks, whether the passed hGrabber handle uses a valid video capture device. If so, we can start and stop the live video stream:

.. code-block:: python

   if( ic.IC_IsDevValid(hGrabber)): 
      ic.IC_StartLive(hGrabber,1)
      ic.IC_MsgBox(tis.T("Click OK to stop"), tis.T("Simple Live Video"))

      ic.IC_StopLive(hGrabber)
   else:
      ic.IC_MsgBox(tis.T("No device opened"), tis.T("Simple Live Video"))


All programs should release the hGrabber handle in order to clean up memory. Thus, this is the last line of the sample:

.. code-block:: python

   ic.IC_ReleaseGrabber(hGrabber)

