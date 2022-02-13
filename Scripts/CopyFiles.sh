#!/bin/bash
 
# Copy Assets to Output
cp -f -R -u -a Sandbox/Assets Output/Assets

# Copy glfw3.dll file to Output
mkdir Output/Binary
cp -f -u -a DaemonEngine.Windows.GraphicsLibraryFramework.DllImport/Binary/glfw3.dll Output/Binary/glfw3.dll

