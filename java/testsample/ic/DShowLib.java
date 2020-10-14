package ic;

public class DShowLib{
    static {
        try {
            System.loadLibrary("JNI_ICImagingControl"); // Load native library hello.dll (Windows) or libhello.so (Unixes)
            System.out.println("DLL Loaded");
            //  at runtime
            // This library contains a native method called sayHello()

        } catch (Exception e) {
            System.out.println("Failed to load DLL");
        }
     } 
 
     // Declare an instance native method sayHello() which receives no parameter and returns void
   public static enum tColorformatEnum
   {
    eInvalidColorformat,
    eRGB32,
    eRGB24,
    eRGB565,
    eRGB555,
    eRGB8,	
    eUYVY,
    eY800,
    eYGB1,
    eYGB0,
    eBY8,
    eY16,
    eRGB64  
   } ;
   public native boolean InitLibrary();
   public native void ExitLibrary();
}

