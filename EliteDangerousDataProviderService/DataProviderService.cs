﻿using EliteDangerousDataDefinitions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;

namespace EliteDangerousDataProviderService
{
    /// <summary>Access to EDDP data<summary>
    public class DataProviderService
    {
        public static StarSystem GetSystemData(string system)
        {
            var client = new WebClient();
            var response = client.DownloadString("http://api.eddp.co:16161/systems/" + system);
            return StarSystemFromEDDP(response);
        }

        public static StarSystem StarSystemFromEDDP(string data)
        {
            return StarSystemFromEDDP(JObject.Parse(data));
        }

        public static StarSystem StarSystemFromEDDP(dynamic json)
        {
            StarSystem StarSystem = new StarSystem();

            StarSystem.Name = (string)json["name"];
            StarSystem.Population = (long?)json["population"] == null ? 0 : (long)json["population"];
            StarSystem.Allegiance = (string)json["allegiance"];
            StarSystem.Government = (string)json["government"];
            StarSystem.Faction = (string)json["faction"];
            StarSystem.PrimaryEconomy = (string)json["primary_economy"];
            StarSystem.State = (string)json["state"] == "None" ? null : (string)json["state"];
            StarSystem.Security = (string)json["security"];
            StarSystem.Power = (string)json["power"];
            StarSystem.PowerState = (string)json["power_state"];

            StarSystem.X = (decimal?)json["x"];
            StarSystem.Y = (decimal?)json["y"];
            StarSystem.Z = (decimal?)json["z"];

            StarSystem.Stations = StationsFromEDDP(json);
            return StarSystem;
        }

        public static List<Station> StationsFromEDDP(dynamic json)
        {
            List<Station> Stations = new List<Station>();

            foreach (dynamic station in json["stations"])
            {
                Station Station = new Station();
                Station.Name = (string)station["name"];

                Station.Allegiance = (string)station["allegiance"];
                Station.DistanceFromStar = (long?)station["distance_to_star"];
                Station.HasRefuel = (bool?)station["has_refuel"];
                Station.HasRearm = (bool?)station["has_rearm"];
                Station.HasRepair = (bool?)station["has_repair"];
                Station.HasOutfitting = (bool?)station["has_outfitting"];
                Station.HasShipyard = (bool?)station["has_shipyard"];
                Station.HasMarket = (bool?)station["has_market"];
                Station.HasBlackMarket = (bool?)station["has_blackmarket"];

                if (((string)station["type"]) != null)
                {
                    Station.Model = StationModels[(string)station["type"]];
                }

                if (((string)station["max_landing_pad_size"]) != null)
                {
                    Station.LargestShip = LandingPads[(string)station["max_landing_pad_size"]];
                }
            }
            return Stations;
        }

        private static Dictionary<string, ShipSize> LandingPads = new Dictionary<string, ShipSize>()
        {
            { "S", ShipSize.Small },
            { "M", ShipSize.Medium },
            { "L", ShipSize.Large },
            { "H", ShipSize.Huge },
        };

        private static Dictionary<string, StationModel> StationModels = new Dictionary<string, StationModel>()
        {
            { "Civilian Outpost", StationModel.CivilianOutpost },
            { "Commercial Outpost", StationModel.CommercialOutpost },
            { "Coriolis Starport", StationModel.CoriolisStarport },
            { "Industrial Outpost", StationModel.IndustrialOutpost },
            { "Military Outpost", StationModel.MilitaryOutpost },
            { "Mining Outpost", StationModel.MiningOutpost },
            { "Ocellus Starport", StationModel.OcellusStarport },
            { "Orbis Starport", StationModel.OrbisStarport },
            { "Planetary Outpost", StationModel.PlanetaryOutpost },
            { "Planetary Port", StationModel.PlanetaryPort },
            { "Scientific Outpost", StationModel.ScientificOutpost},
            { "Unknown Outpost", StationModel.UnknownOutpost},
            { "Unknown Planetary", StationModel.UnknownPlanetary},
            { "Unknown Starport", StationModel.UnknownStarport},
            { "Unsanctioned Outpost", StationModel.UnsanctionedOutpost},
        };
    }
}
