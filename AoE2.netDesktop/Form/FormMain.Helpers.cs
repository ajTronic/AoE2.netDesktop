﻿namespace AoE2NetDesktop.Form;

using AoE2NetDesktop.AoE2DE;
using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility.Forms;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// App main form.
/// </summary>
public partial class FormMain : ControllableForm
{
    private Dictionary<string, Action<PropertySettings>> onChangePropertyHandler;
    private FormSettings formSettings;

    /// <summary>
    /// Gets lastMatchLoader.
    /// </summary>
    public LastMatchLoader LastMatchLoader { get; }

    /// <summary>
    /// Gets Settings.
    /// </summary>
    public CtrlSettings CtrlSettings { get; private set; }

    private void InitOnChangePropertyHandler()
    {
        onChangePropertyHandler = new() {
            { nameof(PropertySettings.ChromaKey), OnChangePropertyChromaKey },
            { nameof(PropertySettings.IsHideTitle), OnChangeIsHideTitle },
            { nameof(PropertySettings.IsAlwaysOnTop), OnChangePropertyIsAlwaysOnTop },
            { nameof(PropertySettings.Opacity), OnChangePropertyOpacity },
            { nameof(PropertySettings.IsTransparency), OnChangePropertyIsTransparency },
            { nameof(PropertySettings.DrawHighQuality), OnChangePropertyDrawHighQuality },
            { nameof(PropertySettings.IsAutoReloadLastMatch), OnChangeIsAutoReloadLastMatch },
        };
    }

    private void SetOptionParams()
    {
        SetChromaKey(CtrlSettings.PropertySetting.ChromaKey);
        OnChangeIsHideTitle(CtrlSettings.PropertySetting);
        TopMost = CtrlSettings.PropertySetting.IsAlwaysOnTop;
        Opacity = CtrlSettings.PropertySetting.Opacity;
        OnChangePropertyIsTransparency(CtrlSettings.PropertySetting);
        OnChangeIsAutoReloadLastMatch(CtrlSettings.PropertySetting);
        DrawEx.DrawHighQuality = CtrlSettings.PropertySetting.DrawHighQuality;
    }

    private void OnChangeProperty(object sender, PropertyChangedEventArgs e)
    {
        onChangePropertyHandler.TryGetValue(e.PropertyName, out Action<PropertySettings> action);
        if(action != null) {
            action.Invoke((PropertySettings)sender);
        } else {
            throw new ArgumentOutOfRangeException($"Invalid {nameof(e.PropertyName)}: {e.PropertyName}");
        }
    }

    private void OnChangePropertyChromaKey(PropertySettings propertySettings)
    {
        SetChromaKey(propertySettings.ChromaKey);
        OnChangePropertyIsTransparency(propertySettings);
    }

    private void OnChangePropertyDrawHighQuality(PropertySettings propertySettings)
    {
        DrawEx.DrawHighQuality = propertySettings.DrawHighQuality;
        Refresh();
    }

    private void OnChangePropertyIsTransparency(PropertySettings propertySettings)
    {
        if(propertySettings.IsTransparency) {
            TransparencyKey = ColorTranslator.FromHtml(CtrlSettings.PropertySetting.ChromaKey);
        } else {
            TransparencyKey = default;
        }
    }

    private void OnChangePropertyOpacity(PropertySettings propertySettings)
    {
        Opacity = propertySettings.Opacity;
    }

    private void OnChangePropertyIsAlwaysOnTop(PropertySettings propertySettings)
    {
        TopMost = propertySettings.IsAlwaysOnTop;
    }

    private void OnChangeIsAutoReloadLastMatch(PropertySettings propertySettings)
    {
        if(propertySettings.IsAutoReloadLastMatch) {
            LastMatchLoader.Start();
        } else {
            LastMatchLoader.Stop();
        }
    }

