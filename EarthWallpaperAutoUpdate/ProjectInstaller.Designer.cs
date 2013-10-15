namespace EarthWallpaperAutoUpdate
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EarthWallpaperProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.EarthWallpaperInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // EarthWallpaperProcessInstaller
            // 
            this.EarthWallpaperProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.EarthWallpaperProcessInstaller.Password = null;
            this.EarthWallpaperProcessInstaller.Username = null;
            this.EarthWallpaperProcessInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceProcessInstaller1_AfterInstall);
            // 
            // EarthWallpaperInstaller
            // 
            this.EarthWallpaperInstaller.ServiceName = "EarthWallpaperAutoUpdate";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.EarthWallpaperProcessInstaller,
            this.EarthWallpaperInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller EarthWallpaperProcessInstaller;
        private System.ServiceProcess.ServiceInstaller EarthWallpaperInstaller;
    }
}