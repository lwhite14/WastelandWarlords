#ifndef HEXREP_H
#define HEXREP_H

#include <string>
#include <imgui.h>

class HexRep 
{
public:
	float x;
	float y;
	std::string terrainType;
	ImColor colour;

	HexRep(float x, float y, std::string terrainType, ImColor colour) :
		x{ x },
		y{ y },
		terrainType{ terrainType },
		colour{ colour }
	{
		
	}
};

#endif //HEXREP_H