package ic;
import ic.DShowLib.tColorformatEnum;
import java.awt.image.*;


public class MemBuffer{
    {
        System.loadLibrary("JNI_ICImagingControl"); 
    }
    private long _pMemBuffer;

    /**
     * Returns the size of the MemBuffer in bytes. 
     * The size of the MemBuffer in bytes. 

     * @return
     */
    public native int getBufferSize();

    /**
     * Writes the buffer to disk as a .BMP file. 
     * @param filename Specifies the name and the path of the file. The path is specified relative to the current directory. 
     * @return true on success, false on error.
     */
    public native boolean save(String filename);    

    /**
     * Returns the width and height of the image data stored in this buffer. 
     * @return A SIZE structure, containing the width and height of the image data stored in this buffer
     */
    public native SIZE getSize();

    /**
     * Returns the number of bits that represent a single pixel. 
     * @return The number of bits that represent a single pixel with the current color format.
     */
    public native int getBitsPerPixel();

    /**
     * Returns the color format of this buffer. 
     * @return The color format of this buffer as a tColorformatEnum.
     */
    public native tColorformatEnum getColorformat();

    /**
     * Returns a pointer to the image data. 
     * 
     * Please note, that depending of the frame type, images may be stored bottom up, 
     * therefore the pointer returned by this method may point to the first byte of 
     * the first pixel of the last line of the image. 
     * @return A pointer to the image data. 
     */
    public native byte[] getPtr();

    /**
     * This method increments the lock count of this buffer by 1, 
     * so that the grabber cannot overwrite the data in this buffer. 
     */
    public native void lock();

    /**
     * This method decrements the lock count by 1. 
     * The buffer is locked, if the lock count is greater than 0. If the lock count is 0, this method has no effect. 
     */
    public native void unlock();

    /**
     * Returns the image in the MemBuffer as Buffered image.
     * @param Image If not null, then the passed image will receive the new content.  If null, a new object is created.
     * @return The new image.
     */
    public native BufferedImage getBufferedImage(BufferedImage Image); 

}

