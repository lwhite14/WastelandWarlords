# Wasteland Warlords
4X strategy game developed with Unity.

## Source Code
### Unity Project
You can download a build from the releases tab, or download the source code, inspect it, and build it yourself. The game was made with Unity 2021.3.11, you will need this version or a later version to open the project. Download the source code, unzip the compressed folder, and open that folder in Unity Hub to inspect the source project. The project will require the [YamlDotNet](https://assetstore.unity.com/packages/tools/integration/yamldotnet-for-unity-36292) package in order to load the map files. 

### Map Editor
The map editor is developed using [glfw](https://www.glfw.org/), [imgui](https://github.com/ocornut/imgui), [yaml-cpp](https://github.com/jbeder/yaml-cpp), and [nativefiledialog](https://github.com/mlabbe/nativefiledialog). A build of the map editor is included within the source code, however, if you wish to debug this application then you will need all of the dependancies. The imgui dependancies are included within the repository, however, you will need to install glfw, yaml-cpp, and nativefiledialog yourself, and then link these libraries to the Visual Studio solution in order to debug the code. You will also need glad, you can download the include and library files from [here](https://glad.dav1d.de/). Include the glad.c file at the root of the map editor project, this is essential as this file is machine specific and as such couldn't be included in the shared repository.

## Download
 - [Download Source Code.](https://github.com/lwhite14/WastelandWarlords/archive/main.zip)
 - Or go to the releases tab and download both a build and the source code!
#
Made by Luke White!