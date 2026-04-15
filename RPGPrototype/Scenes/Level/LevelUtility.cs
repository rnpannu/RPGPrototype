using System;
using System.IO;
using System.Runtime.InteropServices;
using RPGPrototype.Log;

namespace RPGPrototype.Scenes;

public static class LevelUtility
{
	public static int[,] LoadIntGrid(string path, string level)
	{
		if (Path.GetExtension(path) != ".csv")
		{
			throw new ArgumentException("Only .csv files are accepted");
		}

		string basePath = "";
		if (RuntimeInformation.IsOSPlatform((OSPlatform.Windows)))
		{
			basePath = "../../../Content/maps/Map/simplified/" + level + "/";
		}
		else if (RuntimeInformation.IsOSPlatform((OSPlatform.Linux)))
		{
			basePath = Path.Combine("Content", "Tilemaps", "City", "simplified", "Level_0");
		}

		string file = Path.Combine(basePath, path);

		if (!File.Exists(file))
		{
			throw new FileNotFoundException("Failed to load intgrid at: " + file);
		}
		
		string[] lines = File.ReadAllLines(file);
		int mapHeight = lines.Length;
		int[,] intGrid = null;
		
		for (int i = 0; i < mapHeight; i++)
		{
			string[] rowData = lines[i].Split(",", StringSplitOptions.RemoveEmptyEntries);
			if (intGrid == null)
			{
				intGrid = new int[mapHeight, rowData.Length];
			}
			for (int j = 0; j < rowData.Length; j++)
			{
				if (int.TryParse(rowData[j].Trim(), out int result)) // pass by reference
				{
					intGrid[i, j] = result;
				}
				else
				{
					Logger.Log($"Non integer entry at [{i},{j}]. Value: '[{@rowData[j]}]'" + " in file: " +
					           file, LogLevel.Error);
				}
			}
		}

		return intGrid;
	}
}