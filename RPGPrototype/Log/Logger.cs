using System;
using System.IO;

namespace RPGPrototype.Log;

public static class Logger
{
	private static readonly string logFilePath = "../../../Log/log.txt";

	public static void Log(string message, LogLevel logLevel)
	{
		string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{logLevel.ToString().ToUpper()}] {message}";
		File.AppendAllText(logFilePath, entry + Environment.NewLine);
	}

}