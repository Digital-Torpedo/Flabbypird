using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml.Serialization;

namespace Flabbypird
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }

    class Highscore
    {

        static Highscore INSTANCE;

        /// <summary>
        /// Instanz der Klasse Highscore.
        /// </summary>
        public static Highscore I
        {
            get
            {
                if (INSTANCE == null)
                    INSTANCE = new Highscore();

                return INSTANCE;
            }
        }

        const int HIGHSCORE_LIMIT = 10;

        List<Player> PlayerScore = new List<Player>();

        public Highscore(string path = @".\highscore\xml.xml")
        {
            var serializer = new XmlSerializer(typeof(Player));

            bool fileExist = File.Exists(path);
            var FileStream = new FileStream(@".\highscore.xml", FileMode.OpenOrCreate);

            if (fileExist)
                PlayerScore = (serializer.Deserialize(FileStream) as Player[]).ToList();

            FileStream.Close();
        }

        public void Add(string playerName, int score)
        {
            PlayerScore.Add(new Player() { Name = playerName, Score = score });
            Sort();
        }

        /// <summary>
        /// Speichert und schließt automatisch den FileStream, wenn die Klasse weggeworfen wird.
        /// </summary>
        ~Highscore()
        {
            var serializer = new XmlSerializer(typeof(Player[]));
            var FileStream = new FileStream(@".\highscore.xml", FileMode.OpenOrCreate);
            serializer.Serialize(FileStream, PlayerScore.ToArray());
            FileStream.Close();
        }

        void Sort()
        {
            PlayerScore.OrderBy(player => player.Score).Take(HIGHSCORE_LIMIT).ToArray();
        }

        public string All()
        {
            return string.Join(Environment.NewLine, PlayerScore.Select(player => string.Format("{0}: {1}", player.Name, player.Score)));
        }
    }
}
