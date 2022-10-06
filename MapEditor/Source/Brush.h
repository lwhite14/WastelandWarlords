#ifndef BRUSH_H
#define BRUSH_H

#include <iostream>
#include <imgui.h>

class Brush 
{
public:
	std::string name;
	ImColor colour;
	
	Brush(std::string name, ImColor colour) :
		name{ name },
		colour{ colour }
	{
	
	}
};


#endif //BRUSH_H