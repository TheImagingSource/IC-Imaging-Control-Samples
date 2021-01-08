import java.awt.*;
import java.awt.image.*;
import java.awt.Graphics;
import java.awt.geom.AffineTransform;
import javax.swing.*;

public class PictureBox extends JComponent {
    private static final long serialVersionUID = 1L;
    public BufferedImage Image;

    @Override
    protected void paintComponent(final Graphics g) {
        super.paintComponent(g);
        if( Image != null){
            BufferedImage Image2 = createFlipped(Image);
            g.drawImage(Image2,0,0, this.getWidth(), this.getHeight(), this);
        }
        else{
            g.setColor(Color.WHITE);
            g.fillRect(0, 0, this.getWidth(), this.getHeight());
        }
        g.dispose();
    }

    private static BufferedImage createFlipped(BufferedImage image)
    {
        AffineTransform at = new AffineTransform();
        at.concatenate(AffineTransform.getScaleInstance(1, -1));
        at.concatenate(AffineTransform.getTranslateInstance(0, -image.getHeight()));
        return createTransformed(image, at);
    } 

    private static BufferedImage createTransformed(
        BufferedImage image, AffineTransform at)
    {
        BufferedImage newImage = new BufferedImage(
            image.getWidth(), image.getHeight(),
            BufferedImage.TYPE_INT_ARGB);
        Graphics2D g = newImage.createGraphics();
        g.transform(at);
        g.drawImage(image, 0, 0, null);
        g.dispose();
        return newImage;
    }
}