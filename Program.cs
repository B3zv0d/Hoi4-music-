#region premístění souborů
/*
string d = @"C:\Users\DELL\Downloads\ZPV_v_PDF_neggri.pdf";
string name = Path.GetFileName(d);
string k = @"C:\\Users\\DELL\\OneDrive\\Pictures\\Camera Roll";
string dest = Path.Combine(k, name);
File.Move(d, dest);
*/
#endregion
using HOI4_music;

Music music = new Music();
bool Continue = true;
while (Continue)
{ 
    Console.Clear();
    music.Menu(Continue);
}

