﻿using EddiDataDefinitions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EddiEvents
{
    public class StarScannedEvent : Event
    {
        public const string NAME = "Star scanned";
        public const string DESCRIPTION = "Triggered when you complete a scan of a stellar body";
        public static string SAMPLE = "{ \"timestamp\":\"2018-12-01T08:04:24Z\", \"event\":\"Scan\", \"ScanType\":\"AutoScan\", \"BodyName\":\"Arietis Sector UJ-Q b5-2\", \"BodyID\":0, \"DistanceFromArrivalLS\":0.000000, \"StarType\":\"L\", \"StellarMass\":0.218750, \"Radius\":249075072.000000, \"AbsoluteMagnitude\":11.808075, \"Age_MY\":10020, \"SurfaceTemperature\":1937.000000, \"Luminosity\":\"V\", \"RotationPeriod\":119097.164063, \"AxialTilt\":0.000000 }";

        public static Dictionary<string, string> VARIABLES = new Dictionary<string, string>();

        static StarScannedEvent()
        {
            VARIABLES.Add("bodyname", "The name of the star that has been scanned");
            VARIABLES.Add("chromaticity", "The apparent colour of the star that has been scanned");
            VARIABLES.Add("stellarclass", "The stellar class of the star that has been scanned (O, G, etc)");
            VARIABLES.Add("solarmass", "The mass of the star that has been scanned, relative to Sol's mass");
            VARIABLES.Add("massprobability", "The probablility of finding a star of this class with this mass");
            VARIABLES.Add("radius", "The radius of the star that has been scanned, in metres");
            VARIABLES.Add("solarradius", "The radius of the star that has been scanned, compared to Sol");
            VARIABLES.Add("radiusprobability", "The probablility of finding a star of this class with this radius");
            VARIABLES.Add("absolutemagnitude", "The absolute (bolometric) magnitude of the star that has been scanned");
            VARIABLES.Add("luminosity", "The luminosity of the star that has been scanned");
            VARIABLES.Add("luminosityclass", "The luminosity class of the star that has been scanned");            
            VARIABLES.Add("tempprobability", "The probablility of finding a star of this class with this temperature");
            VARIABLES.Add("age", "The age of the star that has been scanned, in millions of years");
            VARIABLES.Add("ageprobability", "The probablility of finding a star of this class with this age");
            VARIABLES.Add("temperature", "The temperature of the star that has been scanned");
            VARIABLES.Add("distance", "The distance in LS from the main star");
            VARIABLES.Add("distancefromarrival", "The distance in LS from the main star (old - do not use... preserved for compatibility)");
            VARIABLES.Add("orbitalperiod", "The number of seconds taken for a full orbit of the main star");
            VARIABLES.Add("rotationalperiod", "The number of seconds taken for a full rotation");
            VARIABLES.Add("semimajoraxis", "");
            VARIABLES.Add("eccentricity", "");
            VARIABLES.Add("inclination", "");
            VARIABLES.Add("periapsis", "");
            VARIABLES.Add("rings", "The star's rings");
            VARIABLES.Add("estimatedvalue", "The estimated value of the current scan");
            VARIABLES.Add("estimatedhabzoneinner", "The estimated inner radius of the habitable zone of the scanned star, in light seconds, not considering other stars in the system");
            VARIABLES.Add("estimatedhabzoneouter", "The estimated outer radius of the habitable zone of the scanned star, in light seconds, not considering other stars in the system");
            VARIABLES.Add("mainstar", "True if the star is the main / primary star in the star system");
        }

        // Variable names for this event should match the class property names for maximum compatibility with the BodyDetails() function in Cottle

        public string bodyname => star.bodyname;

        public string stellarclass => star.stellarclass;

        public decimal solarmass => (decimal)star.solarmass;

        public decimal? massprobability => star.massprobability;

        public decimal radius => (decimal)star.radius;

        public decimal solarradius => (decimal)star.solarradius;

        public decimal? radiusprobability => star.radiusprobability;

        public decimal absolutemagnitude => (decimal)star.absolutemagnitude;

        public decimal luminosity => (decimal)star.luminosity;

        public string luminosityclass => star.luminosityclass;

        public decimal? tempprobability => star.tempprobability;

        public long age => (long)star.age;

        public decimal? ageprobability => star.ageprobability;

        public decimal temperature => (decimal)star.temperature;

        public string chromaticity => star.chromaticity;

        public decimal distance => (decimal)star.distance;

        public decimal? orbitalperiod => star.orbitalperiod;

        public decimal rotationalperiod => (decimal)star.rotationalperiod;

        public decimal? semimajoraxis => star.semimajoraxis;

        public decimal? eccentricity => star.eccentricity;

        public decimal? inclination => star.inclination;

        public decimal? periapsis => star.periapsis;

        public List<Ring> rings => star.rings;

        public decimal? estimatedhabzoneinner => star.estimatedhabzoneinner;

        public decimal? estimatedhabzoneouter => star.estimatedhabzoneouter;

        public long? estimatedvalue => star.estimatedvalue;

        public bool mainstar => (bool)star.mainstar;

        // Variables below are not intended to be user facing
        public long? bodyId { get; private set; }
        public List<IDictionary<string, object>> parents { get; private set; }
        public string scantype { get; private set; } // One of AutoScan, Basic, Detailed, NavBeacon, NavBeaconDetail
                                                     // AutoScan events are detailed scans triggered via proximity. 
        public Body star { get; private set; }

        // Deprecated, maintained for compatibility with user scripts
        [JsonIgnore, Obsolete("Use bodyname instead")]
        public string name => bodyname; 
        [JsonIgnore, Obsolete("Use inclination instead")]
        public decimal? orbitalinclination => inclination;  
        [JsonIgnore, Obsolete("Use rotationalperiod instead")]
        public decimal rotationperiod => rotationalperiod;  
        [JsonIgnore, Obsolete("Use distance instead")]
        public decimal distancefromarrival => distance;

        public StarScannedEvent(DateTime timestamp, string scantype, Body star) : base(timestamp, NAME)
        {
            this.star = star;
            this.scantype = scantype;
        }
    }
}
