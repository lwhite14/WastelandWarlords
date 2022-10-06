
#include "Runner.h"

int main(int)
{
    Runner* runner = new Runner("Map Editor", 1600, 900);
    Editor* editor = new Editor();
    return runner->Run(editor);
}
