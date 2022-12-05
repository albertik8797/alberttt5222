using System.Data;
using System.Transactions;

namespace ConsoleApp1
{
    static class Prov
    {
        public static DriveInfo[] alldrivers = new DriveInfo[10];
        public static string curentpath = "Диски";
        public static string lastpath;
        public static void Create()
        {
            alldrivers = DriveInfo.GetDrives();
        }
        public static string[] folderin(string dir)
        {
            lastpath = dir;
            curentpath = dir;
            Directory.SetCurrentDirectory(dir);
            return Directory.GetFileSystemEntries(dir);
        }

    }
    public class navicon
    {
        private int startcol = 10, startrow = 50;
        private int newpos, lastpos;
        string[] coun = new string[255];
        public int arrcont = 0;
        private void show()
        {
            Console.SetCursorPosition(startcol - 3, startrow - 3);
            Console.WriteLine(Prov.curentpath);
            for (int i = 0; i < arrcont; i++)
            {
                Console.SetCursorPosition(startcol, startrow + i);
                Console.WriteLine(coun[i]);
            }
        }
        private void clearshow()
        {
            Console.SetCursorPosition(startcol - 3, startrow - 3);
            Console.WriteLine("                                                    ");

            for (int i = 0; i < arrcont; i++)
            {
                Console.SetCursorPosition(startcol - 3, startrow + i);
                Console.WriteLine("                                                               ");
            }
        }

        private void showpos()
        {
            Console.SetCursorPosition(startcol - 3, startrow + lastpos);
            Console.Write(' ');

            Console.SetCursorPosition(startcol - 3, startrow + newpos);
            Console.Write('\u0010');

        }
        public void drivetocoun(DriveInfo[] drv)
        {
            arrcont = drv.Count();
            for (int i = 0; i < arrcont; i++) { coun[i] = drv[i].Name; }
            show();
        }
        public void foldertocount(string[] art)
        {
            coun = art;
            arrcont = art.Count();
        }
        public int keyinfo()
        {
            ConsoleKeyInfo cki;
            Console.TreatControlCAsInput = true;
            int i = 0;
            do
            {
                cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.UpArrow: i = 1; break;
                    case ConsoleKey.DownArrow: i = 2; break;
                    case ConsoleKey.LeftArrow: i = 3; break;
                    case ConsoleKey.RightArrow: i = 4; break;
                    case ConsoleKey.Escape: i = 5; break;
                    case ConsoleKey.Enter: i = 6; break;
                }
            } while (i == 0);
            return i;
        }
        public void main()
        {
            int i;
            show();
            showpos();
            do
            {
                i = keyinfo();
                if (i != 5) { lastpos = newpos; }
                switch (i)
                {
                    case 1: newpos--; break;
                    case 2: newpos++; break;
                        //case 5: newpos++; break;
                }
                if (newpos > arrcont - 1) { newpos = 0; }
                if (newpos < 0) { newpos = arrcont - 1; }
                showpos();
                if (i == 6)
                {
                    clearshow();
                    foldertocount(Prov.folderin(coun[newpos]));
                    show();
                    newpos = lastpos = 0;
                    showpos();
                }
                if (i == 5)
                {
                    clearshow();
                    Console.Clear();
                    drivetocoun(Prov.alldrivers);
                    show();
                    newpos = lastpos = 0;
                    showpos();
                }


            } while (i != 8);


        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Prov.Create();
            navicon navi = new navicon();
            Console.Clear();
            navi.drivetocoun(Prov.alldrivers);
            navi.main();
        }
    }
}