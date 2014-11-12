using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using EnvDTE80;

namespace jweg.CMakeTouch
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidCMakeTouchPkgString)]
    public sealed class CMakeTouchPackage : Package
    {
        private DTE2 _ide;

        public CMakeTouchPackage()
        {}
        

        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if ( null != mcs )
            {
                MenuCommand menuItem = new MenuCommand(MenuItemCallback,
                    new CommandID(GuidList.guidCMakeTouchCmdSet, (int)PkgCmdIDList.cmdidCMakeTouchProject));
                mcs.AddCommand( menuItem );

                menuItem = new MenuCommand(MenuItemCallback,
                    new CommandID(GuidList.guidCMakeTouchCmdSet, (int)PkgCmdIDList.cmdidCMakeTouchNode));
                mcs.AddCommand(menuItem);
                
                menuItem = new MenuCommand(MenuItemCallback,
                    new CommandID(GuidList.guidCMakeTouchCmdSet, (int)PkgCmdIDList.cmdidCMakeTouchFolder));
                mcs.AddCommand(menuItem);
            }
        }


        private void TouchFile(string fileName)
        {
            try
            {
                System.IO.File.SetLastWriteTimeUtc(fileName, DateTime.UtcNow);
            }
            catch (System.Exception)
            {}
        }


        private void ProcessProjectItem(ProjectItem item, bool touchAnyFile)
        {
            if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFile)
            {
                string fileName = item.get_FileNames(0);
                if (touchAnyFile || fileName.EndsWith("CMakeLists.txt"))
                {
                    TouchFile(fileName);
                }
            }
            else if (item.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder ||
                     item.Kind == EnvDTE.Constants.vsProjectItemKindVirtualFolder)
            {
                // Recurse to children
                foreach (ProjectItem child in item.ProjectItems) ProcessProjectItem(child, touchAnyFile);
            }
        }


        private void MenuItemCallback(object sender, EventArgs e)
        {
            MenuCommand command = sender as MenuCommand;
            bool touchAnyFile = PkgCmdIDList.cmdidCMakeTouchNode == command.CommandID.ID;
            
            if (null == _ide) _ide = (DTE2)GetService(typeof(DTE));

            IEnumerable<UIHierarchyItem> items = ((object[])_ide.ToolWindows.SolutionExplorer.SelectedItems).Cast<UIHierarchyItem>().ToList();
            foreach (UIHierarchyItem item in items)
            {
                Project project = item.Object as Project;
                if (null != project)
                {
                    foreach (ProjectItem pItem in project.ProjectItems) ProcessProjectItem(pItem, touchAnyFile);
                }

                ProjectItem projItem = item.Object as ProjectItem;
                if (null != projItem)
                {
                    ProcessProjectItem(projItem, touchAnyFile);
                }          
            }
        }

    }
}
