using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSketchImageXcassets
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("enter the image location");

            var folderlocation = Console.ReadLine();

            Console.WriteLine("enter the name");

            var name = Console.ReadLine();


            sliceimage(folderlocation, name);

        }

        public static void sliceimage(string image, string name)
        {//C:\Users\matt\Desktop\BackUpFastFix\GoogleDriveSSolutions\Social Empire\Game sprites\Character images\Female\Layer0_Back Hair\Female_Hair_05_B.png

            string filename = image;
           
            ushort width = 64;
            ushort height = 98;
         
            Bitmap bmp = null;
            try
            {
                bmp = (Bitmap)Image.FromFile(filename);
            }
            catch
            {
                Console.WriteLine("Error 5: Cannot load image from file.");
                
            }
            if (width == 0)
                width = (ushort)bmp.Width;
            if (height == 0)
                height = (ushort)bmp.Height;
            int columns = bmp.Width / width;
            int rows = bmp.Height / height;
            int cells = columns * rows;
            if (cells == 0)
            {
                Console.WriteLine("Warning: Cannot split image in 0 cells.");
               
            }
            string dir;
            try
            {
                dir = Path.GetDirectoryName(filename) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(filename);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
            catch
            {
                Console.WriteLine("Error 6: Cannot create directory for cells.");
      
            }
            Console.WriteLine();
            Console.WriteLine("About to split image in {0} rows x {1} columns = {2} cells...", rows, columns, cells);
            Console.WriteLine();
            int cellpadding = (cells - 1).ToString().Length;
            int rowpadding = (rows - 1).ToString().Length;
            string cellfile;
            string cellpath;
            string direction = "N";
            try
            {
                for (int row = 0; row < rows; row++)
                {
                    Console.Write("Row " + row.ToString().PadLeft(rowpadding, ' ') + ":  ");
                    for (int column = 0; column < columns; column++)
                    {
                        if (row == 0)
                        {
                            direction = "S";
                        }
                        else if (row == 1)
                        {
                            direction = null;
                        }
                        else if (row == 2)
                        {
                            direction = "W";
                        }
                        else if (row == 3)
                        {
                            direction = null;
                        }
                        else if (row == 4)
                        {
                            direction = "N";
                        }
                        else if (row == 5)
                        {
                            direction = null;
                        }
                        else if (row == 6)
                        {
                            direction = "E";
                        }
                        else if (row == 7)
                        {
                            direction = null;
                        }


                        if (direction != null)
                        {


                            //C:\Users\matt\Desktop\BackUpFastFix\GoogleDriveSSolutions\Social Empire\Game sprites\Character images\Female\Layer3_Shirt\Female_Shirt_02.png

                            cellfile = String.Format("{0}{1}{2}.png", name, direction, (column + 1).ToString());
                            cellpath = @"C:\sprites\" + Path.DirectorySeparatorChar + cellfile;
                            bmp.Clone(new Rectangle(column * width, row * height, width, height), bmp.PixelFormat).Save(cellpath, ImageFormat.Png);
                            Console.Write(cellfile + "  ");


                            //create the image set folder
                            DirectoryInfo imgsetfolder = Directory.CreateDirectory(cellpath.Replace(".png", ".imageset"));
                            //copy file to directory
                            File.Copy(cellpath, imgsetfolder.FullName +"//"+ cellfile);

                            //create jsonfile in folder
                            string jsontext = GetFileText().Replace("FemaleSkinN2.png", cellfile);
                            using (StreamWriter sw = new StreamWriter(imgsetfolder.FullName + "//" + "Contents.json"))
                            {
                                sw.Write(jsontext);
                            }

                            File.Delete(cellpath);
                        }

                    
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("{0} files written to disk.", cells);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error 7: " + ex.Message);
                
            }
          
        }

        public static string GetFileText()
        {
            StreamReader sw = new StreamReader("Contents.json");
            string file = sw.ReadToEnd();
            return file;
        }

    }
}
