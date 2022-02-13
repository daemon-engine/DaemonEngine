#!/bin/bash
 
# Copy Assets to Output
cp -f -R -u -a Sandbox/Assets Sandbox/bin/Release/net6.0-windows/Assets

# Copy glfw3.dll file to Output
mkdir Sandbox/bin/Release/net6.0-windows/Binary
cp -f -u -a DaemonEngine.Windows.GraphicsLibraryFramework.DllImport/Binary/glfw3.dll Sandbox/bin/Release/net6.0-windows/Binary/glfw3.dll

