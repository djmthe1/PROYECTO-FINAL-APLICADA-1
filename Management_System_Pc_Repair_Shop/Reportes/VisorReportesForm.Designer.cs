﻿namespace Management_System_Pc_Repair_Shop.Reportes
{
    partial class VisorReportesForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisorReportesForm));
            this.visorReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // visorReportViewer
            // 
            this.visorReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visorReportViewer.Location = new System.Drawing.Point(0, 0);
            this.visorReportViewer.Name = "visorReportViewer";
            this.visorReportViewer.Size = new System.Drawing.Size(728, 432);
            this.visorReportViewer.TabIndex = 0;
            // 
            // VisorReportesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 432);
            this.Controls.Add(this.visorReportViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VisorReportesForm";
            this.Text = "Reporte";
            this.Load += new System.EventHandler(this.VisorReportesForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer visorReportViewer;
    }
}