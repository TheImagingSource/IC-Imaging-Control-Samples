import ic.*;
import java.util.List;

public class stringprop {
    DShowLib _DShowLib;
    Grabber _grabber;

    public stringprop() {
        _DShowLib = new DShowLib();
        _DShowLib.InitLibrary();
        _grabber = new Grabber();

        _grabber.openDev("DFK Z30GP031");

        if(_grabber.isDevValid()){
            if( _grabber.getProperties() > 0){
                /*
                System.out.println("Available Properties:");
                for(String pn : _grabber.getAvailableProperties()){
                    System.out.println((pn));
                }*/
                        

                System.out.println("Aavailable Strings:");
                try{
                    List<String> mapstrings = _grabber.PropertyGetStrings("WhiteBalance_Auto Preset");
                    for(String mapstring:mapstrings)  
                        System.out.println(mapstring);  
                }
                catch(Exception ex){
                    System.out.println(ex.getMessage());
                }

                System.out.println("Current String:");
                try{
                    String value = _grabber.PropertyGet("WhiteBalance_Auto Preset");
                    System.out.println(value);
                }
                catch(Exception ex){
                    System.out.println(ex.getMessage());
                }

                try{
                    System.out.println("Set property");
                    _grabber.PropertySet("WhiteBalance_Auto Preset","Auto Deep Shade");
                }
                catch(Exception ex){
                    System.out.println(ex.getMessage());
                }

                System.out.println("New String:");
                try{
                    String value = _grabber.PropertyGet("WhiteBalance_Auto Preset");
                    System.out.println(value);
                }
                catch(Exception ex){
                    System.out.println(ex.getMessage());
                }


            }
        }
    }

    public static void main(String[] args)
    {
        //JOptionPane.showMessageDialog(null, "Connect debugger");
        new stringprop();
    }

}
