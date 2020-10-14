package ic;

import java.util.ArrayList;

public class Grabber{
    {
        System.loadLibrary("JNI_ICImagingControl"); 
        createGrabber();
     }

     @Override
     public void finalize() {
         deleteGrabber();
     }

     private long _grabber;
     private FrameHandlerSink _Sink;
     private ArrayList<Property> _Properties = new ArrayList<>();

     public native void createGrabber();
     public native void deleteGrabber();

     /**
      *  Show the IC Imaging Control built in device delection dialog. 
      */
     public native void showDevicePage();

     /**
      *  Show the IC Imaging Control built in device properties dialog. 
      */
      public native void showVCDPropertyPage();

      /**
       * Saves the state of the currently opened device to a file. The grabber state can be restored using loadDeviceStateFromFile
       * @param FilenName Specifies the filename of the settings data file. 
       * @return true, if the operation was successful. If an error occurred, 
       */   
    public native boolean saveDeviceStateToFile(String FilenName);

    /**
     * Restores the device settings from a file that was previously saved by saveDeviceStateToFile. 
     * @param FileName Specifies the XML file that contains the settings data. 
     * @param openDev If the parameter is set to true, the method tries to open the device that is specified by the settings data. If the parameter is false, no device will be opened, but the settings data will be applied to an already opened device. 
     * @return true, if the operation was successful, otherwise false.
     */
    public native boolean loadDeviceStateFromFile(String FileName, boolean openDev);

     /** 
      * Start the live video stream
      * @param show  If true, the live video is shown on screen.boolean
      * @return true, if the live mode was started successfully, false otherwise. 
      */
     public native boolean startLive(boolean show);
     /** 
      * Stops the live video stream
      * @return true, if the grabber could be stopped successfully, false otherwise. 
      */
     public native void stopLive();

     /** 
      * Checks whether the video capture device associated with this object still exists. A video capture device may become invalid at any time (e.g. if the device has been unplugged). 
      * @return   true, if a device is associated with this object and present in the system, false otherwise. 
       */
     public native boolean isDevValid();

     /** 
      * Checks whether the current video capture device is in live mode.
      * @return true ,if the grabber is currently in live mode, false otherwise. 
      */
     public native boolean isLive();

     /** 
      * Opens a video capture device for grabbing.
      * @param dev Specifies the name of the video capture device to be opened. 
      * @return true, if the device was opened successfully, false, otherwise. 
      */
     public native boolean openDev(String dev);

    /** 
     * Opens a video capture device for grabbing.
     * @param Serial Specifies the serial number of the video capture device to be opened.  
     * @return true, if the device was opened successfully, false, otherwise. 
     */
    public native boolean openDev(int Serial);

    /** 
     * This method sets the video format to be used by the currently active video capture device. 
     * @param videoformatstring Specifies the name of the new video format to be used by the currently active video capture device, e.g. "RGB32 (640x480)"
     * @return true, if the video format could be changed successfully, false otherwise.
     */
    public native boolean setVideoFormat( String videoformatstring );

    /** 
     * Sets the frame rate of the device.   
     * @param fps  The frame rate to be set, specified in frames per second. 
     * @return true, if the frame rate could be set successfully, false, otherwise. 
     */
    public native boolean setFPS( double fps );


    /** 
     * Returns the frame rate currently set. 
     * @return The current frame rate. The return value describes number for frames per second. 
     */
    public native double getFPS();


    /**
     * Assigns a FrameHandlerSink to a Grabber object.
     * @param _sink The FrameHandlerSink to be assigned.
     * @retuurn true The sink type was changed successfully, false An error occurred. 
     */
    public native boolean setSinkType(FrameHandlerSink _sink);
    
    //private native long addInternalListener(GrabberListener _Listener);

    /**
     * Adds a from GrabberListener inherited class to the grabber for callbacks.
     * 
     * @param _Listener The Listener class to be added.
     */
    public native void addListener(GrabberListener _Listener);


    /**
     * Sets the size of the video window. 
     * @param Width Specifies the width of the video window.
     * @param Height Specifies the height of the video window
     * @return
     */
    public native boolean setWindowSize(int Width, int Height);

