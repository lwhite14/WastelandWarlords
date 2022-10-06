#ifndef EDITOR_H
#define EDITOR_H

#include <vector>
#include <iostream>
#include "ImGuiUtils.h"
#include "Runner.h"
#include "HexCalc.h"
#include "Brush.h"
#include "HexRep.h"
#include "UniversalHexSize.h"

class Editor 
{
private:
	ImGuiViewport*				viewport;
	ImGuiWindowFlags			m_mainWindowFlags;
	ImGuiWindowFlags			m_childWindowFlags;
	ImGuiDockNodeFlags			m_dockspaceFlags;
	int							m_width;
	int							m_height;
	std::vector<HexRep>			hexCells;
	std::vector<Brush>			brushTypes = 
								{
									Brush("Default", ImColor(ImVec4(1, 1, 1, 1))),
									Brush("Water", ImColor(ImVec4(0, 0, 1, 1))),
									Brush("WaterShallow", ImColor(ImVec4(0.2, 0.2, 1, 1))),
									Brush("Plains", ImColor(ImVec4(0.75, 1, 0.75, 1))),
									Brush("Forest", ImColor(ImVec4(0, 1, 0, 1))),
									Brush("ImpactSite", ImColor(ImVec4(0.2, 0.2, 0.2, 1)))
								};
	int							rows;
	int							cols;

public:
	Editor() 
	{
		m_mainWindowFlags = ImGuiWindowFlags_None;
		m_childWindowFlags = ImGuiWindowFlags_None;
		m_dockspaceFlags = ImGuiWindowFlags_None;
	}

