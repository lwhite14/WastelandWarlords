#ifndef RUNNER_H
#define RUNNER_H

#include <imgui.h>
#include <imgui_impl_glfw.h>
#include <imgui_impl_opengl3.h>
#include <stdio.h>
#include <GLFW/glfw3.h>
#include <iostream>
#include "Editor.h"
#include "Dependencies/stb/stb_image.h"

class Runner 
{
private:
	int m_width;
	int m_height;
	GLFWwindow* m_window;

	void MainLoop(Editor* editor)
	{
		while (!glfwWindowShouldClose(m_window) && !glfwGetKey(m_window, GLFW_KEY_ESCAPE))
		{
			glfwSwapBuffers(m_window);
			glfwPollEvents();

			editor->Render(m_window);
		}
	}

public:
	Runner(const char* windowName, int width, int height) 
	{
		m_width = width;
		m_height = height;

		if (!glfwInit())
		{
			exit(EXIT_FAILURE);
		}

		glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 4);
		glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 6);
		glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE);
		glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
		glfwWindowHint(GLFW_RESIZABLE, GL_FALSE);

		m_window = glfwCreateWindow(width, height, windowName, NULL, NULL);
		if (!m_window)
		{
			std::cerr << "UNABLE TO CREATE OPENGL CONTEXT" << std::endl;
			glfwTerminate();
			exit(EXIT_FAILURE);
		}
		glfwMakeContextCurrent(m_window);

		int iWidth, iHeight;
		int channels;
		unsigned char* iPixels = stbi_load("MapEditor.png", &iWidth, &iHeight, &channels, 4);

		GLFWimage images[1];
		images[0].width = iWidth;
		images[0].height = iHeight;
		images[0].pixels = iPixels;
		glfwSetWindowIcon(m_window, 1, images);
	}

	int Run(Editor* editor) 
	{
		glfwSetInputMode(m_window, GLFW_CURSOR, GLFW_CURSOR_NORMAL);

		editor->SetDimensions(m_width, m_height);
		editor->Init(m_window);

		MainLoop(editor);

		editor->CleanUp();
		glfwTerminate();

		return EXIT_SUCCESS;
	}
};


#endif //RUNNER_H