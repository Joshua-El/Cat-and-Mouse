#!/bin/bash
#In the official documemtation the line above always has to be the first line of any script file.

#Author: Joshua Elmer
#Course: CPSC223n
#Semester: Fall 2020
#Assignment: Final
#Program title: Final

echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo "Compile the file finaluserinterface.cs:"
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:cat_n_mouse_userinterface.dll cat_n_mouse_userinterface.cs

echo "Compile and link finalmain.cs:"
mcs -r:System -r:System.Windows.Forms -r:cat_n_mouse_userinterface.dll -out:go.exe cat_n_mouse_main.cs

echo "Run the Final program"
./go.exe

echo "The bash script has terminated."
