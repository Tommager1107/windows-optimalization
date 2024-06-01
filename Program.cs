using System;
using System.Diagnostics;

namespace WindowsOptimizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== Windows 10 Optimization Software ==========");
            Console.WriteLine("Autor: Tommager");
            Console.WriteLine("Verzia: 1.0.0");
            Console.WriteLine("Popis: Tento program vykonáva základné optimalizačné úkony pre Windows 10.");
            Console.WriteLine("TENTO SOFTWARE NIE JE NIJAKO SPOJENÝ S WINDOWSOM A MICROSOFTOM");
            Console.WriteLine("ZA ŽIADNE STRATY DÁT PRÍPADNE POŠKODENIE PC NEZODPOVEDÁME");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Pre začatie procesu optimalizácie stlačte Enter...");
            Console.ReadLine();

            Console.WriteLine("Starting optimization process...");

            // Stop and disable Windows Search Indexing
            ExecuteCommand("sc stop \"WSearch\"", "Stopping Windows Search Indexing...");
            ExecuteCommand("sc config \"WSearch\" start= disabled", "Disabling Windows Search Indexing...");

           

            // Clean temporary files
            ExecuteCommand("del /q/f/s %TEMP%\\*", "Cleaning Temporary Files...");
            ExecuteCommand("del /q/f/s C:\\Windows\\Temp\\*", "Cleaning Windows Temporary Files...");

            // Disk Cleanup
            ExecuteCommand("cleanmgr /sagerun:1", "Running Disk Cleanup...");

            // Optimize and defragment the disk
            ExecuteCommand("defrag C: /O", "Optimizing and defragmenting the disk...");

            // Disable unnecessary startup services (Example services)
            ExecuteCommand("sc config \"DiagTrack\" start= disabled", "Disabling Diagnostics Tracking Service...");
            ExecuteCommand("sc config \"dmwappushservice\" start= disabled", "Disabling dmwappushservice...");

            // System File Checker (SFC)
            ExecuteCommand("sfc /scannow", "Running System File Checker...");

            // Check Disk (CHKDSK)
            ExecuteCommand("chkdsk /f", "Running Check Disk...");

            // Scan drivers
            Console.WriteLine("Skenovanie ovládačov...");
            ScanDrivers();

            Console.WriteLine("Optimization process completed!");
            Console.WriteLine("Software bol optimalizovaný. Stlačte Enter pre ukončenie programu...");
            Console.ReadLine();
        }

        static void ExecuteCommand(string command, string description)
        {
            try
            {
                Console.WriteLine(description);
                ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.CreateNoWindow = true;

                using (Process process = new Process())
                {
                    process.StartInfo = processStartInfo;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    Console.WriteLine(output);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing command '{command}': {ex.Message}");
            }
        }

        static void ScanDrivers()
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo("powershell.exe", "/c Get-WmiObject Win32_PnPSignedDriver | Select-Object DeviceName, DriverVersion, DriverDate");
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.CreateNoWindow = true;

                using (Process process = new Process())
                {
                    process.StartInfo = processStartInfo;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    Console.WriteLine("Driver Scan Results:");
                    Console.WriteLine(output);
                    Console.WriteLine("Ak sú niektoré ovládače zastaralé, odporúčame ich aktualizovať pomocou Windows Update alebo stiahnuť najnovšie verzie z webových stránok výrobcu.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing PowerShell command: {ex.Message}");
            }
        }
    }
}
