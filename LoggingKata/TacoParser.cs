namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            var cells = line.Split(',');

            if (cells.Length < 3)
            {
                logger.LogError("Line doesn't have complete info");

                return null;
            }

            var lat = double.Parse(cells[0]);
            var lon = double.Parse(cells[1]);
            var name = cells[2];

            var point = new Point();
            point.Latitude = lat;
            point.Longitude = lon;

            TacoBell bell = new TacoBell();
            bell.Location = point;
            bell.Name = name;

            return bell;
        }
    }
}