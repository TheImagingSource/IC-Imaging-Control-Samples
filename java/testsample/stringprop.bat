@echo off
cls
mkdir class
javac -classpath .;ic.jar  -d class stringprop.java
java -classpath .\class;ic.jar stringprop
