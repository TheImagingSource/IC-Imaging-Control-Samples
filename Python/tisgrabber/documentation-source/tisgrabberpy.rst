
==================
tisgrabber.py file
==================

This Python script contains helper functions for the tisgrabber function declarations and something more.

- declareFunctions( ic )
  
  This function sets the ``argtypes`` of some functions. ``ic`` is the DLL object returned by ``loadLibrary()``

- T(instr)        
  This is a convenience function for ``.encoding("utf-8")``.

- openDevice(ic)

  This function implements the above mentioned open a device by state file. It returns a hGrabber handle

More functions can be added in future.

Constants
---------

The file ``tisgrabber.py`` contains also constants used by the tisgrabber.dll

.. code-block:: python

    class SinkFormats(Enum):
    Y800 = 0
    RGB24 = 1 
    RGB32 = 2
    UYVY = 3  
    Y16 = 4

    ImageFileTypes = {'BMP':0, 'JPEG':1}

    IC_SUCCESS = 1
    IC_ERROR = 0
    IC_NO_HANDLE = -1
    IC_NO_DEVICE = -2
    IC_NOT_AVAILABLE = -3
    IC_NO_PROPERTYSET = -3
    IC_DEFAULT_WINDOW_SIZE_SET = -3
    IC_NOT_IN_LIVEMODE = -3
    IC_PROPERTY_ITEM_NOT_AVAILABLE = -4
    IC_PROPERTY_ELEMENT_NOT_AVAILABLE = -5
    IC_PROPERTY_ELEMENT_WRONG_INTERFACE = -6
    IC_INDEX_OUT_OF_RANGE = -7
    IC_WRONG_XML_FORMAT = -1
    IC_WRONG_INCOMPATIBLE_XML = -3
    IC_NOT_ALL_PROPERTIES_RESTORED = -4
    IC_DEVICE_NOT_FOUND = -5
    IC_FILE_NOT_FOUND = 35

Usage in own Scripts
--------------------

A Python script, which uses the ``tisgrabber.py`` file should start as follows:

.. code-block:: python

    import ctypes 
    import tisgrabber as tis

    ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")

    tis.declareFunctions(ic)

Now all functions of the tisgrabber.dll can be used and the program can continue with#

.. code-block:: python

    ic.IC_InitLibrary(0)
    hGrabber = ic.IC_ShowDeviceSelectionDialog(None)
