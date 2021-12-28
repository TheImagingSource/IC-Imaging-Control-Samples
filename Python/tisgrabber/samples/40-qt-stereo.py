import PyQt5.QtCore
import PyQt5.QtWidgets as qt5
import sys
import cv2 as cv2
import numpy as np

import time
import ctypes 
import tisgrabber as tis

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)
ic.IC_InitLibrary(0)

# Create the camera objects.
LeftCamera = ic.IC_CreateGrabber()
RightCamera = ic.IC_CreateGrabber()


class CallbackUserdata(ctypes.Structure):
    """ Example for user data passed to the callback function. """
    def __init__(self):
        self.width = 0
        self.height = 0
        self.BytesPerPixel = 0
        self.buffer_size = 0
        self.oldbrightness = 0
        self.getNextImage = 0
        self.cvMat = None


def LeftCallback(hGrabber, pBuffer, framenumber, pData):
    """ This is an example callback function for image processing  with
        OpenCV

    :param: hGrabber: This is the real pointer to the grabber object.
    :param: pBuffer : Pointer to the first pixel's first byte
    :param: framenumber : Number of the frame since the stream started
    :param: pData : Pointer to additional user data structure
    """
    if pData.getNextImage == 1:
        pData.getNextImage = 2
        print("    Left")
        if pData.buffer_size > 0:
            image = ctypes.cast(pBuffer, ctypes.POINTER(ctypes.c_ubyte * pData.buffer_size))

            pData.cvMat = np.ndarray(buffer=image.contents,
                                     dtype=np.uint8,
                                     shape=(pData.height.value,
                                            pData.width.value,
                                            pData.BytesPerPixel))
        pData.getNextImage = 0
        

def RightCallback(hGrabber, pBuffer, framenumber, pData):
    """ This is an example callback function for image processing  with 
        OpenCV. 

    :param: hGrabber: This is the real pointer to the grabber object.
    :param: pBuffer : Pointer to the first pixel's first byte
    :param: framenumber : Number of the frame since the stream started
    :param: pData : Pointer to additional user data structure
    """
    if pData.getNextImage == 1:
        pData.getNextImage = 2
        print("    Right")
        if pData.buffer_size > 0:
            image = ctypes.cast(pBuffer, ctypes.POINTER(ctypes.c_ubyte * pData.buffer_size))

            pData.cvMat = np.ndarray(buffer=image.contents,
                                     dtype=np.uint8,
                                     shape=(pData.height.value,
                                            pData.width.value,
                                            pData.BytesPerPixel))

        pData.getNextImage = 0


# Manage the callbacks
# Create the function pointer.
LeftCallbackfunc = ic.FRAMEREADYCALLBACK(LeftCallback)
RightCallbackfunc = ic.FRAMEREADYCALLBACK(RightCallback)

LeftUserData = CallbackUserdata()
RightUserData = CallbackUserdata()


ic.IC_SetFrameReadyCallback(LeftCamera, LeftCallbackfunc, LeftUserData)
ic.IC_SetFrameReadyCallback(RightCamera, RightCallbackfunc, RightUserData)


##########################################################################

def SelectLeftDevice():
    global LeftCamera

    ic.IC_StopLive(LeftCamera)

    LeftCamera = ic.IC_ShowDeviceSelectionDialog(None)

    if ic.IC_IsDevValid(LeftCamera): 
        ic.IC_SetHWnd(LeftCamera,  LeftVideo.winId())

        startCamera(LeftUserData, LeftCamera)
        ic.IC_SaveDeviceStateToFile(LeftCamera, tis.T("left.xml"))


def ShowLeftProperties():
    global LeftCamera

    if ic.IC_IsDevValid(LeftCamera):
        ic.IC_ShowPropertyDialog(LeftCamera)
        ic.IC_SaveDeviceStateToFile(LeftCamera, tis.T("left.xml"))

def SelectRightDevice():
    global RightCamera

    ic.IC_StopLive(RightCamera)

    RightCamera = ic.IC_ShowDeviceSelectionDialog(None)

    if ic.IC_IsDevValid(RightCamera): 
        ic.IC_SetHWnd(RightCamera, RightVideo.winId())
        startCamera(LeftUserData, RightCamera)
        ic.IC_SaveDeviceStateToFile(RightCamera, tis.T("right.xml"))


def ShowRightProperties():
    global RightCamera

    if ic.IC_IsDevValid(RightCamera):
        ic.IC_ShowPropertyDialog(RightCamera)
        ic.IC_SaveDeviceStateToFile(RightCamera, tis.T("right.xml"))


