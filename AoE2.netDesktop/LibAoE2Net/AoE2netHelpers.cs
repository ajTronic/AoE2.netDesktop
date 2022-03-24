﻿namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LibAoE2net;

    /// <summary>
    /// Helper class of AoE2net API.
    /// </summary>
    public static class AoE2netHelpers
    {
        /// <summary>
        /// Get player last match.
        /// </summary>
        /// <param name="userIdType">ID type.</param>
        /// <param name="idText">ID text.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">if idText is null.</exception>
        public static async Task<PlayerLastmatch> GetPlayerLastMatchAsync(IdType userIdType, string idText)
        {
            if (idText is null) {
                throw new ArgumentNullException(nameof(idText));
            }

            var ret = userIdType switch {
                IdType.Steam => await AoE2net.GetPlayerLastMatchAsync(idText).ConfigureAwait(false),
                IdType.Profile => await AoE2net.GetPlayerLastMatchAsync(int.Parse(idText)),
                _ => new PlayerLastmatch(),
            };

            foreach (var player in ret.LastMatch.Players) {
                List<PlayerRating> rate;
                var leaderBoardId = ret.LastMatch.LeaderboardId ?? 0;

                if (player.SteamId != null) {
                    rate = await AoE2net.GetPlayerRatingHistoryAsync(player.SteamId, leaderBoardId, 1);
                } else if (player.ProfilId is int profileId) {
                    rate = await AoE2net.GetPlayerRatingHistoryAsync(profileId, leaderBoardId, 1);
                } else {
                    rate = new List<PlayerRating>();
                }

                if (rate.Count != 0) {
                    player.Rating ??= rate[0].Rating;
                } else {
                    player.Rating = null;
                }
            }

            return ret;
        }
    }
}