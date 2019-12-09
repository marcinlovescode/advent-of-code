using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Utils
{
    public static class SpaceImageFormat
    {
        public static IList<SpaceImageFormatLayer> Decode(int[] pixels, int layerWidth, int layerHeight)
        {
            var remainingPixels = pixels;
            var result = new List<SpaceImageFormatLayer>();
            while (remainingPixels.Length != 0)
            {
                var layer = new SpaceImageFormatLayer(layerWidth, layerHeight);
                layer.LoadLayer(remainingPixels.Take(layerHeight*layerWidth).ToArray());
                result.Add(layer);
                remainingPixels = remainingPixels.Skip(layerHeight * layerWidth).ToArray();
            }

            return result;
        }
    }

    public class SpaceImageFormatLayer
    {
        public int[,] Pixles { get; }
        public int Height => height;
        public int Width => width;
        private int height;
        private int width;

        public SpaceImageFormatLayer(int width, int height)
        {
            Pixles = new int[height,width];
            this.height = height;
            this.width = width;
        }
        
        public void LoadLayer(int[] pixels)
        {
            if (pixels.Length != height * width)
            {
                throw new Exception("Invalid number of pixels");
            }
            var counter = 0;
            for (int i = 0; i < height; i++)
            {
                for (int k = 0; k < width; k++)
                {
                    Pixles[i, k] = pixels[counter];
                    counter++;
                }
            }
        }

        public int GetNumberOfDigits(int digit)
        {
            var counter = 0;
            foreach (var pixel in Pixles)
            {
                if (pixel == digit)
                    counter++;
            }
            return counter;
        }

        public SpaceImageFormatLayer Flatten(SpaceImageFormatLayer top)
        {
            if (top.Height != height || top.Width != width)
            {
                throw new Exception("Invalid dimensions");
            }
            
            var result = new SpaceImageFormatLayer(Width,Height);
            
            for(int i=0; i< Height; i++)
            {
                for(int k=0; k< Width; k++)
                {
                    if (top.Pixles[i, k] == 2)
                    {
                        result.Pixles[i, k] = this.Pixles[i, k];
                    }
                    else
                    {
                        result.Pixles[i, k] = top.Pixles[i, k];
                    }
                }
            }

             return result;
        }

        public string Print()
        {
            var strBuilder = new StringBuilder();
            for (int i = 0; i < height; i++)
            {
                for (int k = 0; k < width; k++)
                {
                    if (Pixles[i, k] == 0)
                    {
                        strBuilder.Append(' ');
                    }
                    else if (Pixles[i, k] == 1)
                    {
                        strBuilder.Append('#');
                    }
                    else
                    {
                        strBuilder.Append('2');;
                    }
                }
                strBuilder.AppendLine();
            }

            return strBuilder.ToString();
        }
    }
}