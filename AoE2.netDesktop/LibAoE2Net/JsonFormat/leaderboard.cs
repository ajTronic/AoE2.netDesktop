﻿#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace LibAoE2net
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Leaderboard
    {
        [DataMember(Name = "profile_id")]
        public int? ProfileId { get; set; }

        [DataMember(Name = "rank")]
        public int? Rank { get; set; }

        [DataMember(Name = "rating")]
        public int? Rating { get; set; }

        [DataMember(Name = "steam_id")]
        public string SteamId { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "clan")]
        public string Clan { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "previous_rating")]
        public int? PreviousRating { get; set; }

        [DataMember(Name = "highest_rating")]
        public int? HighestRating { get; set; }

        [DataMember(Name = "streak")]
        public int? Streak { get; set; }

        [DataMember(Name = "lowest_streak")]
        public int? LowestStreak { get; set; }

        [DataMember(Name = "highest_streak")]
        public int? HighestStreak { get; set; }

        [DataMember(Name = "games")]
        public int? Games { get; set; }

        [DataMember(Name = "wins")]
        public int? Wins { get; set; }

        [DataMember(Name = "losses")]
        public int? Losses { get; set; }

        [DataMember(Name = "drops")]
        public int? Drops { get; set; }

        [DataMember(Name = "last_match")]
        public int? LastMatch { get; set; }

        [DataMember(Name = "last_match_time")]
        public int? LastMatchTime { get; set; }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented