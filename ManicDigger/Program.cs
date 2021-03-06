﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using ManicDigger;
using ManicDigger.Renderers;
using ManicDiggerServer;
using System.Text;
using System.Net.Sockets;
using Lidgren.Network;
using ManicDigger.ClientNative;
#endregion

namespace GameModeFortress
{
    public class ManicDiggerProgram
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new ManicDiggerProgram(args);
        }
        public ManicDiggerProgram(string[] args)
        {
            dummyNetwork = new DummyNetwork();
            dummyNetwork.Start(new MonitorObject(), new MonitorObject());
            crashreporter = new CrashReporter();
            crashreporter.Start(delegate { Start(args); });
        }
        CrashReporter crashreporter;
        private void Start(string[] args)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            if (!Debugger.IsAttached)
            {
                System.Environment.CurrentDirectory = appPath;
            }
            bool IsSinglePlayer;
            string singleplayerpath;

            bool MenuResultSinglePlayer = false;
            ConnectData MenuResultMenuConnectData = null;
            string MenuResultSavegamePath = null;

            ConnectData connectdata = new ConnectData();
            if (args.Length > 0)
            {
                if (args[0].EndsWith(".mdlink", StringComparison.InvariantCultureIgnoreCase))
                {
                    XmlDocument d = new XmlDocument();
                    d.Load(args[0]);
                    string mode = XmlTool.XmlVal(d, "/ManicDiggerLink/GameMode");
                    if (mode != "Fortress")
                    {
                        throw new Exception("Invalid game mode: " + mode);
                    }
                    connectdata.SetIp(XmlTool.XmlVal(d, "/ManicDiggerLink/Ip"));
                    int port = int.Parse(XmlTool.XmlVal(d, "/ManicDiggerLink/Port"));
                    connectdata.SetPort(port);
                    connectdata.SetUsername(XmlTool.XmlVal(d, "/ManicDiggerLink/User"));
                    connectdata.SetIsServePasswordProtected(Misc.ReadBool(XmlTool.XmlVal(d, "/ManicDiggerLink/PasswordProtected")));
                    IsSinglePlayer = false;
                    singleplayerpath = null;
                }
                else
                {
                    connectdata = ConnectData.FromUri(new GamePlatformNative().ParseUri(args[0]));
                    IsSinglePlayer = false;
                    singleplayerpath = null;
                }
            }
            else
            {
                try
                {
                    if (File.Exists("cito.txt"))
                    {
                        MainMenu mainmenu = new MainMenu();
                        GamePlatformNative platform = new GamePlatformNative();
                        OpenTK.Graphics.GraphicsMode mode = new OpenTK.Graphics.GraphicsMode(new OpenTK.Graphics.ColorFormat(32), 24, 0, 2, new OpenTK.Graphics.ColorFormat(32));
                        using (GameWindowNative game = new GameWindowNative(mode))
                        {
                            platform.window = game;
                            game.platform = platform;
                            mainmenu.Start(platform);
                            platform.Start();
                            //g.Start();
                            game.Run(60.0);
                        }
                        MenuResultSinglePlayer = mainmenu.GetMenuResultSinglePlayer();
                        MenuResultMenuConnectData = mainmenu.GetMenuResultMenuConnectData();
                        MenuResultSavegamePath = mainmenu.GetMenuResultSavegamePath();
                    }
                }
                catch
                {
                }
                //new Thread(ServerThreadStart).Start();
                //p.GameUrl = "127.0.0.1:25570";
                //p.User = "Local";
              //  Menu form = new Menu();
              //  Application.Run(form);
              //  if (form.Chosen == ChosenGameType.None)
              //  {
              //      return;
              //  }
              //  IsSinglePlayer = form.Chosen == ChosenGameType.Singleplayer;
                if (MenuResultSinglePlayer)
                {
                    if (MenuResultSavegamePath == null)
                    {
                        return;
                    }
                    singleplayerpath = MenuResultSavegamePath;
                    connectdata.SetIsServePasswordProtected(false);
                }
                else
                {
                    if (MenuResultMenuConnectData == null)
                    {
                        return;
                    }
                    connectdata = MenuResultMenuConnectData;
                    singleplayerpath = null;
                }
            }
            savefilename = singleplayerpath;

            string serverPassword = "";
            //if (connectdata.GetIsServePasswordProtected())
            //{
            //    PasswordForm passwordForm = new PasswordForm();
            //    DialogResult dialogResult = passwordForm.ShowDialog();

            //    if (dialogResult == DialogResult.OK)
            //    {
            //        serverPassword = passwordForm.Password;
            //    }
            //    if (dialogResult == DialogResult.Cancel)
            //    {
            //        // TODO: go back to main menu
            //        throw new Exception();
            //    }
            //}
            connectdata.SetServerPassword(serverPassword);
            StartGameWindowAndConnect(MenuResultSinglePlayer, connectdata, singleplayerpath);
        }

        void StartGameWindowAndConnect(bool issingleplayer, ConnectData connectdata, string singleplayersavepath)
        {
            if (issingleplayer)
            {
                new Thread(ServerThreadStart).Start();
                connectdata.SetUsername("Local");
            }
        restart:
            ManicDiggerGameWindow w = new ManicDiggerGameWindow();
            w.issingleplayer = issingleplayer;
            this.curw = w;
            if (issingleplayer)
            {
                DummyNetClient netclient = new DummyNetClient();
                netclient.SetPlatform(new GamePlatformNative());
                netclient.SetNetwork(dummyNetwork);
                w.main = netclient;
            }
            else
            {
                var config = new NetPeerConfiguration("ManicDigger");
                //w.main = new MyNetClient() { client = new NetClient(config) };
                //w.main = new TcpNetClient() { };
                EnetNetClient client = new EnetNetClient();
                client.SetPlatform(new GamePlatformNative());
                w.main = client;
            }
            var glwindow = new GlWindow(w);
            w.d_GlWindow = glwindow;
            w.d_Exit = exit;
            w.connectdata = connectdata;
            GamePlatformNative platform = new GamePlatformNative() { window = w.d_GlWindow };
            platform.SetExit(exit);
            w.game.SetPlatform(platform);
             ((GamePlatformNative)w.game.GetPlatform()).crashreporter = crashreporter;
            w.Start();
            w.Run();
            if (w.reconnect)
            {
                goto restart;
            }
            exit.SetExit(true);
        }
        ManicDiggerGameWindow curw;

        DummyNetwork dummyNetwork;

        string savefilename;
        public GameExit exit = new GameExit();
        //bool StartedSinglePlayerServer = false;
        public void ServerThreadStart()
        {
            try
            {
                Server server = new Server();
                server.SaveFilenameOverride = savefilename;
                server.exit = exit;
                DummyNetServer netServer = new DummyNetServer();
                netServer.SetPlatform(new GamePlatformNative());
                netServer.SetNetwork(dummyNetwork);
                server.d_MainSocket = netServer;
                server.Start();
                for (; ; )
                {
                    server.Process();
                    Thread.Sleep(1);
                    while (curw == null)
                    {
                        Thread.Sleep(1);
                    }
                    curw.StartedSinglePlayerServer = true;
                    if (exit != null && exit.GetExit()) { server.SaveAll(); return; }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
