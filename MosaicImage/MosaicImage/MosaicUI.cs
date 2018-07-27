using ImageProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MosaicImage
{
    public partial class MosaicUI : Form
    {
        private string _source_img_folder;
        private string _model_image_path;
        private int _mosaic_grid_width;
        private int _mosaic_grid_height;
        private MosaicImageProcessing _image_processing;
        private BackgroundWorker _bw_processing;
        private bool _coloured_mosaic_mode;
        private int _mosaic_zoom_value;
        private Bitmap _mosaic_image;
        private Bitmap _model_image;

        public MosaicUI()
        {
            InitializeComponent();

            _bw_processing = new BackgroundWorker();
            _bw_processing.DoWork += _bw_processing_DoWork;
            _bw_processing.RunWorkerCompleted += _bw_processing_RunWorkerCompleted;

            ProcessingProgressBar.Minimum = 0;
            ProcessingProgressBar.Maximum = 100;
            ProcessingProgressBar.Visible = false;
            ProgressBarLabel.Visible = false;

            _image_processing = new ImageProcessing.MosaicImageProcessing();
            _image_processing.ProcessingProgressChangedEvent += _image_processing_ProcessingProgressChangedEvent;

            MosaicZoomLabel.Text = "100 %";
            _mosaic_zoom_value = 100;
            //_mosaic_image = AForge.Imaging.Image.FromFile(@"C:\Users\karviatr\source\repos\MosaicImage\MosaicImage\bin\Debug\combined_image.jpg");
            //MosaicImagePB.Image = (Bitmap)_mosaic_image.Clone();

            try
            {
                LoadPreviousSettings();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not load settings: {ex.Message}");
            }
        }

        #region Load and save

        private void LoadPreviousSettings()
        {
            string[] lines = File.ReadAllLines(@"Settings\FileSettings.txt");

            try
            {
                _model_image_path = lines[0];
                _source_img_folder = lines[1];
                _mosaic_grid_width = int.Parse(lines[2]);
                _mosaic_grid_height = int.Parse(lines[3]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Missing some load settings: {ex.Message}");
            }

            ModelImgLabel.Text = _model_image_path;
            SourceImgLabel.Text = _source_img_folder;
            GridWidthTB.Text = _mosaic_grid_width.ToString();
            GridHeightTB.Text = _mosaic_grid_height.ToString();
            LoadModelImage();
        }

        private void SaveSettings()
        {
            string[] settings_string = new string[4];
            settings_string[0] = _model_image_path;
            settings_string[1] = _source_img_folder;
            settings_string[2] = _mosaic_grid_width.ToString();
            settings_string[3] = _mosaic_grid_height.ToString();
            try
            {
                File.WriteAllLines(@"Settings\FileSettings.txt", settings_string);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing settings: {ex.Message}");
            }
        }

        private void LoadModelImage()
        {
            try
            {
                Bitmap _model_image = AForge.Imaging.Image.FromFile(_model_image_path);
                try
                {
                    ModelImagePB.Image.Dispose();
                }
                catch (Exception) { }
                ModelImagePB.Image = _model_image;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading model image: {ex.Message}");
            }
        }

        #endregion

        #region Image processing functions

        private void _bw_processing_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap mosaic_image = null;
            try
            {
                UserInputFeasibilityTest();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Input for image processing was not feasible: {ex.Message}");
            }
            try
            {
                mosaic_image = _image_processing.CreateMosaic(_mosaic_grid_width, _mosaic_grid_height, _source_img_folder, (Bitmap)ModelImagePB.Image, _coloured_mosaic_mode);
            }
            catch (Exception) { }
            e.Result = mosaic_image;
        }

        private void UserInputFeasibilityTest()
        {
            if (_mosaic_grid_height <= 0 || _mosaic_grid_width <= 0)
                throw new Exception("Invalid grid width or height.");

            if (!Directory.Exists(_source_img_folder))
                throw new Exception($"Directory '{_source_img_folder}' could not be found.");


            string[] source_img_files = Directory.GetFiles(_source_img_folder);
            int required_different_imgs = _mosaic_grid_width * _mosaic_grid_height;
            int source_img_count = 0;
            foreach (string source_img_file in source_img_files)
            {
                bool isImage = _image_processing.IsImage(source_img_file);
                if (isImage)
                    source_img_count++;
            }

            if (source_img_count < required_different_imgs)
                throw new Exception($"Requested mosaic requires {_mosaic_grid_height} x {_mosaic_grid_width} = {required_different_imgs} " +
                                    $"different images but only found {source_img_count} at '{_source_img_folder}'");

        }




        private void _bw_processing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressBarLabel.Visible = false;
            ProcessingProgressBar.Visible = false;
            _image_processing.CancelProcessing = false;
            if (e.Result == null)
                return;

            _mosaic_image = (Bitmap)e.Result;
            try
            {
                MosaicImagePB.Image.Dispose();
            }
            catch (Exception) { }
            MosaicImagePB.Image = (Bitmap)_mosaic_image.Clone();
        }

        private void _image_processing_ProcessingProgressChangedEvent(object sender, ProcessingProgressEventArgs e)
        {
            UpdateProgress(e);
        }

        public void UpdateProgress(ProcessingProgressEventArgs e)
        {
            if (!InvokeRequired)
            {
                ProgressBarLabel.Text = e.ProcessingTask;
                ProcessingProgressBar.Value = e.PercentComplete;
            }
            else
            {
                Invoke(new Action<ProcessingProgressEventArgs>(UpdateProgress), e);
            }
        }

        #endregion

        #region Button commands and user input

        private void ComputeButton_Click(object sender, EventArgs e)
        {
            ProcessingProgressBar.Visible = true;
            ProgressBarLabel.Visible = true;

            if (!_bw_processing.IsBusy)
                _bw_processing.RunWorkerAsync();
        }

        private void ModelImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a model image";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _model_image_path = openFileDialog.FileName;
                ModelImgLabel.Text = _model_image_path;
                LoadModelImage();
                SaveSettings();
            }
        }

        private void SourceImgButton_Click(object sender, EventArgs e)
        {
            string selection_result = SelectFolderDialog();
            if (selection_result == "")
                return;
            _source_img_folder = selection_result;
            SourceImgLabel.Text = _source_img_folder;
            SaveSettings();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveMosaicDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving mosaic image: {ex.Message}");
            }
        }

        private void CancelProcessingButton_Click(object sender, EventArgs e)
        {
            if (_bw_processing.IsBusy)
                _image_processing.CancelProcessing = true;
        }
        
        private void SaveMosaicDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog.Title = "Save mosaic image.";
            saveFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.  
                FileStream fs = (FileStream)saveFileDialog.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the  
                // File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based.  
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        _mosaic_image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case 2:
                        _mosaic_image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case 3:
                        _mosaic_image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private string SelectFolderDialog()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
                return "";
            }
        }


        private void GridWidthTB_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(GridWidthTB.Text, out _mosaic_grid_width);
            SaveSettings();
        }

        private void GridHeightTB_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(GridHeightTB.Text, out _mosaic_grid_height);
            SaveSettings();
        }

        private void ColourModeCB_CheckedChanged(object sender, EventArgs e)
        {
            _coloured_mosaic_mode = ColourModeCB.Checked;
        }

        private void MosaicZoomTrackBar_Scroll(object sender, EventArgs e)
        {
            _mosaic_zoom_value = MosaicZoomTrackBar.Value;
            ZoomImage(MosaicImagePB, _mosaic_zoom_value);
            MosaicZoomLabel.Text = $"{_mosaic_zoom_value} %";
        }

        #endregion

        #region Zoom functions

        private void ZoomImage(PictureBox ZoomPictureBox, int ZoomValue)
        {
            ResizeAndDisplayImage(ZoomValue / 100.0);
        }

        private void ResizeAndDisplayImage(double ratio)
        {
            MosaicImagePB.BackColor = Color.Black;

            if (_mosaic_image == null)
                return;

            int sourceWidth = _mosaic_image.Width;
            int sourceHeight = _mosaic_image.Height;
            int targetWidth = (int)(ratio * MosaicImagePB.Width);
            int targetHeight = (int)(ratio * MosaicImagePB.Height);
            int targetTop;
            int targetLeft;

            if (panning)
            {
                targetTop = (MosaicImagePB.Height - targetHeight) / 2 + movingPoint.Y;
                targetLeft = (MosaicImagePB.Width - targetWidth) / 2 + movingPoint.X;
            }
            else
            {
                targetTop = (MosaicImagePB.Height - targetHeight) / 2;
                targetLeft = (MosaicImagePB.Width - targetWidth) / 2;
            }
            
            Bitmap tempBitmap = new Bitmap(MosaicImagePB.Width, MosaicImagePB.Height,
                                           PixelFormat.Format24bppRgb);

            tempBitmap.SetResolution(_mosaic_image.HorizontalResolution,
                                     _mosaic_image.VerticalResolution);
            Graphics bmGraphics = Graphics.FromImage(tempBitmap);
            bmGraphics.Clear(Color.Black);
            bmGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            bmGraphics.DrawImage(_mosaic_image,
                                 new Rectangle(targetLeft, targetTop, targetWidth, targetHeight),
                                 new Rectangle(0, 0, sourceWidth, sourceHeight),
                                 GraphicsUnit.Pixel);

            bmGraphics.Dispose();

            try
            {
                MosaicImagePB.Image.Dispose();
            }
            catch (Exception) { }

            MosaicImagePB.Image = tempBitmap;
        }



        private Point startingPoint = Point.Empty;
        private Point movingPoint = Point.Empty;
        private bool panning = false;

        private void MosaicImagePB_MouseDown(object sender, MouseEventArgs e)
        {
            panning = true;
            startingPoint = new Point(e.Location.X - movingPoint.X,
                                      e.Location.Y - movingPoint.Y);
        }

        private void MosaicImagePB_MouseMove(object sender, MouseEventArgs e)
        {
            if (panning)
            {
                movingPoint = new Point(e.Location.X - startingPoint.X,
                                        e.Location.Y - startingPoint.Y);
                ZoomImage(MosaicImagePB, _mosaic_zoom_value);
            }
        }

        private void MosaicImagePB_MouseUp(object sender, MouseEventArgs e)
        {
            panning = false;
        }

        private void ResetZoomButton_Click(object sender, EventArgs e)
        {
            MosaicZoomTrackBar.Value = 100;
            _mosaic_zoom_value = 100;
            MosaicZoomLabel.Text = "100 %";
            ZoomImage(MosaicImagePB, _mosaic_zoom_value);
        }

        #endregion
    }
}
