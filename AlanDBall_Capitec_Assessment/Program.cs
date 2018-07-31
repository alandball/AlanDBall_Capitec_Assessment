using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanDBall_Capitec_Assessment
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string fileName;
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();
            fileName = fd.FileName;
            Console.Write(fileName);


            var users = new List<string>{"Alan", "Ward", "Martin"};

            foreach (var user in users)
            {
                Console.WriteLine(user);

            }



            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
