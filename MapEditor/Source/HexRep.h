#ifndef HEXREP_H
#define HEXREP_H

#include <string>
#include <imgui.h>

class HexRep 
{
public:
	float x;
	float y;

	int trueCoordX;
	int trueCoordZ;

	float height;

	std::string terrainType;
	ImColor colour;

	HexRep(float x, float y, std::string terrainType, ImColor colour, float height) :
		x{ x },
		y{ y },
		terrainType{ terrainType },
		colour{ colour },
		height{ height }
	{
		
	}
};

#endif //HEXREP_H