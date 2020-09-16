using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.ComponentModel.DataAnnotations;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable locA = null;
            ITrackable locB = null;
            double distance = 0;

            for (int i = 0; i < locations.Length; i++) 
            {
                var locationOne = locations[i];

                var corA = new GeoCoordinate();

                corA.Latitude = locationOne.Location.Latitude;
                corA.Longitude = locationOne.Location.Longitude;

                for (int j = 0; j < locations.Length; j++)
                {
                    var locationTwo = locations[j];

                    var corB = new GeoCoordinate();
                    corB.Latitude = locationTwo.Location.Latitude;
                    corB.Longitude = locationTwo.Location.Longitude;

                    if(corA.GetDistanceTo(corB) > distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        locA = locationOne;
                        locB = locationTwo;

                    }
                }
            }

            logger.LogInfo($"The two locations farthest apart are {locA.Name} and {locB.Name} at {distance * 0.000621371} miles apart.");
        }
    }
}
