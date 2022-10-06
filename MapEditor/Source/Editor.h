#ifndef EDITOR_H
#define EDITOR_H

#include <vector>
#include <iostream>
#include "ImGuiUtils.h"
#include "Runner.h"

class Editor 
{
private:
	ImGuiWindowFlags m_windowFlags;
	int m_width;
	int m_height;
	std::vector<ImVec2> hexLocations;

public:
	Editor() 
	{
		m_windowFlags = ImGuiWindowFlags_None;
	}

	void Init(GLFWwindow* window)
	{
		IMGUI_CHECKVERSION();
		ImGui::CreateContext();
		ImGui::StyleColorsClassic();
		ImGui_ImplGlfw_InitForOpenGL(window, true);
		ImGui_ImplOpenGL3_Init("#version 460");

		ImGui::SetUpStyle();

		const ImGuiViewport* viewport = ImGui::GetMainViewport();
		ImGui::SetNextWindowPos(viewport->WorkPos);
		ImGui::SetNextWindowSize(viewport->WorkSize);
		ImGui::SetNextWindowViewport(viewport->ID);
		m_windowFlags |= ImGuiWindowFlags_NoResize;
		m_windowFlags |= ImGuiWindowFlags_NoTitleBar;
		//m_windowFlags |= ImGuiWindowFlags_NoScrollbar;
		m_windowFlags |= ImGuiWindowFlags_MenuBar;
		m_windowFlags |= ImGuiWindowFlags_NoMove;
		m_windowFlags |= ImGuiWindowFlags_NoCollapse;
		//m_windowFlags |= ImGuiWindowFlags_NoNav;
		//m_windowFlags |= ImGuiWindowFlags_NoBackground;
		m_windowFlags |= ImGuiWindowFlags_NoBringToFrontOnFocus;
		m_windowFlags |= ImGuiWindowFlags_NoDocking;
		//m_windowFlags |= ImGuiWindowFlags_UnsavedDocument;
		m_windowFlags |= ImGuiWindowFlags_NoNavFocus;
	}

	void Render() 
	{
		glEnable(GL_DEPTH_TEST);
		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
		glViewport(0, 0, m_width, m_height);

		ImGui_ImplOpenGL3_NewFrame();
		ImGui_ImplGlfw_NewFrame();
		ImGui::NewFrame();

		ImGuiIO& io = ImGui::GetIO();

		ImGui::Begin("Editor", (bool*)0, m_windowFlags);
        if (ImGui::BeginMenuBar())
        {
			if (ImGui::BeginMenu("File"))
			{
				if (ImGui::MenuItem("Reset")) 
				{
				
				}
				if (ImGui::MenuItem("Export"))
				{

				}
				ImGui::EndMenu();
			}
            ImGui::EndMenuBar();
        }
		if (ImGui::IsWindowFocused())
		{
			if (ImGui::IsMouseClicked(ImGuiMouseButton_Left)) 
			{
				hexLocations.push_back(ImVec2(io.MousePos.x, io.MousePos.y));
			}
		}
		ImDrawList* drawList = ImGui::GetWindowDrawList();
		for (unsigned int i = 0; i < hexLocations.size(); i++) 
		{
			drawList->AddNgonFilled(hexLocations[i], 30, ImColor(ImVec4(255, 0, 0, 255)), 6);
		}
		ImGui::End();


		ImGui::ShowDemoWindow();

		ImGui::Render();
		ImGui_ImplOpenGL3_RenderDrawData(ImGui::GetDrawData());
		if (ImGui::GetIO().ConfigFlags & ImGuiConfigFlags_ViewportsEnable)
		{
			GLFWwindow* backup_current_context = glfwGetCurrentContext();
			ImGui::UpdatePlatformWindows();
			ImGui::RenderPlatformWindowsDefault();
			glfwMakeContextCurrent(backup_current_context);
		}
	}

	void SetDimensions(int w, int h) 
	{
		m_width = w;
		m_height = h;
	}

	void CleanUp() 
	{
		ImGui_ImplOpenGL3_Shutdown();
		ImGui_ImplGlfw_Shutdown();
		ImGui::DestroyContext();
	}
};

#endif //EDITOR_H