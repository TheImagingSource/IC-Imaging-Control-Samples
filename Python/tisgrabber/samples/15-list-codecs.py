"""
This samples shows, how to list the avaialble codecs and save
their names into a list.
"""
import ctypes
import tisgrabber as tis

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)


def enumCodecCallback(codecname, userdata):
    """
    Callback for enumerating codecs
    :param codecname: Name of codec as byte array
    :param userdata: Python object, e.g a list for receiving the codec names
    :return: 0 for continuing, 1 for stopping the enumeration.
    """
    userdata.append(codecname.decode("utf-8"))
    return 0


enumCodecCallbackfunc = ic.ENUMCODECCB(enumCodecCallback)

ic.IC_InitLibrary(0)
codecs = []

ic.IC_enumCodecs(enumCodecCallbackfunc, codecs)
print("Available Codecs:")
for codec in codecs:
    print(codec)
