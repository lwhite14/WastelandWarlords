
#include "Runner.h"

int main(int)
{
    Runner* runner = new Runner("Map Editor", 800, 600);
    Editor* editor = new Editor();
    return runner->Run(editor);
}