    private void InitEventHandler()
    {
        foreach(Control item in Controls) {
            foreach(Control panelItem in ((Panel)item).Controls) {
                panelItem.MouseDown += Controls_MouseDown;
                panelItem.MouseMove += Controls_MouseMove;
            }
        }
    }

    private void OnChangeIsHideTitle(PropertySettings propertySettings)
    {
        var top = RectangleToScreen(ClientRectangle).Top;
        var left = RectangleToScreen(ClientRectangle).Left;
        var width = RectangleToScreen(ClientRectangle).Width;

        SuspendLayout();

        if(propertySettings.IsHideTitle && FormBorderStyle != FormBorderStyle.None) {
            MinimumSize = new Size(860, 275);
            FormBorderStyle = FormBorderStyle.None;
            Top = top;
            Left = left;
            Width = width + 13;
        } else if(!propertySettings.IsHideTitle && FormBorderStyle != FormBorderStyle.Sizable) {
            MinimumSize = new Size(860, 310);
            FormBorderStyle = FormBorderStyle.Sizable;
            Top -= RectangleToScreen(ClientRectangle).Top - Top;
            Left -= RectangleToScreen(ClientRectangle).Left - Left;
            Width = width - 13;
        } else {
            // nothing to do.
        }

        ResumeLayout();
    }

    private void RestoreWindowStatus()
    {
        Top = Settings.Default.WindowLocationMain.Y;
        Left = Settings.Default.WindowLocationMain.X;
        Width = Settings.Default.WindowSizeMain.Width;
        Height = Settings.Default.WindowSizeMain.Height;
    }

    private void SaveWindowPosition()
    {
        Settings.Default.WindowLocationMain = new Point(Left, Top);
        Settings.Default.WindowSizeMain = new Size(Width, Height);
    }

    private void ClearLastMatch()
    {
        labelMap.Text = $"Map: -----";
        labelServer.Text = $"Server: -----";
        labelGameId.Text = $"GameID: --------";
        labelAveRate1.Text = $"Team1 Ave. Rate: ----";
        labelAveRate2.Text = $"Team2 Ave. Rate: ----";
        labelErrText.Text = string.Empty;

        pictureBoxMap1v1.Image = null;
        labelMap1v1.Text = string.Empty;
        labelServer1v1.Text = $"Server: -----";
        labelGameId1v1.Text = $"GameID: --------";

        ClearPlayersLabel();
        Refresh();
    }

    private void ClearPlayersLabel()
    {
        foreach(var item in labelCiv) {
            item.Text = "----";
        }

        foreach(var item in labelName) {
            item.Text = "----";
            item.Tag = null;
        }

        foreach(var item in labelRate) {
            item.Text = "----";
        }

        foreach(var item in pictureBox) {
            item.Visible = false;
        }

        label1v1ColorP1.Text = string.Empty;
        labelName1v1P1.Text = string.Empty;
        labelName1v1P1.Tag = null;
        pictureBoxCiv1v1P1.ImageLocation = null;
        pictureBoxUnit1v1P1.Image = null;
        labelRate1v1P1.Text = string.Empty;
        labelWins1v1P1.Text = string.Empty;
        labelLoses1v1P1.Text = string.Empty;
        labelCiv1v1P1.Text = string.Empty;

        label1v1ColorP2.Text = string.Empty;
        labelName1v1P2.Text = string.Empty;
        labelName1v1P2.Tag = null;
        pictureBoxCiv1v1P2.ImageLocation = null;
        pictureBoxUnit1v1P2.Image = null;
        labelRate1v1P2.Text = string.Empty;
        labelWins1v1P2.Text = string.Empty;
        labelLoses1v1P2.Text = string.Empty;
        labelCiv1v1P2.Text = string.Empty;
    }