def Close():
    global RightCamera
    global LeftCamera
    if ic.IC_IsDevValid(LeftCamera):
        ic.IC_StopLive(LeftCamera)

    if ic.IC_IsDevValid(RightCamera):
        ic.IC_StopLive(RightCamera)

    app.quit()


def restorelastuseddevices():
    ''' Restore the last used devices
    '''
    ic.IC_LoadDeviceStateFromFile(LeftCamera, tis.T("left.xml"))
    if ic.IC_IsDevValid(LeftCamera): 
        startCamera(LeftUserData, LeftCamera)

    ic.IC_LoadDeviceStateFromFile(RightCamera, tis.T("right.xml"))
    if ic.IC_IsDevValid(RightCamera):
        startCamera(RightUserData, RightCamera)


def CreateUserData(ud, camera):
    ''' Create the user data for callback for the passed camera
    :param ud User data to create
    :param camera The camera connected to the user data
    '''
    ud.width = ctypes.c_long()
    ud.height = ctypes.c_long()
    iBitsPerPixel = ctypes.c_int()
    colorformat = ctypes.c_int()

    # Query the values
    ic.IC_GetImageDescription(camera, ud.width, ud.height, iBitsPerPixel, colorformat)

    ud.BytesPerPixel = int(iBitsPerPixel.value / 8.0)
    ud.buffer_size = ud.width.value * ud.height.value * ud.BytesPerPixel
    ud.getNextImage = 0


def startCamera(UserData, camera):
    '''Start the passed camera
    :param UserData user data connected with the camera
    :param Camera The camera to start
    '''
    ic.IC_SetContinuousMode(camera, 0)
    ic.IC_StartLive(camera, 1)
    CreateUserData(UserData, camera)


def OnSnapImagePair():
    print("wait")
    RightUserData.getNextImage = 1
    LeftUserData.getNextImage = 1
    while RightUserData.getNextImage != 0 or LeftUserData.getNextImage != 0:
        time.sleep(0.005)

    print("done")
    # Here we (should) have our image par in the user data as numpy // cv Matrix
    cv2.imwrite("Left.bmp", cv2.flip(LeftUserData.cvMat, 0))
    cv2.imwrite("right.bmp", cv2.flip(RightUserData.cvMat, 0))

#######################################################


app = PyQt5.QtWidgets.QApplication(sys.argv)

w = PyQt5.QtWidgets.QMainWindow()
w.resize(1280, 480)
w.move(300, 300)
w.setWindowTitle('Stereo')

mainMenu = w.menuBar()
fileMenu = mainMenu.addMenu('&File')

exitAct = PyQt5.QtWidgets.QAction("&Exit", app)
exitAct.setStatusTip("Exit program")
exitAct.triggered.connect(Close)
fileMenu.addAction(exitAct)
#######################################################
LeftMenu = mainMenu.addMenu('&Left Camera')
devselAct = PyQt5.QtWidgets.QAction("&Select", app)
devselAct.triggered.connect(SelectLeftDevice)
LeftMenu.addAction(devselAct)

devpropAct = PyQt5.QtWidgets.QAction("&Properties", app)
devpropAct.triggered.connect(ShowLeftProperties)
LeftMenu.addAction(devpropAct)

######################################################
RightMenu = mainMenu.addMenu('&Right Camera')
devselAct = PyQt5.QtWidgets.QAction("&Select", app)
devselAct.triggered.connect(SelectRightDevice)
RightMenu.addAction(devselAct)

devpropAct = PyQt5.QtWidgets.QAction("&Properties", app)
devpropAct.triggered.connect(ShowRightProperties)
RightMenu.addAction(devpropAct)

# Create Window layout with 2 video windows

MainWindow = PyQt5.QtWidgets.QWidget()

vboxlayout = qt5.QVBoxLayout()

hboxlayout = qt5.QHBoxLayout()

LeftVideo = qt5.QWidget()
RightVideo = qt5.QWidget()

hboxlayout.addWidget(LeftVideo)
hboxlayout.addWidget(RightVideo)

vboxlayout.addLayout(hboxlayout)
SnapButton = qt5.QPushButton("Snap Image Pair")
SnapButton.clicked.connect(OnSnapImagePair)

vboxlayout.addWidget(SnapButton)

MainWindow.setLayout(vboxlayout)
w.setCentralWidget(MainWindow)

w.show()

# Pass the window handles to the left and right camera.
ic.IC_SetHWnd(LeftCamera,  LeftVideo.winId())
ic.IC_SetHWnd(RightCamera,  RightVideo.winId())

restorelastuseddevices()

app.exec()
