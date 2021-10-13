﻿namespace AoE2NetDesktop.Form
{
    using System;
    using LibAoE2net;
    using ScottPlot;

    /// <summary>
    /// Player country graph.
    /// </summary>
    public class PlayerCountryPlot : BarPlotEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerCountryPlot"/> class.
        /// </summary>
        /// <param name="formsPlot">Parent FormsPlot.</param>
        public PlayerCountryPlot(FormsPlot formsPlot)
            : base(formsPlot)
        {
            formsPlot.Configuration.LockHorizontalAxis = true;
            formsPlot.Configuration.LockVerticalAxis = true;
            formsPlot.Plot.Title("Player's country");
            ShowValuesAboveBars = true;
            XMin = -1;
            ItemLabel = "Country";
            ValueLabel = "Game count";
            ItemLabelRotation = 45;
            Orientation = Orientation.Horizontal;
            formsPlot.Render();
        }

        /// <summary>
        /// Plot played player country.
        /// </summary>
        /// <param name="playerMatchHistory">PlayerMatchHistory.</param>
        /// <param name="profileId">profile ID.</param>
        public void Plot(PlayerMatchHistory playerMatchHistory, int profileId)
        {
            if (playerMatchHistory is null) {
                throw new ArgumentNullException(nameof(playerMatchHistory));
            }

            Values.Clear();

            foreach (var match in playerMatchHistory) {
                foreach (var player in match.Players) {
                    var selectedPlayer = match.GetPlayer(profileId);
                    if (player != selectedPlayer) {
                        var country = CountryCode.ConvertToFullName(player.Country);
                        if (!Values.ContainsKey(country)) {
                            var stackedData = new StackedBarGraphData(0, 0);
                            Values.Add(country, stackedData);
                        }

                        Values[country].Lower++;
                    }
                }
            }

            if (Values.Count != 0) {
                Render();
            }
        }
    }
}