#ifndef HEXCALC_H
#define HEXCALC_H

#include <imgui.h>

namespace HexCalc 
{
	inline static ImVec2 CalcHexPos(ImVec2 roughPos);
	inline static int RoundTo(int numToRound, int multiple);
	inline static bool IsOdd(int x);

	inline static ImVec2 CalcHexPos(ImVec2 roughPos)
	{
		int newX = RoundTo(roughPos.x, 30);
		int newY = RoundTo(roughPos.y, 30);
		if (IsOdd(newY / 30)) 
		{
			newX += 15;
		}
		return ImVec2(newX, newY);
	}

	inline static int RoundTo(int numToRound, int multiple)
	{
		if (multiple == 0)
			return numToRound;

		int remainder = numToRound % multiple;
		if (remainder == 0)
			return numToRound;

		return numToRound + multiple - remainder;
	}

	inline static bool IsOdd(int x) 
	{
		if (x % 2) 
		{
			return true;
		}
		else 
		{
			return false;
		}
	}

}

#endif //HEXCALC_H