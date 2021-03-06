﻿import System.Diagnostics;
 
static function shellp(filename : String, arguments : String) : Process  {
    var p = new Process();
    p.StartInfo.Arguments = arguments;
    p.StartInfo.CreateNoWindow = true;
    p.StartInfo.UseShellExecute = false;
    p.StartInfo.RedirectStandardOutput = true;
    p.StartInfo.RedirectStandardInput = true;
    p.StartInfo.RedirectStandardError = true;
    p.StartInfo.FileName = filename;
    p.Start();
    return p;
}
 
static function shell( filename : String, arguments : String) : String {
    var p = shellp(filename, arguments);
    var output = p.StandardOutput.ReadToEnd();
    p.WaitForExit();
    return output;
}