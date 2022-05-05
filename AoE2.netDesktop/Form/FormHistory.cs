﻿namespace AoE2NetDesktop.Form
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using LibAoE2net;

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
        private async void FormHistory_ShownAsync(object sender, EventArgs e)
        {
            tabControlHistory.SelectedIndex = Settings.Default.SelectedIndexTabControlHistory;
            tabControlHistory.UseWaitCursor = true;

            if (await Controler.ReadPlayerMatchHistoryAsync()) {
                UpdateMatchesTabView();
                UpdateListViewPlayers();
                UpdateStatisticsTabGraph();
                UpdatePlayersTabGraph();
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
}