    private void InitPlayersCtrlList()
    {
        labelCiv.AddRange(new List<Label> {
            labelCivP1, labelCivP2, labelCivP3, labelCivP4,
            labelCivP5, labelCivP6, labelCivP7, labelCivP8,
        });

        labelName.AddRange(new List<Label> {
            labelNameP1, labelNameP2, labelNameP3, labelNameP4,
            labelNameP5, labelNameP6, labelNameP7, labelNameP8,
        });

        labelColor.AddRange(new List<Label> {
            labelColorP1, labelColorP2, labelColorP3, labelColorP4,
            labelColorP5, labelColorP6, labelColorP7, labelColorP8,
        });

        labelRate.AddRange(new List<Label> {
            labelRateP1, labelRateP2, labelRateP3, labelRateP4,
            labelRateP5, labelRateP6, labelRateP7, labelRateP8,
        });

        pictureBox.AddRange(new List<PictureBox> {
            pictureBox1, pictureBox2, pictureBox3, pictureBox4,
            pictureBox5, pictureBox6, pictureBox7, pictureBox8,
        });
    }

    private void OpenSettings()
    {
        if(formSettings == null || formSettings.IsDisposed) {
            formSettings = new FormSettings(CtrlSettings);
        }

        formSettings.Show();
        formSettings.Activate();
    }

    private void ResizePanels()
    {
        const int ctrlMargin = 5;

        panelTeam1.Width = (Width - panelGameInfo.Width - 15) / 2;
        panelTeam2.Width = panelTeam1.Width;
        panelTeam1.Left = ctrlMargin;
        panelTeam2.Left = ctrlMargin + panelTeam1.Width;
        panelTeam2.Top = ctrlMargin;
        panelTeam1.Top = ctrlMargin;

        panelGameInfo.Left = panelTeam2.Left + panelTeam2.Width + ctrlMargin;

        labelErrText.Top = panelTeam1.Top + panelTeam1.Height + ctrlMargin;
        labelErrText.Left = ctrlMargin;
        labelErrText.Width = Width - 22;
        labelErrText.Height = Height - labelErrText.Top - 50;

        panel1v1.Top = ctrlMargin;
        panel1v1.Left = ctrlMargin;
    }

    private void SetChromaKey(string htmlColor)
    {
        Color chromaKey;

        try {
            chromaKey = ColorTranslator.FromHtml(htmlColor);
        } catch(ArgumentException) {
            chromaKey = Color.Empty;
        }

        SetChromaKey(chromaKey);
    }

    private void SetChromaKey(Color chromaKey)
    {
        for(int i = 0; i < AoE2DeApp.PlayerNumMax; i++) {
            labelCiv[i].BackColor = Color.Transparent;
            labelName[i].BackColor = chromaKey;
            labelRate[i].BackColor = chromaKey;
            pictureBox[i].BackColor = chromaKey;
        }

        BackColor = chromaKey;
        panelTeam1.BackColor = chromaKey;
        panelTeam2.BackColor = chromaKey;
        labelAveRate1.BackColor = chromaKey;
        labelAveRate2.BackColor = chromaKey;
        labelMap.BackColor = chromaKey;
        labelGameId.BackColor = chromaKey;
        labelServer.BackColor = chromaKey;

        pictureBoxCiv1v1P1.BackColor = chromaKey;
        pictureBoxCiv1v1P2.BackColor = chromaKey;
        pictureBoxUnit1v1P1.BackColor = chromaKey;
        pictureBoxUnit1v1P2.BackColor = chromaKey;
        pictureBox1v1RateHistoryP1.BackColor = chromaKey;
        pictureBox1v1RateHistoryP2.BackColor = chromaKey;

        labelName1v1P1.BackColor = chromaKey;
        labelName1v1P2.BackColor = chromaKey;
        labelCiv1v1P1.BackColor = chromaKey;
        labelCiv1v1P2.BackColor = chromaKey;
        labelTeamResultP1.BackColor = chromaKey;
        labelTeamResultP2.BackColor = chromaKey;

        labelRate1v1.BackColor = chromaKey;
        labelRate1v1P1.BackColor = chromaKey;
        labelRate1v1P2.BackColor = chromaKey;

        labelWins1v1.BackColor = chromaKey;
        labelWins1v1P1.BackColor = chromaKey;
        labelWins1v1P2.BackColor = chromaKey;

        labelLoses.BackColor = chromaKey;
        labelLoses1v1P1.BackColor = chromaKey;
        labelLoses1v1P2.BackColor = chromaKey;

        labelMap1v1.BackColor = chromaKey;
        pictureBoxMap1v1.BackColor = chromaKey;
        labelGameId1v1.BackColor = chromaKey;
        labelServer1v1.BackColor = chromaKey;
    }

