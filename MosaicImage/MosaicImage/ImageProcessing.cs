using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class ProcessingProgressEventArgs : EventArgs
    {
        public int PercentComplete { get; set; }
        public string ProcessingTask { get; set; }
    }

    public class ImageSettings
    {
        public string source_img_folder { get; set; }
        public string model_image_path { get; set; }
        public int mosaic_grid_width { get; set; }
        public int mosaic_grid_height { get; set; }
    }

    public class MosaicImageProcessing
    {
        public bool CancelProcessing { get; set; }

        public event EventHandler<ProcessingProgressEventArgs> ProcessingProgressChangedEvent;
        public void OnProcessingProgressChanged(ProcessingProgressEventArgs e)
        {
            if (ProcessingProgressChangedEvent != null)
                ProcessingProgressChangedEvent.Invoke(this, e);
        }

        public bool IsImage(string source)
        {
            return (source.EndsWith(".png") || source.EndsWith(".jpg") || source.EndsWith(".bmp") || source.EndsWith(".tiff") || source.EndsWith(".gif"));
        }

        public bool IsGrayScale(Image image)
        {
            Color color = ((Bitmap)image).GetPixel(0, 0);
            if (color.A != 0 && (color.R != color.G || color.G != color.B))
            {
                return false;
            }
            return true;
        }


        public Bitmap CreateMosaic(int grid_width, int grid_height, string sourceFolder, Bitmap original_img, bool ColourMosaic)
        {
            int original_width = original_img.Width;
            int original_height = original_img.Height;

            //int grid_side = 30;
            int sub_image_width = (int)(original_width / (double)grid_width);
            int sub_image_height = (int)(original_height / (double)grid_height);

            Console.WriteLine($"Sub image: {sub_image_width} x {sub_image_height}");
            Console.WriteLine($"Sub image pixels: {grid_width * sub_image_width} x {grid_height * sub_image_height} == {original_width} x {original_height}");

            AForge.Imaging.Filters.ResizeNearestNeighbor resize = new AForge.Imaging.Filters.ResizeNearestNeighbor(sub_image_width, sub_image_height);
            AForge.Imaging.Filters.Grayscale grayscale = new AForge.Imaging.Filters.Grayscale(1 / 3.0, 1 / 3.0, 1 / 3.0);

            string[] files = Directory.GetFiles(sourceFolder);

            Bitmap combined_image = new Bitmap(original_width, original_height);
            Graphics g = Graphics.FromImage(combined_image);

            Bitmap original_gray = grayscale.Apply(original_img);
            Bitmap[] original_slices = new Bitmap[grid_width * grid_height];
            for (int i = 0; i < grid_width; i++)
            {
                for (int j = 0; j < grid_height; j++)
                {
                    if (CancelProcessing)
                        return null;
                    Rectangle rect = new Rectangle(i * sub_image_width, j * sub_image_height, sub_image_width, sub_image_height);
                    AForge.Imaging.Filters.Crop crop_region = new AForge.Imaging.Filters.Crop(rect);
                    Bitmap slice = (ColourMosaic == true) ? crop_region.Apply(original_img) : crop_region.Apply(original_gray);
                    original_slices[i * grid_width + j] = slice;
                }
            }

            Bitmap[] candidates = new Bitmap[files.Length];
            for (int i = 0; i < candidates.Length; i++)
            {
                if (i % 100 == 0 || i + 100 > candidates.Length)
                {
                    int PercentComplete = (int)((100.0 * i) / candidates.Length);
                    Console.WriteLine($"Candidate preprocessing progress: {PercentComplete}%");
                    OnProcessingProgressChanged(new ProcessingProgressEventArgs() { PercentComplete = PercentComplete, ProcessingTask = "Preprocessing..." });
                }

                if (CancelProcessing)
                    return null;

                if (!IsImage(files[i]))
                    continue;

                Bitmap candidate_image = AForge.Imaging.Image.FromFile(files[i]);
                Bitmap candidate_gray;
                Bitmap resized_image;
                if (IsGrayScale(candidate_image) && false)
                {
                    if (ColourMosaic)
                        continue;

                    resized_image = resize.Apply(candidate_image);
                }
                else
                {
                    if (ColourMosaic)
                        resized_image = resize.Apply(candidate_image);
                    else
                    {
                        candidate_gray = grayscale.Apply(candidate_image);
                        resized_image = resize.Apply(candidate_gray);
                    }
                }

                candidates[i] = resized_image;
            }

            List<int> used_indices = new List<int>();
            int step = 0;
            for (int i = 0; i < grid_width; i++)
            {
                for (int j = 0; j < grid_height; j++)
                {
                    if (CancelProcessing)
                        return null;
                    int PercentComplete = (int)((100.0*step)/(grid_width* grid_height -1));
                    OnProcessingProgressChanged(new ProcessingProgressEventArgs() { PercentComplete = PercentComplete, ProcessingTask = "Creating mosaic..." });
                    Console.WriteLine($"Finding best match to slice {step}/{grid_width * grid_height - 1}...");
                    int best_match_index = FindBestMatch(original_slices[step], candidates, used_indices);
                    used_indices.Add(best_match_index);
                    Bitmap sub_image = candidates[best_match_index];
                    int cornerX = i * sub_image_width;
                    int cornerY = j * sub_image_height;
                    g.DrawImage(sub_image, new Point(cornerX, cornerY));
                    step++;
                }
            }

            combined_image.Save("combined_image.jpg");

            return combined_image;
        }

        private int FindBestMatch(Bitmap model_image, Bitmap[] candidates, List<int> used_indices)
        {
            double best_similarity = 0;
            int best_index = 0;
            AForge.Imaging.ExhaustiveTemplateMatching template_matching = new AForge.Imaging.ExhaustiveTemplateMatching(0);

            for (int i = 0; i < candidates.Length; i++)
            {
                if (used_indices.Contains(i))
                    continue;
                AForge.Imaging.TemplateMatch[] result;
                try
                {
                   result = template_matching.ProcessImage(model_image, candidates[i]);

                }
                catch (Exception)
                {
                    return 0;
                }
                double similarity = result[0].Similarity;
                if (i == 0 || similarity > best_similarity)
                {
                    best_similarity = similarity;
                    best_index = i;
                }
            }
            return best_index;
        }
    }


}
