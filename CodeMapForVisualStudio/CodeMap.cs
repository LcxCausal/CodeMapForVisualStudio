using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using EnvDTE;

namespace CodeMapForVisualStudio
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("d910937f-89b5-4633-b592-040b1ca35401")]
    [ProvideToolWindow(typeof(CodeMap), Style = VsDockStyle.Float)]
    public class CodeMap : ToolWindowPane
    {
        private WindowEvents windowEvents;
        private DocumentEvents documentEvents;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeMap"/> class.
        /// </summary>
        public CodeMap() : base(null)
        {
            Caption = "CodeMap";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            Content = new CodeMapControl();
        }

        public override void OnToolWindowCreated()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            base.OnToolWindowCreated();

            var codeMapPackage = Package as CodeMapPackage;

            // Bind window actived event.
            windowEvents = codeMapPackage.DTE.Events.WindowEvents;
            windowEvents.WindowActivated += WindowEvents_WindowActivated;

            // Bind document saved and opened events.
            documentEvents = codeMapPackage.DTE.Events.DocumentEvents;
            documentEvents.DocumentSaved += DocumentEvents_DocumentSaved;
            documentEvents.DocumentOpened += DocumentEvents_DocumentOpened;
        }

        private void DocumentEvents_DocumentOpened(Document Document)
        {
        }

        private void DocumentEvents_DocumentSaved(Document Document)
        {
        }

        private void WindowEvents_WindowActivated(Window GotFocus, Window LostFocus)
        {
        }
    }
}
