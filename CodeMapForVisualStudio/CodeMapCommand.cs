using System;
using System.ComponentModel.Design;
using System.Drawing;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace CodeMapForVisualStudio
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CodeMapCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int OpenCommandId = 0x0100;
        public const int SettingsCommandId = 0x0101;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("88334d34-89ef-420d-80cd-9b81ebd1a414");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        private CodeMap codeMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeMapCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private CodeMapCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            codeMap = null;
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var openCommandId = new CommandID(CommandSet, OpenCommandId);
            var openMenuItem = new MenuCommand(Open, openCommandId);
            commandService.AddCommand(openMenuItem);

            var settingsCommandID = new CommandID(CommandSet, SettingsCommandId);
            var settingsMenuItem = new MenuCommand(ChangeSettings, settingsCommandID);
            commandService.AddCommand(settingsMenuItem);
        }

        internal CodeMap CodeMap => codeMap;

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CodeMapCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in CodeMapCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            var commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new CodeMapCommand(package, commandService);
        }

        /// <summary>
        /// Shows the tool window when the menu item is clicked.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void Open(object sender, EventArgs e)
        {
            LoadCodeMapWindow();
        }

        /// <summary>
        /// Change Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeSettings(object sender, EventArgs e)
        {
            if (codeMap == null && package != null)
                LoadCodeMapWindow();

            using (var settingsForm = new SettingsForm(codeMap))
                settingsForm.ShowDialog();
        }

        private void LoadCodeMapWindow()
        {
            package.JoinableTaskFactory.RunAsync(async delegate
            {
                var window = await package.ShowToolWindowAsync(typeof(CodeMap), 0, true, package.DisposalToken);

                if ((null == window) || (null == window.Frame))
                    throw new NotSupportedException("Cannot create tool window");
                else
                    codeMap = window as CodeMap;
            });
        }
    }
}
