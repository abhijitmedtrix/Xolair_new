using UnityEditor;
using UnityEngine;

class ToggleSymbols {

	private static string s_ReleaseSymbol      			= "FINAL_BUILD";

	const int priority 									= 1200;
	const int indent									= 11;
	
	private static string ClearSymbol (string symbols, string symbolName)
	{
		if (symbols.Contains(symbolName + ";")) {
			symbols = symbols.Replace(symbolName + ";", "");
		} else if (symbols.Contains(symbolName)) {
			symbols = symbols.Replace(symbolName, "");
		}
		return symbols;
	}

	/// <summary>
	/// Toggles the platform dependent multiplayer compiler symbole.
	/// </summary>
	[MenuItem("Tools/Build symbols/Enable FINAL_BUILD", false, priority)]
	public static void EnableReleaseBuildSymbol ()
	{
		string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

		// if didn't exist
		if (symbols == ClearSymbol(symbols, s_ReleaseSymbol)) {
			// add symbol
			symbols += (";" + s_ReleaseSymbol);
		}	
		PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, symbols);
		AssetDatabase.Refresh();

		PlayerSettings.fullScreenMode = FullScreenMode.FullScreenWindow;
	}

	[MenuItem("Tools/Build symbols/Disable FINAL_BUILD", false, priority)]
	public static void DisableReleaseBuildSymbol ()
	{
		string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

		// if didn't exist
		symbols	= ClearSymbol(symbols, s_ReleaseSymbol);
		PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, symbols);
		AssetDatabase.Refresh();

		PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
	}
}
