namespace MosaicImage
{
    partial class MosaicUI
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
            this.ComputeButton = new System.Windows.Forms.Button();
            this.ModelImageButton = new System.Windows.Forms.Button();
            this.SourceImgButton = new System.Windows.Forms.Button();
            this.ModelImgLabel = new System.Windows.Forms.Label();
            this.SourceImgLabel = new System.Windows.Forms.Label();
            this.ModelImagePB = new System.Windows.Forms.PictureBox();
            this.MosaicImagePB = new System.Windows.Forms.PictureBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.GridWidthTB = new System.Windows.Forms.TextBox();
            this.GridHeightTB = new System.Windows.Forms.TextBox();
            this.GridWidthLabel = new System.Windows.Forms.Label();
            this.GridHeightLabel = new System.Windows.Forms.Label();
            this.ProcessingProgressBar = new System.Windows.Forms.ProgressBar();
            this.ColourModeCB = new System.Windows.Forms.CheckBox();
            this.ProgressBarLabel = new System.Windows.Forms.Label();
            this.CancelProcessingButton = new System.Windows.Forms.Button();
            this.MosaicZoomTrackBar = new System.Windows.Forms.TrackBar();
            this.MosaicZoomLabel = new System.Windows.Forms.Label();
            this.ResetZoomButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ModelImagePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MosaicImagePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MosaicZoomTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // ComputeButton
            // 
            this.ComputeButton.Location = new System.Drawing.Point(61, 169);
            this.ComputeButton.Name = "ComputeButton";
            this.ComputeButton.Size = new System.Drawing.Size(75, 23);
            this.ComputeButton.TabIndex = 0;
            this.ComputeButton.Text = "Compute!";
            this.ComputeButton.UseVisualStyleBackColor = true;
            this.ComputeButton.Click += new System.EventHandler(this.ComputeButton_Click);
            // 
            // ModelImageButton
            // 
            this.ModelImageButton.Location = new System.Drawing.Point(27, 29);
            this.ModelImageButton.Name = "ModelImageButton";
            this.ModelImageButton.Size = new System.Drawing.Size(134, 23);
            this.ModelImageButton.TabIndex = 1;
            this.ModelImageButton.Text = "Select model image";
            this.ModelImageButton.UseVisualStyleBackColor = true;
            this.ModelImageButton.Click += new System.EventHandler(this.ModelImageButton_Click);
            // 
            // SourceImgButton
            // 
            this.SourceImgButton.Location = new System.Drawing.Point(27, 58);
            this.SourceImgButton.Name = "SourceImgButton";
            this.SourceImgButton.Size = new System.Drawing.Size(134, 23);
            this.SourceImgButton.TabIndex = 2;
            this.SourceImgButton.Text = "Select source images";
            this.SourceImgButton.UseVisualStyleBackColor = true;
            this.SourceImgButton.Click += new System.EventHandler(this.SourceImgButton_Click);
            // 
            // ModelImgLabel
            // 
            this.ModelImgLabel.AutoSize = true;
            this.ModelImgLabel.Location = new System.Drawing.Point(189, 34);
            this.ModelImgLabel.Name = "ModelImgLabel";
            this.ModelImgLabel.Size = new System.Drawing.Size(59, 13);
            this.ModelImgLabel.TabIndex = 3;
            this.ModelImgLabel.Text = "model path";
            // 
            // SourceImgLabel
            // 
            this.SourceImgLabel.AutoSize = true;
            this.SourceImgLabel.Location = new System.Drawing.Point(189, 63);
            this.SourceImgLabel.Name = "SourceImgLabel";
            this.SourceImgLabel.Size = new System.Drawing.Size(63, 13);
            this.SourceImgLabel.TabIndex = 4;
            this.SourceImgLabel.Text = "source path";
            // 
            // ModelImagePB
            // 
            this.ModelImagePB.Location = new System.Drawing.Point(295, 156);
            this.ModelImagePB.Name = "ModelImagePB";
            this.ModelImagePB.Size = new System.Drawing.Size(500, 500);
            this.ModelImagePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ModelImagePB.TabIndex = 5;
            this.ModelImagePB.TabStop = false;
            // 
            // MosaicImagePB
            // 
            this.MosaicImagePB.Location = new System.Drawing.Point(840, 156);
            this.MosaicImagePB.Name = "MosaicImagePB";
            this.MosaicImagePB.Size = new System.Drawing.Size(500, 500);
            this.MosaicImagePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MosaicImagePB.TabIndex = 6;
            this.MosaicImagePB.TabStop = false;
            this.MosaicImagePB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MosaicImagePB_MouseDown);
            this.MosaicImagePB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MosaicImagePB_MouseMove);
            this.MosaicImagePB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MosaicImagePB_MouseUp);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(49, 252);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(102, 23);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Save mosaic";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // GridWidthTB
            // 
            this.GridWidthTB.Location = new System.Drawing.Point(27, 94);
            this.GridWidthTB.Name = "GridWidthTB";
            this.GridWidthTB.Size = new System.Drawing.Size(134, 20);
            this.GridWidthTB.TabIndex = 8;
            this.GridWidthTB.TextChanged += new System.EventHandler(this.GridWidthTB_TextChanged);
            // 
            // GridHeightTB
            // 
            this.GridHeightTB.Location = new System.Drawing.Point(27, 120);
            this.GridHeightTB.Name = "GridHeightTB";
            this.GridHeightTB.Size = new System.Drawing.Size(134, 20);
            this.GridHeightTB.TabIndex = 9;
            this.GridHeightTB.TextChanged += new System.EventHandler(this.GridHeightTB_TextChanged);
            // 
            // GridWidthLabel
            // 
            this.GridWidthLabel.AutoSize = true;
            this.GridWidthLabel.Location = new System.Drawing.Point(189, 97);
            this.GridWidthLabel.Name = "GridWidthLabel";
            this.GridWidthLabel.Size = new System.Drawing.Size(54, 13);
            this.GridWidthLabel.TabIndex = 10;
            this.GridWidthLabel.Text = "Grid width";
            // 
            // GridHeightLabel
            // 
            this.GridHeightLabel.AutoSize = true;
            this.GridHeightLabel.Location = new System.Drawing.Point(189, 123);
            this.GridHeightLabel.Name = "GridHeightLabel";
            this.GridHeightLabel.Size = new System.Drawing.Size(58, 13);
            this.GridHeightLabel.TabIndex = 11;
            this.GridHeightLabel.Text = "Grid height";
            // 
            // ProcessingProgressBar
            // 
            this.ProcessingProgressBar.Location = new System.Drawing.Point(660, 706);
            this.ProcessingProgressBar.Name = "ProcessingProgressBar";
            this.ProcessingProgressBar.Size = new System.Drawing.Size(313, 23);
            this.ProcessingProgressBar.TabIndex = 12;
            // 
            // ColourModeCB
            // 
            this.ColourModeCB.AutoSize = true;
            this.ColourModeCB.Location = new System.Drawing.Point(170, 173);
            this.ColourModeCB.Name = "ColourModeCB";
            this.ColourModeCB.Size = new System.Drawing.Size(104, 17);
            this.ColourModeCB.TabIndex = 14;
            this.ColourModeCB.Text = "Coloured mosaic";
            this.ColourModeCB.UseVisualStyleBackColor = true;
            this.ColourModeCB.CheckedChanged += new System.EventHandler(this.ColourModeCB_CheckedChanged);
            // 
            // ProgressBarLabel
            // 
            this.ProgressBarLabel.AutoSize = true;
            this.ProgressBarLabel.Location = new System.Drawing.Point(772, 673);
            this.ProgressBarLabel.Name = "ProgressBarLabel";
            this.ProgressBarLabel.Size = new System.Drawing.Size(85, 13);
            this.ProgressBarLabel.TabIndex = 15;
            this.ProgressBarLabel.Text = "progress bar text";
            // 
            // CancelProcessingButton
            // 
            this.CancelProcessingButton.Location = new System.Drawing.Point(61, 210);
            this.CancelProcessingButton.Name = "CancelProcessingButton";
            this.CancelProcessingButton.Size = new System.Drawing.Size(75, 23);
            this.CancelProcessingButton.TabIndex = 16;
            this.CancelProcessingButton.Text = "Cancel";
            this.CancelProcessingButton.UseVisualStyleBackColor = true;
            this.CancelProcessingButton.Click += new System.EventHandler(this.CancelProcessingButton_Click);
            // 
            // MosaicZoomTrackBar
            // 
            this.MosaicZoomTrackBar.Location = new System.Drawing.Point(859, 94);
            this.MosaicZoomTrackBar.Maximum = 400;
            this.MosaicZoomTrackBar.Minimum = 10;
            this.MosaicZoomTrackBar.Name = "MosaicZoomTrackBar";
            this.MosaicZoomTrackBar.Size = new System.Drawing.Size(348, 45);
            this.MosaicZoomTrackBar.TabIndex = 17;
            this.MosaicZoomTrackBar.Value = 100;
            this.MosaicZoomTrackBar.Scroll += new System.EventHandler(this.MosaicZoomTrackBar_Scroll);
            // 
            // MosaicZoomLabel
            // 
            this.MosaicZoomLabel.AutoSize = true;
            this.MosaicZoomLabel.Location = new System.Drawing.Point(1024, 68);
            this.MosaicZoomLabel.Name = "MosaicZoomLabel";
            this.MosaicZoomLabel.Size = new System.Drawing.Size(70, 13);
            this.MosaicZoomLabel.TabIndex = 18;
            this.MosaicZoomLabel.Text = "zoom amount";
            // 
            // ResetZoomButton
            // 
            this.ResetZoomButton.Location = new System.Drawing.Point(1236, 97);
            this.ResetZoomButton.Name = "ResetZoomButton";
            this.ResetZoomButton.Size = new System.Drawing.Size(75, 23);
            this.ResetZoomButton.TabIndex = 19;
            this.ResetZoomButton.Text = "Reset zoom";
            this.ResetZoomButton.UseVisualStyleBackColor = true;
            this.ResetZoomButton.Click += new System.EventHandler(this.ResetZoomButton_Click);
            // 
            // MosaicUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1529, 741);
            this.Controls.Add(this.ResetZoomButton);
            this.Controls.Add(this.MosaicZoomLabel);
            this.Controls.Add(this.MosaicZoomTrackBar);
            this.Controls.Add(this.CancelProcessingButton);
            this.Controls.Add(this.ProgressBarLabel);
            this.Controls.Add(this.ColourModeCB);
            this.Controls.Add(this.ProcessingProgressBar);
            this.Controls.Add(this.GridHeightLabel);
            this.Controls.Add(this.GridWidthLabel);
            this.Controls.Add(this.GridHeightTB);
            this.Controls.Add(this.GridWidthTB);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.MosaicImagePB);
            this.Controls.Add(this.ModelImagePB);
            this.Controls.Add(this.SourceImgLabel);
            this.Controls.Add(this.ModelImgLabel);
            this.Controls.Add(this.SourceImgButton);
            this.Controls.Add(this.ModelImageButton);
            this.Controls.Add(this.ComputeButton);
            this.Name = "MosaicUI";
            this.Text = "Mosaic UI";
            ((System.ComponentModel.ISupportInitialize)(this.ModelImagePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MosaicImagePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MosaicZoomTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ComputeButton;
        private System.Windows.Forms.Button ModelImageButton;
        private System.Windows.Forms.Button SourceImgButton;
        private System.Windows.Forms.Label ModelImgLabel;
        private System.Windows.Forms.Label SourceImgLabel;
        private System.Windows.Forms.PictureBox ModelImagePB;
        private System.Windows.Forms.PictureBox MosaicImagePB;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox GridWidthTB;
        private System.Windows.Forms.TextBox GridHeightTB;
        private System.Windows.Forms.Label GridWidthLabel;
        private System.Windows.Forms.Label GridHeightLabel;
        private System.Windows.Forms.ProgressBar ProcessingProgressBar;
        private System.Windows.Forms.CheckBox ColourModeCB;
        private System.Windows.Forms.Label ProgressBarLabel;
        private System.Windows.Forms.Button CancelProcessingButton;
        private System.Windows.Forms.TrackBar MosaicZoomTrackBar;
        private System.Windows.Forms.Label MosaicZoomLabel;
        private System.Windows.Forms.Button ResetZoomButton;
    }
}

