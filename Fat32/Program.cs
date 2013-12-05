using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fat32
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();

            Block[] theBlocks = new Block[10];
            File[] theFiles = new File[3];
            for (int i = 0; i < theBlocks.Length; i++)
            {
                theBlocks[i] = new Block(r.Next(100));
            }

            theFiles[0] = new File("random.txt", 70);
            theFiles[1] = new File("hello.txt", 25);
            theFiles[2] = new File("world.txt", 46);

            Console.WriteLine("The available blocks are: ");
            foreach (var b in theBlocks)
            {
                Console.Write(b.size + " ");
            }
            for (int i = 0; i < theFiles.Length; i++)
            {
                for (int k = 0; k < theBlocks.Length; k++)
                {
                    if(theBlocks[k].available==true)
                    {
                        if(theBlocks[k].size>=theFiles[i].size)
                        {
                            theBlocks[k].available = false;
                            theFiles[i].block=theBlocks[k];
                            theFiles[i].saved=true;
                            break;
                        }
                    }
                }
            }
            foreach (var f in theFiles)
            {
                if (f.saved==true)
                {
                    Console.Write("\nFile {0} was saved in block with size {1}, and left {2} of space available", f.name, f.block.size, f.block.size - f.size);
                }
            }
            Console.ReadLine();
        }
    }

    public class Block
    {
        public int size { get; set; }
        public bool available { get; set; }

        public Block (int _size)
        {
            size = _size;
            available = true;
        }
    }

    public class File
    {
        public int size { get; set; }
        public string name { get; set; }
        public Block block { get; set; }
        public bool saved { get; set; }
        
        public File(string _name, int _size)
        {
            name = _name;
            size = _size;
            block = new Block(0);
            saved = false;
        }

    }
}
