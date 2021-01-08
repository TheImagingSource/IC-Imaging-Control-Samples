# IC Java Samples
The sample "test1" is a sample showing some properties, how to snap images, how to get an image callback. It uses a graphical user interface.
The sample "stringprop" shows how to use camera properties of type string. Here you must exchange the camera name in the java file. 


### Running example
```
mkdir class
javac -classpath .;ic.jar  -d class test1.java
```
Java 11 and 12 create the "class" directory automatically, Java 8 does not.
The "class" directory contains the "*.class" files, which are the Java byte code files.

```
java -classpath .\class;ic.jar test1
```

For testing the string properties, echanage "test1" by "stringprop" in the command lines.
