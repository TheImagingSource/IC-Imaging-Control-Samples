package ic;

public class FrameHandlerSink{
    {
        System.loadLibrary("JNI_ICImagingControl"); 
    }

    private long _psink;
    private ic.MemBuffer[] _MemBufferCollection; 

    /**
     * Creates a FrameHanderSink for image handling in memory
     * @param color PixelFormat DShowLib::tColorFormatEnum
     * @param countBuffers Number of buffers in the ring buffer. Should be more than 1.
     */
    public FrameHandlerSink(DShowLib.tColorformatEnum color, int countBuffers) {
        _MemBufferCollection = createFrameHandlerSink(color.ordinal(), countBuffers);
	}

	@Override
    public void finalize() {
        deleteFrameHandlerSink();
    }

    public native MemBuffer[] createFrameHandlerSink(int color, int countBuffers);
    private native void deleteFrameHandlerSink();

    /**
     * Changes this sink's operating mode between Grab and Snap mode. 
     * In grab mode, all frames reaching the sink are presented to the frame filter 
     * or filter chain and then copied into the MemBufferCollection. After that, 
     * the frameReady event of the GrabberListener is called. In snap mode, snapImages
     * or snapImagesAsync has to be called in order to trigger the image acquisition process. 
     * @param mode A boolean determining the new snap mode. True: snapImages() must be called to save and image in memory, 
     *              false : all images from video capture device are saved automatically in memory.
     */
    public native void setSnapMode( boolean mode ); 

    /**
     * Queues up a snap job, thus allowing a number of frames to be copied into the
     *  MemBufferCollection. The method returns when either the specified number of frames 
     * are snapped, or the timeout has occurred. 
     * @param count Number of frames to copy into the MemBufferCollection
     * @param timeout Number of milliseconds to wait before returning from this function, should no frames be available from the device. 
     * @return true on success, false on error, e.g. timeout
     */
    public native boolean snapImages(  int count, int timeout);

    public native MemBuffer getLastAcqMemBuffer();

    /**
     * Returns the current MemBufferCollection. 
     * @return Array containing the MemBuffers of the FrameHandlerSink
     */
	public MemBuffer[] getMemBufferCollection() {
		return _MemBufferCollection;
	}  
}