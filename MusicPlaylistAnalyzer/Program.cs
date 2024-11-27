namespace Music_Playlist_Analyzer;
using System.Linq;
#pragma warning disable CS8618
#pragma warning disable CS8600
class Program
{   
    
    
    public class Song
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public int Size { get; set; }
        public int Time { get; set; }
        public int Year { get; set; }
        public int Plays { get; set; }

        
        override public string ToString()
        {
            return String.Format("Name: {0}, Artist: {1}, Album: {2}, Genre: {3}, Size: {4}, Time: {5}, Year: {6}, Plays: {7}", Name, Artist, Album, Genre, Size, Time, Year, Plays);
        }
    }

    public static List<Song> ReadDataFile(string fileName)
    {
        var songs= new List<Song>();
        try
        {
            using (var reader = new StreamReader(fileName))
            {   
                reader.ReadLine(); //skip first line
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var fields = line.Split('\t');
                    var song = new Song
                    {
                        Name = fields[0],
                        Artist = fields[1],
                        Album = fields[2],
                        Genre = fields[3],
                        Size = Convert.ToInt32(fields[4]),
                        Time = Convert.ToInt32(fields[5]),
                        Year = Convert.ToInt32(fields[6]),
                        Plays = Convert.ToInt32(fields[7])
                    };
                    
                    songs.Add(song);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error reading data file: {e.Message}");
        }
        return songs;
    }
    static void GenerateReport(List<Song> songs, string reportFileName) {
        string mainReport = "";
        var moreThan200Plays = songs.Where(song => song.Plays >= 200);
        int altSongCnt = songs.Count(song => song.Genre == "Alternative");
        int hiphopSongCnt = songs.Count(song => song.Genre == "Hip-Hop/Rap");
        var songsWTTF = songs.Where(song => song.Album == "Welcome to the Fishbowl");
        var before1970 = songs.Where(song => song.Year < 1970);
        var longerThan85 = songs.Where(song => song.Name.Length > 85);
        Song longestSong = songs.Aggregate((max, cur) => max.Time > cur.Time ? max : cur);
        var uniqueGenres = songs.Select(song => song.Genre).Distinct();
        var songsPerYear = songs.GroupBy(song => song.Year).ToDictionary(g => g.Key, g => g.Count());
        var totalPlaysPerYear = songs.GroupBy(song => song.Year).ToDictionary(g => g.Key, g => g.Sum(song => song.Plays));
        var uniqueArtists = songs.Select(song => song.Artist).Distinct();

        {   
            mainReport += "Music Playlist Report\n";

            mainReport += "Songs That Recieved More Than 200 Plays\n";
            foreach (Song song in moreThan200Plays) {
                mainReport += song + "\n";
            }

            mainReport += $"\nNumber of Alternative Songs: {Convert.ToString(altSongCnt)}\n";
            mainReport += $"\nNumber of Hip-hop/Rap Songs: {Convert.ToString(hiphopSongCnt)}\n";

            mainReport += "\nSongs from the album Welcome To The Fishbowl\n";
            foreach (Song song in songsWTTF) {
                mainReport += song + "\n";
            }

            mainReport += "\nSongs that were made before 1970\n";
            foreach (Song song in before1970) {
                mainReport += song + "\n";
            }

            mainReport += "\nSongs with titles longer than 85 characters\n";
            foreach (Song song in longerThan85) {
                mainReport += song + "\n";
            }

            mainReport += $"\nLongest Song (time)\n{longestSong}\n";
            
            mainReport += "\nUnique Genres:\n";
            foreach (String genre in uniqueGenres) {
                mainReport += genre + "\n";
            }

            mainReport += "\nYearly number of songs in playlist\n";
            foreach (KeyValuePair<int, int> pair in songsPerYear) {
                mainReport += $"{pair.Key}: {pair.Value}\n";
            }

            mainReport += "\nTotal number of plays by year\n";
            foreach (KeyValuePair<int, int> pair in totalPlaysPerYear) {
                mainReport += $"{pair.Key}: {pair.Value}\n";
            }

            mainReport += "\nUnique Artists\n";
            foreach (String artist in uniqueArtists) {
                mainReport += artist + "\n";
            }
        }
        
        File.WriteAllText(reportFileName, mainReport);
    }

    static void Main(string[] args)
    {   
        if (args.Length != 2) {
            Console.WriteLine("Error: Please provide the file paths in this format | AnalyzeMusicPlaylist <music_playlist_file_path> <report_file_path>");
            return;
        }

        string dataFilePath = args[0];
        string reportFilePath = args[1];

        List<Song> songList = ReadDataFile(dataFilePath);
        GenerateReport(songList, reportFilePath);
        
    }
}
