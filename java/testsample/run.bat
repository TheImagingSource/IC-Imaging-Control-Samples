@echo off
cls
mkdir class
javac -classpath .;ic.jar  -d class test1.java
java -classpath .\class;ic.jar test1
