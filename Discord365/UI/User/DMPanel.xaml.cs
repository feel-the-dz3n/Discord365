﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Discord.Net;
using Discord.Net.WebSockets;
using Discord.WebSocket;

namespace Discord365.UI.User
{
    /// <summary>
    /// Interaction logic for DMPanel.xaml
    /// </summary>
    public partial class DMPanel : UserControl
    {
        public DMPanelButton btnDirect;

        public void SetSelected(DMPanelButton btn)
        {
            foreach (var dm in PanelDM.Children)
            {
                DMPanelButton b = dm as DMPanelButton;

                if (btn == null && b.MarkStyle != DMPanelButton.MarkStyles.NewMessages)
                    b.MarkStyle = DMPanelButton.MarkStyles.None;
                else if (b == btn)
                    b.MarkStyle = DMPanelButton.MarkStyles.SelectedServer;
            }

            foreach (var dm in ServerPanel.Children)
            {
                DMPanelButton b = dm as DMPanelButton;

                if (btn == null && b.MarkStyle != DMPanelButton.MarkStyles.NewMessages)
                    b.MarkStyle = DMPanelButton.MarkStyles.None;
                else if (b == btn)
                    b.MarkStyle = DMPanelButton.MarkStyles.SelectedServer;
            }
        }

        public void AddServer(SocketGuild server)
        {
            DMPanelButton b = new DMPanelButton();

            b.Width = 69;
            b.Height = 50;
            b.Margin = new Thickness(0, 4, 0, 4);

            b.RelatedServer = server;

            ServerPanel.Children.Add(b);
        }

        public void RemoveServer(SocketGuild server)
        {
            foreach(var s in ServerPanel.Children)
            {
                if (((DMPanelButton)s).RelatedServer == server)
                    ServerPanel.Children.Remove((UIElement)s);
            }
        }

        public DMPanel()
        {
            InitializeComponent();

            btnDirect = new DMPanelButton();
            btnDirect.Avatar.ServerName = "Direct Messages";
            btnDirect.Margin = new Thickness(0, 4, 0, 4);
            btnDirect.Width = 69;
            btnDirect.Height = 50;
            btnDirect.MarkStyle = DMPanelButton.MarkStyles.None;
            btnDirect.MouseLeftButtonUp += BtnDirect_MouseLeftButtonUp;

            PanelDM.Children.Add(btnDirect);
        }

        private void BtnDirect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetSelected(btnDirect);

            var header = new DirectMsgsContents.SidebarHeaderSerach();
            var body = new DirectMsgsContents.DirectMessagesSidebarContent();

            App.MainWnd.Sidebar.Set(header, body);
        }
    }
}
