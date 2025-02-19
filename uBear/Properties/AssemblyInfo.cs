﻿using System.Reflection;
using System.Runtime.InteropServices;


[assembly: AssemblyTitle("uBear")]

#if IS_DIVENET

[assembly: AssemblyDescription("DiveNET: Commander - S - USBL Tracking system host application.\n\n" +
                               "Latest release: https://github.com/ucnl/uBear/releases/download/1.0-divenet/uBear_DiveNET.zip \n" +
                               "Support: mailto:support@divenetgps.com\n\n" +
                               "More information about Commander-S USBL:\n" +
                               "https://docs.unavlab.com/\n\n" +                               
                               "Project source code:\n" +
                               "https://github.com/ucnl/uBear\n\n" +
                               "This application is 100% open-source, and based on our projects:\n" +                               
                               "https://github.com/ucnl/UCNLNav\n" +
                               "https://github.com/ucnl/UCNLPhysics\n" +
                               "https://github.com/ucnl/UCNLNMEA\n" +
                               "https://github.com/ucnl/UCNLKML\n" +
                               "https://github.com/ucnl/UCNLDrivers\n" +
                               "https://github.com/ucnl/UCNLUI"
                               )]

[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Beringia enterpises")]
[assembly: AssemblyProduct("uBear")]
[assembly: AssemblyCopyright("Copyright © Beringia enterprises, 2021")]
[assembly: AssemblyTrademark("DiveNET")]
[assembly: AssemblyCulture("")]

#else

[assembly: AssemblyDescription("🐻 uBear - uWave USBL host application.\n\n" +
                               "Latest release: https://api.github.com/repos/ucnl/uBear/zipball \n" +
                               "Support: mailto:support@unavlab.com\n\n" +
                               "More information about uWave:\n" +
                               "https://docs.unavlab.com/navigation_and_tracking_systems_en.html#uwave-usbl\n\n" +
                               "Project source code:\n" +
                               "https://github.com/ucnl/uBear\n\n" +
                               "This application is 100% open-source, and based on our projects:\n" +
                               "https://github.com/ucnl/UCNLNav\n" +
                               "https://github.com/ucnl/UCNLPhysics\n" +
                               "https://github.com/ucnl/UCNLNMEA\n" +
                               "https://github.com/ucnl/UCNLKML\n" +
                               "https://github.com/ucnl/UCNLDrivers\n" +
                               "https://github.com/ucnl/UCNLUI"
                               )]

[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Underwater communication & navigation laboratory, LLC")]
[assembly: AssemblyProduct("uBear")]
[assembly: AssemblyCopyright("Copyright © Underwater communication & navigation laboratory, 2022-2025")]
[assembly: AssemblyTrademark("UC&NL")]
[assembly: AssemblyCulture("")]

#endif

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("72503d5d-3aeb-44a5-bcdc-81bce06e25bb")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.1.*")]
[assembly: AssemblyFileVersion("1.0.0.0")]
