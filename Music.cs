using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace HOI4_music
{
    public class Music
    {
        public void Menu(bool Continue)
        {
            string[] moznosti = { "Add music", "Show added music", "Change music of loading", "Remove added music",};
            for (int i = 0; i < moznosti.Length; i++)
            {
                Console.WriteLine(i + 1 + " " + moznosti[i]);
            }
            volba(Continue);
        }
        private void volba(bool Continue1)
        {
            if (!int.TryParse(Console.ReadLine(), out int music))
            {
                Continue1 = true;
            }
            switch (music)
            {
                case 1:
                    Add();
                    break;

                case 2:

                    ShowMusic();
                    break;

                case 3:
                    ChangeLoading();
                    break;
                case 4:
                    RemoveMusic();
                    break;
                

            }

        }
        private void Add()
        {
            Start:
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Write path of music");

                string path = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(path))
                {
                    continue;
                }
                else
                {
                    if (!path.Contains('\\'))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Write only path of music");
                        Console.ResetColor();
                        Console.WriteLine("Example:");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(@"C:\\Users\\Pc\\Downloads\\Music.ogg\");
                        Console.ResetColor();
                        Console.ReadKey();
                        continue;
                    }
                    if (path.Contains('"'))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter the path without quotes");
                        Console.ReadKey();
                        Console.ResetColor();
                        continue;
                    }

                    string[] Control = path.Split('.');

                    string[] SongName = Control[0].Split("\\");

                    if (Control[1] != "ogg")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Write path only for files with .ogg");
                        Console.ReadKey();
                        Console.ResetColor();
                        continue;
                    }

                    else
                    {

                        string name = Path.GetFileName(path);
                        string[] name1 = name.Split('"');
                        string EndDestination = @"C:\\Program Files (x86)\\Steam\\steamapps\\common\\Hearts of Iron IV\\music\\hoi3";
                        string kDestination = Path.Combine(EndDestination, name1[0]);
                        if (ControlPath("Music.txt", SongName[SongName.Length-1]) == true)
                        {
                            goto Start;
                        }
                        else
                        {
                            File.Copy(path, kDestination);
                            string hoi3Soundtrack = @"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\hoi3\hoi3_soundtrack.txt";
                            string hoi3SoundtrackAsset = @$"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\hoi3\hoi3_music.asset";



                            File.AppendAllText(hoi3Soundtrack, "music={" + Environment.NewLine);
                            File.AppendAllText(hoi3Soundtrack, "song = " + $"\"{SongName[SongName.Length-1]}\"" + Environment.NewLine);
                            File.AppendAllText(hoi3Soundtrack, "}" + Environment.NewLine);
                            File.AppendAllText(hoi3SoundtrackAsset, "music={" + Environment.NewLine);
                            File.AppendAllText(hoi3SoundtrackAsset, "name=" + $"\"{SongName[SongName.Length - 1]}\"" + Environment.NewLine);
                            File.AppendAllText(hoi3SoundtrackAsset, "file=" + $"\"{name}\"" + Environment.NewLine);
                            File.AppendAllText(hoi3SoundtrackAsset, "volume=" + "0.57" + Environment.NewLine);
                            File.AppendAllText(hoi3SoundtrackAsset, "}" + Environment.NewLine);
                            File.AppendAllText("Music.txt", SongName[SongName.Length - 1] + Environment.NewLine);
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Music was sucessfully added");
                            Console.ResetColor();
                            Console.ReadKey();
                        }


                    }
                }
                break;
            }

        }
        private void ShowMusic()
        {
            Console.Clear();
            Console.WriteLine("Added music:");
            string[] file = File.ReadAllLines("Music.txt");
            for (int i = 0; i < file.Length; i++)
            {
                Console.WriteLine($" {file[i]}");
            }
            Console.WriteLine("Press any key to exit ");
            Console.ReadKey();
        }
        private void ChangeLoading()
        {
            bool Continue = true;
            while (Continue)
            {   Console.Clear();
                string[] menu = { "Change on default song", "Add new song for loading", "Changed on added song","Exit"};
                for (int i = 0; i < menu.Length; i++)
                {
                    Console.WriteLine(i + 1 + " " + menu[i]);
                }
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    continue;
                }

                switch (id)
                {
                    case 1:
                        Console.Clear();
                        DefaultLoading();
                        Continue = false;
                        break;

                    case 2:
                        Console.Clear();
                        CustomLoading();
                        Continue = false;
                        break;
                    case 3:
                        Console.Clear();
                        AddedLoading();
                        Continue = false;
                        break;
                        case 4:
                        Continue = false;
                        break;
                }
            }
            



        }

        private void CustomLoading()
        {
            bool Continue = true;
            while (Continue)
            {
                Start:
                Console.Clear();
                Console.WriteLine("Write a music path");

                string path = Console.ReadLine();
                if (!path.Contains('\\'))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter only path of music");
                    Console.ResetColor();
                    Console.WriteLine("Example:");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(@"C:\\Users\\Pc\\Downloads\\Music.ogg\");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }
                if (path.Contains('"'))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter the path without quotes");
                    Console.ReadKey();
                    Console.ResetColor();
                    continue;
                }
                

                string[] Control = path.Split('.');

                

                if (Control[1] != "ogg")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter path only for files with .ogg");
                    Console.ReadKey();
                    Console.ResetColor();
                    continue;
                }

                
                

                    string name = Path.GetFileName(path);
                    string asset = @"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\music.asset";//Change directory
                    string[] name1 = name.Split('"');

                    string EndDestination = @"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music";//Change directory
                    string[] SongName = Control[0].Split("oads\\");
                    string kDestination = Path.Combine(EndDestination, name1[0]);

                if (ControlPath("CustomLoading.txt", SongName[SongName.Length-1]) == true)
                {
                    goto Start;
                }
                else
                {
                    File.Copy(path, kDestination);

                    string[] d = File.ReadAllLines(@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\music.asset");
                    string[] file = d[4].Split('"');
                    file[1] = name;

                    d[4] = $"\tfile = \"{name}\"";
                    File.Delete(asset);
                    for (int i = 0; i < d.Length; i++)
                    {

                        File.AppendAllText(asset, d[i] + Environment.NewLine);
                    }
                    File.AppendAllText("CustomLoading.txt", SongName[SongName.Length-1] + Environment.NewLine);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Loading music was changed");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                    
                
                break;
            }
        }
        private void DefaultLoading()
        {
            string asset = @"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\music.asset";
            string name = "hoi4mainthemeallies.ogg";
            string[] d = File.ReadAllLines(@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\music.asset");
            string[] file = d[4].Split('"');
            file[1] = name;

            d[4] = $"\tfile = \"{name}\"";
            File.Delete(asset);
            for (int i = 0; i < d.Length; i++)
            {

                File.AppendAllText(asset, d[i] + Environment.NewLine);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Loading music was changed");
            Console.ResetColor();
            Console.ReadKey();
        }

        private void AddedLoading()
        {

            string[] file = File.ReadAllLines("CustomLoading.txt");
            while (true)
            {
                Console.Clear();
                int i = 0;
                for (i=0; i < file.Length; i++)
                {
                    Console.WriteLine(i + 1 + " " + file[i]);
                }

                if (!int.TryParse(Console.ReadLine(), out i))
                {
                    continue;
                }
                else
                {
                    switch (i)
                    {
                        default:
                            Set(file[i - 1]);
                            break;
                    }
                }
                break;
            }
        }
        private void Set(string name)
        {
            string asset = @"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\music.asset";

            string[] d = File.ReadAllLines(@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\music.asset");
            string[] file = d[4].Split('"');
            file[1] = name;

            d[4] = $"\tfile = \"{name}.ogg\"";
            File.Delete(asset);
            for (int i = 0; i < d.Length; i++)
            {

                File.AppendAllText(asset, d[i] + Environment.NewLine);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Loading music was changed");
            Console.ResetColor();
            Console.ReadKey();
        }
        private void RemoveAssetMusic(string name)
        {
            
            string AssetFile = File.ReadAllText(@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\hoi3\hoi3_music.asset");
            string[] soundtrack = File.ReadAllLines(@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\hoi3\hoi3_soundtrack.txt");
            File.WriteAllText(@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\hoi3\hoi3_music.asset", String.Empty);
            string[] AssetFileMusic = AssetFile.Split("music");
            string[] AssetFileMusicName;
            File.Delete($@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\hoi3\{name}.ogg");
            int r=0;
            
            for (int i = 0;i < AssetFileMusic.Length;i++)
            {
                AssetFileMusicName = AssetFileMusic[i].Split("name=\"");
                for (int j = 0; j < AssetFileMusicName.Length;j++)
                {
                   string[] d = AssetFileMusicName[j].Split('\"');
                    for (int k = 0; k < d.Length;k++)
                    {
                        if (d[k] == name)
                        {
                            r = i;
                            AssetFileMusic[i] = "";

                            
                        }
                    }

                }
                if (i != r)
                {

                    File.AppendAllText(@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\hoi3\hoi3_music.asset", $"music{AssetFileMusic[i]}");
                }
                else
                {
                    File.AppendAllText(@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\hoi3\hoi3_music.asset", $"{AssetFileMusic[i]}");
                }
            }

        }
        private void RemoveSoundtrackMusic(string name)
        {
            
            string AssetFile = File.ReadAllText(@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\hoi3\hoi3_music.asset");
            string soundtrack = File.ReadAllText(@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\hoi3\hoi3_soundtrack.txt");
            File.WriteAllText(@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\hoi3\hoi3_soundtrack.txt", String.Empty);
            string[] AssetFileMusic = soundtrack.Split("music");
            string[] AssetFileMusicName;
            int r = 0;
           
            for (int i = 0; i < AssetFileMusic.Length; i++)
            {
                AssetFileMusicName = AssetFileMusic[i].Split("song=\"");
                for (int j = 0; j < AssetFileMusicName.Length; j++)
                {
                    string[] d = AssetFileMusicName[j].Split('\"');
                    for (int k = 0; k < d.Length; k++)
                    {
                        if (d[k] == name)
                        {
                            r = i;
                            AssetFileMusic[i] = "";


                        }
                    }

                }
                if (i != r)
                {

                    File.AppendAllText(@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\hoi3\hoi3_soundtrack.txt", $"music{AssetFileMusic[i]}");
                }
                else
                {
                    File.AppendAllText(@"C:\Program Files (x86)\Steam\steamapps\common\Hearts of Iron IV\music\hoi3\hoi3_soundtrack.txt", $"{AssetFileMusic[i]}");
                }
            }
            
        }
        private void RemoveMusicList(string name)
        {

            string[] file = File.ReadAllLines("Music.txt");
            File.Delete("Music.txt");
            string[] file1 = new string[file.Length - 1];
            int k = 0;
            for (int i = 0;i < file.Length;i++)
            {
                if (file[i] != name)
                {
                    file1[k] = file[i];
                    k++;
                }
                else
                {

                }
            }
            for (int i = 0; i < file1.Length;i++)
            {
                File.AppendAllText("Music.txt", file1[i]+Environment.NewLine);
            }
        }
        private void RemoveMusic()
        {
            bool Continue = true;
            while (Continue)
            {
                Console.Clear();
                Console.WriteLine("Chosse which music you want delete");
                string[] file = File.ReadAllLines("Music.txt");
                int i = 0;
                for (; i < file.Length; i++)
                {
                    Console.WriteLine(i + 1 + " " + file[i]);
                }

                if (!int.TryParse(Console.ReadLine(), out i))
                {
                    continue;
                }
                else
                {
                    switch (i)
                    {
                        default:
                            RemoveAssetMusic(file[i - 1]);
                            RemoveSoundtrackMusic(file[i-1]);
                            RemoveMusicList(file[i-1]);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Song was sucessfully deleted");
                            Console.ResetColor();
                            Console.ReadKey();
                            Continue = false;
                            break;
                    }
                }
            }

            //Console.WriteLine(file[1]);

        }
        private bool ControlPath(string FileName,string file)
        {
            string[] f = File.ReadAllLines(FileName);
            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] == file)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This song was added previously");
                    Console.ResetColor();
                    Console.ReadKey();
                    return true;
                }
            }
            return false;
        }
       
    }
}

