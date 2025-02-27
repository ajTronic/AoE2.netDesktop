﻿namespace AoE2NetDesktop.Form.Tests;

using AoE2NetDesktop.AoE2DE;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.PlotEx;
using AoE2NetDesktop.Tests;

using LibAoE2net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScottPlot;

using System;
using System.Collections.Generic;

[TestClass]
public class WinRatePlotTests
{
    private const int ProfileId = TestData.AvailableUserProfileId;
    private const LeaderboardId TestLeaderboardId = LeaderboardId.RM1v1;
    private readonly PlayerMatchHistory playerMatchHistory = new() {
        new Match() {
            LeaderboardId = TestLeaderboardId,
            MapType = 9,
            Players = new List<Player>() {
                new Player() {
                    ProfilId = ProfileId,
                    Civ = 2,
                    Won = true,
                },
            },
        },
        new Match() {
            LeaderboardId = TestLeaderboardId,
            MapType = 9,
            Players = new List<Player>() {
                new Player() {
                    ProfilId = ProfileId,
                    Civ = 2,
                    Won = false,
                },
            },
        },
        new Match() {
            LeaderboardId = TestLeaderboardId,
            MapType = 9,
            Players = new List<Player>() {
                new Player() {
                    ProfilId = ProfileId,
                    Civ = 2,
                    Won = false,
                },
            },
        },
        new Match() {
            LeaderboardId = TestLeaderboardId,
            MapType = 10,
            Players = new List<Player>() {
                new Player() {
                    ProfilId = ProfileId,
                    Civ = 4,
                    Won = null,
                },
            },
        },
    };

    [ClassInitialize]
    public static void Init(TestContext context)
    {
        if(context is null) {
            throw new ArgumentNullException(nameof(context));
        }

        AoE2net.ComClient = new TestHttpClient();
        StringsExt.Init();
    }

    [TestMethod]
    public void PlotTestMap()
    {
        // Arrange

        // Act
        var testClass = new WinRatePlot(new FormsPlot(), 16);
        testClass.Plot(playerMatchHistory, ProfileId, TestLeaderboardId, DataSource.Map);

        // Assert
        Assert.AreEqual(1, testClass.Values["Arabia"].Lower);
        Assert.AreEqual(2, testClass.Values["Arabia"].Upper);
    }

    [TestMethod]
    public void PlotTestCivilization()
    {
        // Arrange

        // Act
        var testClass = new WinRatePlot(new FormsPlot(), 16);
        testClass.Plot(playerMatchHistory, ProfileId, TestLeaderboardId, DataSource.Civilization);

        // Assert
        Assert.AreEqual(1, testClass.Values["Franks"].Lower);
        Assert.AreEqual(2, testClass.Values["Franks"].Upper);
    }

    [TestMethod]
    public void PlotTestMapPlayerMatchHistoryNull()
    {
        // Arrange

        // Act
        var testClass = new WinRatePlot(new FormsPlot(), 16);

        // Assert
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            testClass.Plot(null, ProfileId, TestLeaderboardId, DataSource.Map);
        });
    }
}