    /** 
     * Enables or disables the default window size lock of the video window. 
     * @param Defaultpos If Defaultpos is set to true, the grabber adjusts the live video window display to the width and height of the current video format. In this mode, it is not possible to change the grabber's video window size. If bDefault is set to false, the size of the window for the live video display is independent from the width and height of the current video format. In this mode, the size and position of the live video window may be set by calling setDefaultWindowPosition. The default value is true. 
     */
    public native boolean setDefaultWindowPosition(boolean Defaultpos);
    
    /**
     * Query the property class for the property factory function
     * @param PropertyName
     * @return 1 : PropertyInteger, 2 : PropertyDouble, 3 : PropertyBoolean, 4:OnPush, 5: PropertyString.
     */
    private native int getPropertyClass(String PropertyName);

    /**
     * Get all properties from the video capture device automaticalle
     * They are saved into the _Properties member.
     * @return number of found properties.
     */
    public native int getProperties();

    private Property findProperty(String PropertyName)
    {
        for( Property p : _Properties){
            if( p._name.equals(PropertyName)){
                return p;
            }
        }
        return null;
    }

    /**
     * Check, whether the property of given name is available.
     * @param PropertyName Name of Property to check.
     * @return true: Property available, false: not available 
     */
    public boolean PropertyAvailable(String PropertyName){
        if(findProperty(PropertyName) != null ){
            return true;
        }
        return false;

    }

    public ArrayList<String> getAvailableProperties(){
        ArrayList<String> pl = new ArrayList<>();
        for( Property p : _Properties){
            pl.add(p._name);
        }
        return pl;
    }

    /**
     * Set a property
     * @param PropertyName Name of property to be set
     * @param Value New int value
     * @return true, if the property exists, false if not.
     */
    public boolean PropertySet(String PropertyName,int Value) throws Exception{
        Property p = findProperty(PropertyName);
        if( p != null)
        {
            p.set(Value);
            return true;
        }
        throw new Exception(String.format("Property %s not found.", PropertyName) );
    }

    /**
     * Set a property
     * @param PropertyName Name of property to be set
     * @param Value New double value
     * @return true, if the property exists, false if not.
     */
    public boolean PropertySet(String PropertyName,double Value) throws Exception{
        Property p = findProperty(PropertyName);
        if( p != null)
        {
            p.set(Value);
            return true;
        }
        throw new Exception(String.format("Property %s not found.", PropertyName) );
    }

    /**
     * Set a property
     * @param PropertyName Name of property to be set
     * @param Value New boolean value
     * @return true, if the property exists, false if not.
     */
    public boolean PropertySet(String PropertyName,Boolean Value) throws Exception{
        Property p = findProperty(PropertyName);
        if( p != null)
        {
            p.set(Value);
            return true;
        }
        throw new Exception(String.format("Property %s not found.", PropertyName) );
    }

    /**
     * Set a property
     * @param PropertyName Name of property to be set
     * @param Value New strng value
     * @return true, if the property exists, false if not.
     */
    public boolean PropertySet(String PropertyName,String Value) throws Exception{
        Property p = findProperty(PropertyName);
        if( p != null)
        {
            p.set(Value);
            return true;
        }
        throw new Exception(String.format("Property %s not found.", PropertyName) );
    }

    /**
     * Get a property value
     * @param <T>
     * @param PropertyName
     * @return The value of type T of the property or null, if the property was not found.
     */
    public <T> T PropertyGet(String PropertyName) throws Exception{
        Property p = findProperty(PropertyName);
        if( p != null)
        {
            return p.get();
        }
        throw new Exception(String.format("Property %s not found.", PropertyName) );
    }
    /**
     * Get a minimum property value
     * @param <T>
     * @param PropertyName
     * @return The value of type T of the property or null, if the property was not found.
     */
    public <T> T PropertyGetMin(String PropertyName) throws Exception{
        Property p = findProperty(PropertyName);
        if( p != null)
        {
            return p.getMinimum();
        }

        throw new Exception(String.format("Property %s not found.", PropertyName) );
    }

    /**
     * Get a maximum property value
     * @param <T>
     * @param PropertyName
     * @return The value of type T of the property or null, if the property was not found.
     */
    public <T> T PropertyGetMax(String PropertyName) throws Exception{
        Property p = findProperty(PropertyName);
        if( p != null)
        {
            return p.getMaximum();
        }
        throw new Exception(String.format("Property %s not found.", PropertyName) );
    }

}