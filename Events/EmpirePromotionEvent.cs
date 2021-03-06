﻿using EddiDataDefinitions;
using System;
using System.Collections.Generic;

namespace EddiEvents
{
    public class EmpirePromotionEvent : Event
    {
        public const string NAME = "Empire promotion";
        public const string DESCRIPTION = "Triggered when your rank increases with the Empire";
        public const string SAMPLE = "{ \"timestamp\":\"2017-10-06T23:47:32Z\", \"event\":\"Promotion\", \"Empire\":7 }";
        public static Dictionary<string, string> VARIABLES = new Dictionary<string, string>();

        static EmpirePromotionEvent()
        {
            VARIABLES.Add("rank", "The commander's new Empire rank");
            VARIABLES.Add("femininerank", "The feminine form of the commander's new Empire rank");
            VARIABLES.Add("rating", "The commander's new Federation rank level / rating (as an integer)");
        }

        public string rank { get; private set; }
        public string femininerank { get; private set; }

        public int rating { get; private set; }

        public EmpirePromotionEvent(DateTime timestamp, EmpireRating rating) : base(timestamp, NAME)
        {
            this.rank = rating.maleRank.localizedName;
            this.femininerank = rating.femaleRank.localizedName;
            this.rating = rating.rank;
        }
    }
}
