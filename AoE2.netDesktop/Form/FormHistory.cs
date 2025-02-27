﻿namespace AoE2NetDesktop.Form;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.Utility;
using AoE2NetDesktop.Utility.Forms;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// FormHistory class.
/// </summary>
public partial class FormHistory : ControllableForm
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FormHistory"/> class.
    /// </summary>
    /// <param name="profileId">user profile ID.</param>
    public FormHistory(int profileId)
        : base(new CtrlHistory(profileId))
    {
        InitializeComponent();
        InitMatchesTab();
        InitPlayersTab();
        InitStatisticsTab();
        Icon = Properties.Resources.aoe2netDesktop;
    }

    /// <inheritdoc/>
    protected override CtrlHistory Controler => (CtrlHistory)base.Controler;

    private static void SortByColumn(ListView listView, ColumnClickEventArgs e)
    {
        var listViewItemComparer = (ListViewItemComparer)listView.ListViewItemSorter;
        listViewItemComparer.Column = e.Column;
        listView.Sort();
    }

    private void SaveWindowPosition()
    {
        Settings.Default.WindowLocationHistory = new Point(Left, Top);
        Settings.Default.WindowSizeHistory = new Size(Width, Height);
    }

    private void RestoreWindowPosition()
    {
        Top = Settings.Default.WindowLocationHistory.Y;
        Left = Settings.Default.WindowLocationHistory.X;
        Width = Settings.Default.WindowSizeHistory.Width;
        Height = Settings.Default.WindowSizeHistory.Height;
    }

    ///////////////////////////////////////////////////////////////////////
    // event handlers
    ///////////////////////////////////////////////////////////////////////
    [SuppressMessage("Usage", "VSTHRD100:Avoid async void methods", Justification = SuppressReason.GuiEvent)]
    [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = SuppressReason.GuiEvent)]
    private async void FormHistory_ShownAsync(object sender, EventArgs e)
    {
        tabControlHistory.SelectedIndex = Settings.Default.SelectedIndexTabControlHistory;
        tabControlHistory.UseWaitCursor = true;

        if(await Controler.ReadPlayerMatchHistoryAsync()) {
            UpdateMatchesTabView();
            UpdateStatisticsTabGraph();
            UpdatePlayersTabListView();
            UpdatePlayersTabGraph();
            UpdatePlayersTabListViewFilterCountory();
        } else {
            Debug.Print("ReadPlayerMatchHistoryAsync ERROR.");
        }

        tabControlHistory.UseWaitCursor = false;

        await UpdateListViewStatisticsAsync();

        UseWaitCursor = false;
        Awaiter.Complete();
    }

    private void FormHistory_FormClosing(object sender, FormClosingEventArgs e)
    {
        SaveWindowPosition();
        Settings.Default.Save();
    }

    private void FormHistory_Load(object sender, EventArgs e)
    {
        RestoreWindowPosition();
    }

    private void TabControlHistory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Settings.Default.SelectedIndexTabControlHistory = tabControlHistory.SelectedIndex;
    }
}
