import PyQt5.QtCore
import PyQt5.QtWidgets as qt5
from PyQt5.QtCore import pyqtSignal, QTimer, QObject
import os
import sys
import cv2 as cv2
import numpy as np
import ctypes 
import tisgrabber as tis

ic = ctypes.cdll.LoadLibrary("./tisgrabber_x64.dll")
tis.declareFunctions(ic)
ic.IC_InitLibrary(0)


class WorkerSignals(QObject):
    newimage = pyqtSignal(object)


class CallbackUserdata(ctypes.Structure):
    """ Example for user data passed to the callback function. 
    """
    _namespace__ = "userdata"
    signals = WorkerSignals() 

    def __init__(self, index):
        self.index = index
        self.cvMat = None


def frameReadyCallback(hGrabber, pBuffer, framenumber, pData):
    """ This is an example callback function for image processing with 
        opencv. The image data in pBuffer is converted into a cv Matrix
        and with cv.mean() the average brightness of the image is
        measuered.

    :param: hGrabber: This is the real pointer to the grabber object.
    :param: pBuffer : Pointer to the first pixel's first byte
    :param: framenumber : Number of the frame since the stream started
    :param: pData : Pointer to additional user data structure
    """
    # print("camera {}". format(pData.index))
    Width = ctypes.c_long()
    Height = ctypes.c_long()
    BitsPerPixel = ctypes.c_int()
    colorformat = ctypes.c_int()

    # Query the image description values
    ic.IC_GetImageDescription(hGrabber, Width, Height, BitsPerPixel, colorformat)

    # Calculate the buffer size
    bpp = int(BitsPerPixel.value/8.0)
    buffer_size = Width.value * Height.value * bpp

    if buffer_size > 0:
        image = ctypes.cast(pBuffer, 
                            ctypes.POINTER(
                                ctypes.c_ubyte * buffer_size))

        pData.cvMat = np.ndarray(buffer=image.contents,
                                dtype=np.uint8,
                                shape=(Height.value,
                                        Width.value,
                                        bpp))

        pData.signals.newimage.emit(pData)


# Manage the callbacks
# Create the function pointer.
frameReadyCallbackfunc = ic.FRAMEREADYCALLBACK(frameReadyCallback)


class Camera():
    def __init__(self, index, winID, OnNewImageHandler):
        """
        Creaets a new camera object.
         :param index: is used for identify the camera.
         :param winID: The QT Widget window ID
        """
        self.hGrabber = ic.IC_CreateGrabber()
        self.userdata = CallbackUserdata(index)
        self.userdata.signals.newimage.connect(OnNewImageHandler)
        self.winID = winID
        self.loadDeviceState()

    def showDeviceSelectionDlg(self):
        ic.IC_StopLive(self.hGrabber)
        self.hGrabber = ic.IC_ShowDeviceSelectionDialog(None)

        if ic.IC_IsDevValid(self.hGrabber):
            self.startCamera()
            ic.IC_SaveDeviceStateToFile(self.hGrabber,
                                        tis.T("camera{}.xml".format(self.userdata.index)))

    def showPropertyDlg(self):
        if ic.IC_IsDevValid(self.hGrabber):
            ic.IC_ShowPropertyDialog(self.hGrabber)
            ic.IC_SaveDeviceStateToFile(self.hGrabber,
                                        tis.T("camera{}.xml".format(self.userdata.index)))

    def loadDeviceState(self):
        if os.path.exists("camera{}.xml".format(self.userdata.index)):
            ic.IC_LoadDeviceStateFromFile(self.hGrabber, tis.T("camera{}.xml".format(self.userdata.index)))
            self.startCamera()

    def startCamera(self):
        """Start the camera
        :param UserData user data connected with the camera
        :param Camera The camera to start
        """
        if ic.IC_IsDevValid(self.hGrabber):
            ic.IC_SetHWnd(self.hGrabber, self.winID)
            ic.IC_SetFrameReadyCallback(self.hGrabber, frameReadyCallbackfunc, self.userdata)
            ic.IC_SetContinuousMode(self.hGrabber, 0)
            ic.IC_StartLive(self.hGrabber, 1)

    def stopCamera(self):
        """Start the camera
        :param UserData user data connected with the camera
        :param Camera The camera to start
        """
        if ic.IC_IsDevValid(self.hGrabber):
            ic.IC_StopLive(self.hGrabber)

##########################################################################


def selectDevice(cam):
    cameras[cam].showDeviceSelectionDlg()


def showProperties(cam):
    cameras[cam].showPropertyDlg()


def Close():
    for camera in cameras:
        camera.stopCamera()        

    app.quit()


def OnNewImage(userdata):
    print("camera {}".format(userdata.index))
    # Calculate average image brightness
    gray = cv2.cvtColor(userdata.cvMat, cv2.COLOR_BGR2GRAY)
    mean = cv2.mean(gray)
    brightnessbars[userdata.index].setValue(int(mean[0]))

#######################################################


cameracount = ic.IC_GetDeviceCount()     # Get number of cameras.
videowindows = []
brightnessbars = []
cameras = []

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
cameraMenu = mainMenu.addMenu('&Cameras')
for i in range(0, cameracount):
    devselAct = PyQt5.QtWidgets.QAction("&Select {}".format(i+1), app)
    devselAct.triggered.connect(lambda checked, index=i:  selectDevice(index))
    cameraMenu.addAction(devselAct)

######################################################
propertiesMenu = mainMenu.addMenu('&Properties')
for i in range(0, cameracount):
    devselAct = PyQt5.QtWidgets.QAction("&Camera {}".format(i+1), app)
    devselAct.triggered.connect(lambda checked, index=i: showProperties(index))
    propertiesMenu.addAction(devselAct)

MainWindow = PyQt5.QtWidgets.QWidget()

vboxlayout = qt5.QVBoxLayout()
hboxlayout = qt5.QHBoxLayout()

# Add as many video windows as we have cameras.
for i in range(0, cameracount):
    vvboxlayout = qt5.QVBoxLayout()
    videowindow = qt5.QWidget()

    brightnessbar = qt5.QProgressBar()
    brightnessbar.setRange(0, 256)
    brightnessbar.setOrientation(PyQt5.QtCore.Qt.Horizontal)
    brightnessbar.setValue(25)

    vvboxlayout.addWidget(videowindow)
    vvboxlayout.addWidget(brightnessbar)

    hboxlayout.addLayout(vvboxlayout)
    videowindows.append(videowindow)
    brightnessbars.append(brightnessbar)

# vboxlayout.addLayout(hboxlayout)
MainWindow.setLayout(hboxlayout)
w.setCentralWidget(MainWindow)

w.show()

# Create the camera objects.
for i in range(0, cameracount):
    cameras.append(Camera(i, videowindows[i].winId(), OnNewImage))

app.exec()
