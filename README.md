# Post-Apocalyptic Strategy Game
4X strategy game developed with Unity.

## Source Code
### Unity Project
You can download a build from the releases tab, or download the source code, inspect it, and build it yourself. The project will require the [YamlDotNet](https://assetstore.unity.com/packages/tools/integration/yamldotnet-for-unity-36292) package in order to load the map files. 

### Map Editor
The map editor is developed using [glfw](https://www.glfw.org/), [imgui](https://github.com/ocornut/imgui), [yaml-cpp](https://github.com/jbeder/yaml-cpp), and [nativefiledialog](https://github.com/mlabbe/nativefiledialog). The imgui dependancies are included within the repository, however, you will need to install glfw, yaml-cpp, and nativefiledialog yourself, and then link these libraries to the Visual Studio solution in order to debug the code. You will also need glad, you can download the include and library files from [here](https://glad.dav1d.de/). Include the glad.c file at the root of the map editor project, this is essential as this file is machine specific and as such couldn't be included in the shared repository.