    private void SetMatchData(Match match)
    {
        var aveTeam1 = CtrlMain.GetAverageRate(match.Players, TeamType.OddColorNo);
        var aveTeam2 = CtrlMain.GetAverageRate(match.Players, TeamType.EvenColorNo);
        pictureBoxMap.Image = CtrlMain.LoadMapIcon(match.MapType);
        labelMap.Text = $"Map: {match.GetMapName()}";
        labelServer.Text = $"Server: {match.Server}";
        labelGameId.Text = $"GameID: {match.MatchId}";
        labelAveRate1.Text = $"Team1 Ave. Rate:{aveTeam1}";
        labelAveRate2.Text = $"Team2 Ave. Rate:{aveTeam2}";
    }

    private void SetMatchData1v1(Match match)
    {
        pictureBoxMap1v1.Image = CtrlMain.LoadMapIcon(match.MapType);
        labelMap1v1.Text = match.GetMapName();
        labelServer1v1.Text = $"Server: {match.Server}";
        labelGameId1v1.Text = $"GameID: {match.MatchId}";
    }

    private void SetPlayersData1v1(Player player1, Player player2)
    {
        label1v1ColorP1.Text = player1.GetColorString();
        label1v1ColorP1.BackColor = player1.GetColor();
        labelName1v1P1.Text = CtrlMain.GetPlayerNameString(player1.Name);
        labelName1v1P1.Font = CtrlMain.GetFontStyle(player1, labelName1v1P1.Font);
        labelName1v1P1.Tag = player1;
        pictureBoxCiv1v1P1.ImageLocation = AoE2DeApp.GetCivImageLocation(player1.GetCivEnName());
        pictureBoxUnit1v1P1.Image = UnitImages.Load(player1.GetCivEnName(), player1.GetColor());
        labelRate1v1P1.Text = CtrlMain.GetRateString(player1.Rating);
        labelWins1v1P1.Text = CtrlMain.GetWinsString(player1);
        labelLoses1v1P1.Text = CtrlMain.GetLossesString(player1);
        labelCiv1v1P1.Text = player1.GetCivName();
        labelTeamResultP1.Text = $"";

        label1v1ColorP2.Text = player2.GetColorString();
        label1v1ColorP2.BackColor = player2.GetColor();
        labelName1v1P2.Text = CtrlMain.GetPlayerNameString(player2.Name);
        labelName1v1P2.Font = CtrlMain.GetFontStyle(player2, labelName1v1P2.Font);
        labelName1v1P2.Tag = player2;
        pictureBoxCiv1v1P2.ImageLocation = AoE2DeApp.GetCivImageLocation(player2.GetCivEnName());
        pictureBoxUnit1v1P2.Image = UnitImages.Load(player2.GetCivEnName(), player2.GetColor());
        labelRate1v1P2.Text = CtrlMain.GetRateString(player2.Rating);
        labelWins1v1P2.Text = CtrlMain.GetWinsString(player2);
        labelLoses1v1P2.Text = CtrlMain.GetLossesString(player2);
        labelCiv1v1P2.Text = player2.GetCivName();
        labelTeamResultP2.Text = $"";
    }

