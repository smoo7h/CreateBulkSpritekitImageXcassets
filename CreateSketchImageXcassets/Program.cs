using System;
using System.Collections.Generic;
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
            Console.WriteLine("enter the image folder");

            var folderlocation = Console.ReadLine();
            Console.WriteLine("enter the image Direction");
            var Direction = Console.ReadLine();
            Console.WriteLine("enter the image Layer Name");
            var Layer = Console.ReadLine();

            DirectoryInfo dirInfo = new DirectoryInfo(folderlocation);
            //create new directory
          //  DirectoryInfo newdir = Directory.CreateDirectory(folderlocation + "\\" + dirInfo.GetFiles()[0].Name.Replace(".png",""));
        
            foreach (FileInfo f in dirInfo.GetFiles())
            {
                if (f.Name.Contains(".png"))
                {
                    //create the image set folder
                    DirectoryInfo imgsetfolder = Directory.CreateDirectory(f.FullName.Replace(".png", ".imageset").Replace("N",Direction).Replace("Skin",Layer));
                    //copy file to directory
                    f.CopyTo(imgsetfolder.FullName + "\\" + f.Name.Replace("N",Direction).Replace("Skin",Layer));
                    //create jsonfile in folder
                    string jsontext = GetFileText().Replace("FemaleSkinN2.png", f.Name.Replace("N",Direction).Replace("Skin", Layer));
                    using (StreamWriter sw = new StreamWriter(imgsetfolder.FullName + "//" + "Contents.json"))
                    {
                        sw.Write(jsontext);
                    }

                    f.Delete();

                }
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
