#!/bin/bash

dotnet clean --configuration Release
 
dotnet publish --configuration Release --force --output Output --runtime win10-x64
