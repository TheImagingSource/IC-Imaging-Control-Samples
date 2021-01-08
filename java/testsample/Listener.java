//import java.awt.*;
import java.awt.image.BufferedImage;
import ic.*;

public class Listener extends GrabberListener{

    private int i = 0; 

    public PictureBox _PictureBox;  // will be assigned by the calling (main) class
                                    // So we can draw into it

    private BufferedImage _Image = null;

    public void  deviceLost(Grabber grabber){

   }

   /**
    * Image callback.
    * @param Grabber not used, always null
    * @param MemBuffer the image buffer
    * @param FrameNumber the number of the frame set by the driver.
    */
   public void frameReady(Grabber caller, MemBuffer Buffer, int FrameNumber){
        i = i + 1;
        if( i == 12){
            Buffer.save("Listener.bmp");
            System.out.println("Image saved");
        }

        Buffer.lock();
        byte [] Pixels = Buffer.getPtr();
        // get the middle of the image.
        SIZE ImageDimensions = Buffer.getSize();
        int start = ImageDimensions.cy / 2 * ImageDimensions.cx +  ImageDimensions.cx / 2;

        for( i = start; i < start + 10; i++)
        {
            System.out.print(Pixels[i]);
            System.out.print("  ");
        }
        
        System.out.println(); 
        Pixels = null;
        _Image = Buffer.getBufferedImage(_Image);
        _PictureBox.Image = _Image;
        _PictureBox.repaint();
        
        Buffer.unlock();
   }

}