    private void SetPlayersData(List<Player> players)
    {
        ClearPlayersLabel();
        foreach(var player in players) {
            if(player.Color - 1 is int index
                && index < AoE2netHelpers.PlayerNumMax
                && index > -1) {
                labelName[index].Text = CtrlMain.GetPlayerNameString(player.Name);
                labelName[index].Font = CtrlMain.GetFontStyle(player, labelName[index].Font);
                labelName[index].Tag = player;
                pictureBox[index].Visible = true;
                pictureBox[index].ImageLocation = AoE2DeApp.GetCivImageLocation(player.GetCivEnName());
                labelRate[index].Text = CtrlMain.GetRateString(player.Rating);
                labelCiv[index].Text = player.GetCivName();
            } else {
                labelErrText.Text = $"invalid player.Color[{player.Color}]";
                break;
            }
        }
    }

    private void OnTimer(object sender, EventArgs e)
    {
        LastMatchLoader.Stop();
        if(CtrlMain.IsAoE2deActive()) {
            labelAoE2DEActive.Invoke(() => { labelAoE2DEActive.Text = "AoE2DE active"; });
            CtrlMain.IsTimerReloading = true;
            Invoke(() => updateToolStripMenuItem.PerformClick());
        } else {
            labelAoE2DEActive.Invoke(() => { labelAoE2DEActive.Text = "AoE2DE NOT active"; });
            LastMatchLoader.Start();
        }

        Awaiter.Complete();
    }

    private async Task<Match> SetLastMatchDataAsync(Match match, LeaderboardId? leaderboard)
    {
        if(match.NumPlayers == 2) {
            var leaderboardP1 = await AoE2net.GetLeaderboardAsync(leaderboard, 0, 1, match.Players[0].ProfilId);
            var leaderboardP2 = await AoE2net.GetLeaderboardAsync(leaderboard, 0, 1, match.Players[1].ProfilId);
            var player1 = leaderboardP1.Leaderboards[0];
            var player2 = leaderboardP2.Leaderboards[0];
            match.Players[0].Games = player1.Games;
            match.Players[1].Games = player2.Games;
            match.Players[0].Wins = player1.Wins;
            match.Players[1].Wins = player2.Wins;

            SetPlayersData1v1(match.Players[0], match.Players[1]);
            SetMatchData1v1(match);
        } else {
            SetPlayersData(match.Players);
            SetMatchData(match);
        }

        return match;
    }

    private async Task<Match> RedrawLastMatchAsync(int profileId)
    {
        Match match = null;
        updateToolStripMenuItem.Enabled = false;

        try {
            var playerLastmatch = await AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Profile, profileId.ToString());
            if(labelGameId.Text != $"GameID: {playerLastmatch.LastMatch.MatchId}") {
                LeaderboardId? leaderboard;
                var playerMatchHistory = await AoE2net.GetPlayerMatchHistoryAsync(0, 1, profileId);
                if(playerMatchHistory.Count != 0
                    && playerMatchHistory[0].MatchId == playerLastmatch.LastMatch.MatchId) {
                    match = playerMatchHistory[0];
                    leaderboard = playerMatchHistory[0].LeaderboardId;
                } else {
                    match = playerLastmatch.LastMatch;
                    leaderboard = playerLastmatch.LastMatch.LeaderboardId;
                }

                match = await SetLastMatchDataAsync(match, leaderboard);
                SwitchView(match);
            }
        } catch(Exception ex) {
            labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
        }

        updateToolStripMenuItem.Enabled = true;
        return match;
    }

    private void SwitchView(Match match)
    {
        if(match.NumPlayers == 2) {
            panel1v1.Visible = true;
            panelGameInfo.Visible = false;
            panelTeam1.Visible = false;
            panelTeam2.Visible = false;
        } else {
            panel1v1.Visible = false;
            panelGameInfo.Visible = true;
            panelTeam1.Visible = true;
            panelTeam2.Visible = true;
        }

        labelDateTime.Text = $"Last match data updated: {DateTime.Now}";
    }
}
