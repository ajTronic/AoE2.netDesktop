﻿namespace AoE2NetDesktop
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// AoE2net API class.
    /// </summary>
    public class AoE2net
    {
        private static readonly Uri BaseAddress = new Uri(@"https://aoe2.net/api/");
        private static readonly string AoE2Version = "aoe2de";

        /// <summary>
        /// Gets Player Last Match.
        /// Request the last match the player started playing, this will be the current match if they are still in game.
        /// </summary>
        /// <param name="steamId">steam id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<PlayerLastmatch> GetPlayerLastMatchAsync(string steamId)
        {
            var apiEndPoint = $"player/lastmatch?game={AoE2Version}&steam_id={steamId}";
            var playerLastmatch = await GetFromJsonAsync<PlayerLastmatch>(apiEndPoint);

            return playerLastmatch;
        }

        /// <summary>
        /// Gets Player Rating History.
        /// Request the rating history for a player.
        /// </summary>
        /// <param name="steamId">steamID64.</param>
        /// <param name="leaderBoardId">Leaderboard ID.</param>
        /// <param name="count">Number of matches to get (Must be 10000 or less)).</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<List<PlayerRating>> GetPlayerRatingHistoryAsync(string steamId, LeaderBoardId leaderBoardId, int count)
        {
            var apiEndPoint = $"player/ratinghistory?game={AoE2Version}&leaderboard_id={(int)leaderBoardId}&steam_id={steamId}&count={count}";
            var playerRatingHistory = await GetFromJsonAsync<List<PlayerRating>>(apiEndPoint);

            return playerRatingHistory;
        }

        /// <summary>
        /// Send a GET request to the specified end point and return the value
        /// resulting from deserializing the response body as JSON in an asynchronous operation.
        /// </summary>
        /// <param name="apiEndPoint">API end point.</param>
        /// <returns>JSON deserialize object.</returns>
        private static async Task<T> GetFromJsonAsync<T>(string apiEndPoint)
            where T : new()
        {
            var client = new HttpClient() {
                BaseAddress = BaseAddress,
                Timeout = TimeSpan.FromSeconds(20),
            };

            T ret;
            try {
                Debug.Print($"Send Request {BaseAddress}{apiEndPoint}");

                var jsonText = await client.GetStringAsync(apiEndPoint);
                var serializer = new DataContractJsonSerializer(typeof(T));
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
                ret = (T)serializer.ReadObject(stream);

                Debug.Print($"Get JSON {typeof(T)} {jsonText}");
            } catch (HttpRequestException e) {
                Debug.Print($"Request Error: {e.Message}");
                throw;
            } catch (TaskCanceledException e) {
                Debug.Print($"Timeout: {e.Message}");
                throw;
            }

            return ret;
        }
    }
}