	void Init(GLFWwindow* window)
	{
		IMGUI_CHECKVERSION();
		ImGui::CreateContext();
		ImGui::StyleColorsClassic();
		ImGui_ImplGlfw_InitForOpenGL(window, true);
		ImGui_ImplOpenGL3_Init("#version 460");

		viewport = ImGui::GetMainViewport();

		ImGuiIO* io = &ImGui::GetIO();
		io->ConfigFlags |= ImGuiConfigFlags_DockingEnable;

		ImGui::SetUpStyle();

		m_mainWindowFlags |= ImGuiWindowFlags_NoResize;
		m_mainWindowFlags |= ImGuiWindowFlags_NoTitleBar;
		//m_mainWindowFlags |= ImGuiWindowFlags_NoScrollbar;
		m_mainWindowFlags |= ImGuiWindowFlags_MenuBar;
		m_mainWindowFlags |= ImGuiWindowFlags_NoMove;
		m_mainWindowFlags |= ImGuiWindowFlags_NoCollapse;
		//m_mainWindowFlags |= ImGuiWindowFlags_NoNav;
		//m_mainWindowFlags |= ImGuiWindowFlags_NoBackground;
		m_mainWindowFlags |= ImGuiWindowFlags_NoBringToFrontOnFocus;
		m_mainWindowFlags |= ImGuiWindowFlags_NoDocking;
		//m_mainWindowFlags |= ImGuiWindowFlags_UnsavedDocument;
		m_mainWindowFlags |= ImGuiWindowFlags_NoNavFocus;

		m_dockspaceFlags = ImGuiDockNodeFlags_None;

		m_childWindowFlags = ImGuiModFlags_None;
		//m_childWindowFlags |= ImGuiWindowFlags_NoResize;
		//m_childWindowFlags |= ImGuiWindowFlags_NoTitleBar;
		//m_childWindowFlags |= ImGuiWindowFlags_NoScrollbar;
		//m_childWindowFlags |= ImGuiWindowFlags_MenuBar;
		//m_childWindowFlags |= ImGuiWindowFlags_NoMove;
		m_childWindowFlags |= ImGuiWindowFlags_NoCollapse;
		//m_childWindowFlags |= ImGuiWindowFlags_NoNav;
		//m_childWindowFlags |= ImGuiWindowFlags_NoBackground;
		//m_childWindowFlags |= ImGuiWindowFlags_NoBringToFrontOnFocus;
		//m_childWindowFlags |= ImGuiWindowFlags_NoDocking;
		//m_childWindowFlags |= ImGuiWindowFlags_UnsavedDocument;

		cols = m_width / HexDiameter;
		rows = m_height / HexDiameter;
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

		ImGui::SetNextWindowPos(viewport->WorkPos);
		ImGui::SetNextWindowSize(viewport->WorkSize);
		ImGui::SetNextWindowViewport(viewport->ID);

		ImGui::Begin("Workspace", NULL, m_mainWindowFlags);
		ImGuiID dockspace_id = ImGui::GetID("DockSpace");
		ImGui::DockSpace(dockspace_id, ImVec2(0.0f, 0.0f), m_dockspaceFlags);
		if (ImGui::BeginMenuBar())
		{
			if (ImGui::BeginMenu("File"))
			{
				if (ImGui::MenuItem("Reset"))
				{
					hexCells = std::vector<HexRep>();
				}
				if (ImGui::MenuItem("Export"))
				{

				}
				ImGui::EndMenu();
			}
			ImGui::EndMenuBar();
		}
		ImGui::End();

		static int brushSelected = 0;

		ImGui::Begin("Editor", (bool*)0, m_childWindowFlags);
		if (ImGui::IsWindowHovered())
		{
			if (ImGui::IsMouseClicked(ImGuiMouseButton_Left)) 
			{
				ImVec2 hexPosProper = HexCalc::CalcHexPos(ImVec2(io.MousePos.x, io.MousePos.y));
				bool alreadyExists = false;
				for (unsigned int i = 0; i < hexCells.size(); i++)
				{
					if ((hexPosProper.x == hexCells[i].x) && (hexPosProper.y == hexCells[i].y))
					{
						alreadyExists = true;
					}
				}
				if (!alreadyExists) { hexCells.push_back(HexRep(hexPosProper.x, hexPosProper.y, brushTypes[brushSelected].name, brushTypes[brushSelected].colour)); }
			}
			else if (ImGui::IsMouseClicked(ImGuiMouseButton_Right)) 
			{
				ImVec2 hexPosProper = HexCalc::CalcHexPos(ImVec2(io.MousePos.x, io.MousePos.y));
				int index = -1;
				for (unsigned int i = 0; i < hexCells.size(); i++)
				{
					if ((hexPosProper.x == hexCells[i].x) && (hexPosProper.y == hexCells[i].y))
					{
						index = i;
					}
				}
				if (index != -1)
				{
					hexCells.erase(hexCells.begin() + index);
				}		
			}
		}
		ImDrawList* drawList = ImGui::GetWindowDrawList();
		for (unsigned int i = 0; i < hexCells.size(); i++)
		{
			drawList->AddNgonFilled(ImVec2(hexCells[i].x, hexCells[i].y), HexDiameter / 2, hexCells[i].colour, 8);
		}
		drawList->AddCircleFilled(HexCalc::CalcHexPos(ImVec2(io.MousePos.x, io.MousePos.y)), 2.5f, ImColor(ImVec4(1, 0, 0, 1)), 8);
		for (unsigned int x = 0; x < cols; x++) 
		{
			for (unsigned int y = 0; y < rows; y++) 
			{	
				ImVec2 center = ImVec2((x * HexDiameter), (y * HexDiameter));
				center = HexCalc::CalcHexPos(center);
				drawList->AddRect(ImVec2(center.x - HexDiameter / 2, center.y - HexDiameter / 2), ImVec2(center.x + HexDiameter / 2, center.y + HexDiameter / 2), ImColor(ImVec4(1.0f, 1.0f, 1.0f, 0.25f)), 0.0f, ImDrawFlags_None);
			}
		}
		ImGui::End();

		ImVec2 size = ImVec2(225, 100);
		ImGui::SetNextWindowPos(ImVec2(m_width - size.x , 20), ImGuiCond_FirstUseEver);
		ImGui::SetNextWindowSize(size, ImGuiCond_FirstUseEver);
		ImGui::SetNextWindowViewport(viewport->ID);
		ImGui::Begin("Brush", (bool*)0, m_childWindowFlags);
		if (ImGui::BeginCombo("Brush", brushTypes[brushSelected].name.c_str()))
		{
			for (int i = 0; i < brushTypes.size(); i++)
			{
				if (ImGui::Selectable(brushTypes[i].name.c_str()))
				{
					brushSelected = i;
				}
			}
			ImGui::EndCombo();
		}
		ImGui::End();

		//ImGui::ShowDemoWindow();

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