﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dzakuma.MicroserviceMockup.UI.EmployeeDashboard
{
	public class InterprocessDataSiphon
	{
		private static Logger _internalLogger = LogManager.GetCurrentClassLogger();

		public (JObject individualData, JArray personnelList) DeserializeJsonString(string executablePath, params string[] arguments)
		{
			var data = DecodeProcessString(executablePath, arguments);

			return (SafeDeserializeJsonObject(data), SafeDeserializeJsonArray(data));
		}

		public JObject SafeDeserializeJsonObject(string jsonString)
		{
			try
			{
				return JObject.Parse(jsonString);
			}
			catch (Exception anomaly)
			{
				return null;
			}
		}

		public JArray SafeDeserializeJsonArray(string jsonString)
		{
			try
			{
				return JArray.Parse(jsonString);
			}
			catch (Exception anomaly)
			{
				return null;
			}
		}

		public string DecodeProcessString(string executablePath, params string[] arguments)
		{
			return TransportSafeString.Decode(ReadDataFromProcess(executablePath, arguments));
		}

		public string ReadDataFromProcess(string executablePath, params string[] arguments)
		{
			var testProgram = new Process();
			testProgram.StartInfo.FileName = executablePath;
			testProgram.StartInfo.Arguments = string.Join(" ", arguments);
			testProgram.StartInfo.CreateNoWindow = true;
			testProgram.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			testProgram.StartInfo.UseShellExecute = false;
			testProgram.StartInfo.RedirectStandardError = true;
			testProgram.Start();
			testProgram.WaitForExit(6000);

			var standardErrorOutput = testProgram.StandardError.ReadToEnd().Trim();

			if (testProgram.ExitCode != 0)
			{
				_internalLogger.Warn($"Call to [{executablePath}] did not complete cleanly. Exit Code: [{testProgram.ExitCode}]");
			}

			return standardErrorOutput;
		}
	}
}