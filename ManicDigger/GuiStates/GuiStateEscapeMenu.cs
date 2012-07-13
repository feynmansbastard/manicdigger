﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;

namespace ManicDigger
{
    public class Options
    {
        public bool Shadows;
        public int Font;
        public int DrawDistance = 256;
        public bool UseServerTextures = true;
        public bool EnableSound = true;
        public SerializableDictionary<int, int> Keys = new SerializableDictionary<int, int>();
    }
    partial class ManicDiggerGameWindow
    {
        private void EscapeMenuStart()
        {
            guistate = GuiState.EscapeMenu;
            menustate = new MenuState();
            FreeMouse = true;
            SetEscapeMenuState(EscapeMenuState.Main);
        }
        enum EscapeMenuState
        {
            Main,
            Options,
            Graphics,
            Keys,
            Other,
        }
        EscapeMenuState escapemenustate;
        private void EscapeMenuMouse1()
        {
            foreach (var w in new List<Button>(widgets))
            {
                w.selected = w.Rect.Contains(mouse_current);
                if (w.selected && mouseleftclick)
                {
                    w.InvokeOnClick();
                }
            }
        }
        void SetEscapeMenuState(EscapeMenuState state)
        {
            escapemenustate = state;
            widgets.Clear();
            if (state == EscapeMenuState.Main)
            {
                AddButton(Language.ReturnToGame,(a, b) => { GuiStateBackToGame(); });
                AddButton(Language.Options, (a, b) => { SetEscapeMenuState(EscapeMenuState.Options); });
                AddButton(Language.Exit, (a, b) =>
                {
                    d_Exit.exit = true;
                    this.d_GlWindow.Exit();
                });
                MakeSimpleOptions(20, 50);
            }
            else if (state == EscapeMenuState.Options)
            {
                AddButton(Language.Graphics, (a, b) => { SetEscapeMenuState(EscapeMenuState.Graphics); });
                AddButton(Language.Keys, (a, b) => { SetEscapeMenuState(EscapeMenuState.Keys); });
                AddButton(Language.Other, (a, b) => { SetEscapeMenuState(EscapeMenuState.Other); });
                AddButton(Language.ReturnToMainMenu, (a, b) => { SaveOptions(); SetEscapeMenuState(EscapeMenuState.Main); });
                MakeSimpleOptions(20, 50);
            }
            else if (state == EscapeMenuState.Graphics)
            {
                AddButton(string.Format(Language.ShadowsOption, (d_CurrentShadows.ShadowsFull ? Language.On : Language.Off)),
                    (a, b) =>
                    {
                        d_CurrentShadows.ShadowsFull = !d_CurrentShadows.ShadowsFull;
                        RedrawAllBlocks();
                    });
                AddButton(string.Format(Language.ViewDistanceOption, (d_Config3d.viewdistance)),
                    (a, b) =>
                    {
                        ToggleFog();
                    });
                AddButton(string.Format(Language.UseServerTexturesOption, (options.UseServerTextures ? Language.On : Language.Off)),
                    (a, b) =>
                    {
                        options.UseServerTextures = !options.UseServerTextures;
                    });
                AddButton(string.Format(Language.FontOption, (d_TextRenderer.NewFont ? "2" : "1")),
                    (a, b) =>
                    {
                        d_TextRenderer.NewFont = !d_TextRenderer.NewFont;
                        d_The3d.cachedTextTextures.Clear();
                    });
                AddButton(Language.ReturnToOptionsMenu, (a, b) => { SetEscapeMenuState(EscapeMenuState.Options); });
                MakeSimpleOptions(20, 50);
            }
            else if (state == EscapeMenuState.Other)
            {
                AddButton(string.Format(Language.SoundOption, (d_Audio.Enabled ? Language.On : Language.Off)),
                    (a, b) =>
                    {
                        d_Audio.Enabled = !d_Audio.Enabled;
                    });
                AddButton(Language.ReturnToOptionsMenu, (a, b) => { SetEscapeMenuState(EscapeMenuState.Options); });
                MakeSimpleOptions(20, 50);
            }
            else if (state == EscapeMenuState.Keys)
            {
                int fontsize = 12;
                int textheight = 20;
                for (int i = 0; i < keyhelps.Length; i++)
                {
                    int ii = i; //a copy for closure
                    int defaultkey = keyhelps[i].DefaultKey;
                    int key = defaultkey;
                    if (options.Keys.ContainsKey(defaultkey))
                    {
                        key = options.Keys[defaultkey];
                    }
                    AddButton(string.Format(Language.KeyChange, keyhelps[i].Text, KeyName(key)), (a, b) => { keyselectid = ii; });
                }
                AddButton(Language.DefaultKeys, (a, b) => { options.Keys.Clear(); });
                AddButton(Language.ReturnToOptionsMenu, (a, b) => { SetEscapeMenuState(EscapeMenuState.Options); });
                MakeSimpleOptions(fontsize, textheight);
            }
        }
        private string KeyName(int key)
        {
            if (Enum.IsDefined(typeof(OpenTK.Input.Key), key))
            {
                string s = Enum.GetName(typeof(OpenTK.Input.Key), key);
                return s;
            }
            if (Enum.IsDefined(typeof(SpecialKey), key))
            {
                string s = Enum.GetName(typeof(SpecialKey), key);
                return s;
            }
            return key.ToString();
        }
        void AddButton(string text, EventHandler e)
        {
            Button b = new Button();
            b.Text = text;
            b.OnClick += e;
            widgets.Add(b);
        }
        void MakeSimpleOptions(int fontsize, int textheight)
        {
            int starty = ycenter(widgets.Count * textheight);
            for (int i = 0; i < widgets.Count; i++)
            {
                string s = widgets[i].Text;
                Rectangle rect = new Rectangle();
                SizeF size = d_The3d.TextSize(s, fontsize);
                rect.Width = (int)size.Width + 10;
                rect.Height = (int)size.Height;
                rect.X = xcenter(size.Width);
                rect.Y = starty + textheight * i;
                widgets[i].Rect = rect;
                widgets[i].fontsize = fontsize;
                if (i == keyselectid)
                {
                    widgets[i].fontcolor = Color.Green;
                    widgets[i].fontcolorselected = Color.Green;
                }
            }
        }
        void EscapeMenuDraw()
        {
            SetEscapeMenuState(escapemenustate);
            EscapeMenuMouse1();
            foreach (var w in widgets)
            {
                d_The3d.Draw2dText(w.Text, w.Rect.X, w.Rect.Y, w.fontsize, w.selected ? w.fontcolorselected : w.fontcolor);
            }
        }
        List<Button> widgets = new List<Button>();
        class Button
        {
            public Rectangle Rect;
            public string Text;
            public event EventHandler OnClick;
            public bool selected;
            public int fontsize = 20;
            public Color fontcolor = Color.White;
            public Color fontcolorselected = Color.Red;
            public void InvokeOnClick()
            {
                OnClick(this, new EventArgs());
            }
        }
        class KeyHelp
        {
            public string Text;
            public int DefaultKey;
        }
        enum SpecialKey
        {
            MouseLeftClick = 200,
            MouseRightClick = 201,
        }
        KeyHelp[] keyhelps = new KeyHelp[]
        {
            new KeyHelp(){Text=Language.KeyMoveFoward, DefaultKey=(int)OpenTK.Input.Key.W},
            new KeyHelp(){Text=Language.KeyMoveBack, DefaultKey=(int)OpenTK.Input.Key.S},
            new KeyHelp(){Text=Language.KeyMoveLeft, DefaultKey=(int)OpenTK.Input.Key.A},
            new KeyHelp(){Text=Language.KeyMoveRight, DefaultKey=(int)OpenTK.Input.Key.D},
            new KeyHelp(){Text=Language.KeyJump, DefaultKey=(int)OpenTK.Input.Key.Space},
            //new KeyHelp(){Text="Remove block", DefaultKey=(int)SpecialKey.MouseLeftClick},
            //new KeyHelp(){Text="Place block", DefaultKey=(int)SpecialKey.MouseRightClick},
            new KeyHelp(){Text=Language.KeyShowMaterialSelector, DefaultKey=(int)OpenTK.Input.Key.B},
            new KeyHelp(){Text=Language.KeySetSpawnPosition, DefaultKey=(int)OpenTK.Input.Key.P},
            new KeyHelp(){Text=Language.KeyRespawn, DefaultKey=(int)OpenTK.Input.Key.R},
            new KeyHelp(){Text=Language.KeyToggleFogDistance, DefaultKey=(int)OpenTK.Input.Key.F},
            new KeyHelp(){Text=string.Format(Language.KeyMoveSpeed, "1"), DefaultKey=(int)OpenTK.Input.Key.F1},
            new KeyHelp(){Text=string.Format(Language.KeyMoveSpeed, "10"), DefaultKey=(int)OpenTK.Input.Key.F2},
            new KeyHelp(){Text=Language.KeyFreeMove, DefaultKey=(int)OpenTK.Input.Key.F3},
            new KeyHelp(){Text=Language.KeyThirdPersonCamera, DefaultKey=(int)OpenTK.Input.Key.F5},
            new KeyHelp(){Text=Language.KeyFullscreen, DefaultKey=(int)OpenTK.Input.Key.F11},
            new KeyHelp(){Text=Language.KeyScreenshot, DefaultKey=(int)OpenTK.Input.Key.F12},
            new KeyHelp(){Text=Language.KeyPlayersList, DefaultKey=(int)OpenTK.Input.Key.Tab},
            new KeyHelp(){Text=Language.KeyChat, DefaultKey=(int)OpenTK.Input.Key.Enter},
            new KeyHelp(){Text="Unload blocks", DefaultKey=(int)OpenTK.Input.Key.U},
            new KeyHelp(){Text="Craft", DefaultKey=(int)OpenTK.Input.Key.C},
            new KeyHelp(){Text="Load blocks", DefaultKey=(int)OpenTK.Input.Key.L},
            new KeyHelp(){Text="Enter/leave minecart", DefaultKey=(int)OpenTK.Input.Key.V},
            new KeyHelp(){Text=Language.KeyReverseMinecart, DefaultKey=(int)OpenTK.Input.Key.Q},
            //new KeyHelp(){Text="Swap mouse up-down", BoolId="SwapMouseUpDown"},
        };
        int keyselectid = -1;
        private void EscapeMenuKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == GetKey(OpenTK.Input.Key.Escape))
            {
                if (escapemenustate == EscapeMenuState.Graphics
                    || escapemenustate == EscapeMenuState.Keys
                    || escapemenustate == EscapeMenuState.Other)
                {
                    SetEscapeMenuState(EscapeMenuState.Options);
                }
                else if (escapemenustate == EscapeMenuState.Options)
                {
                    SetEscapeMenuState(EscapeMenuState.Main);
                }
                else
                {
                    SetEscapeMenuState(EscapeMenuState.Main);
                    GuiStateBackToGame();
                }
            }
            if (escapemenustate == EscapeMenuState.Keys)
            {
                if (keyselectid != -1)
                {
                    options.Keys[keyhelps[keyselectid].DefaultKey] = (int)e.Key;
                    keyselectid = -1;
                }
            }
        }
        Options options = new Options();
        XmlSerializer x = new XmlSerializer(typeof(Options));
        public string gamepathconfig = GameStorePath.GetStorePath();
        string filename = "ClientConfig.xml";
        void LoadOptions()
        {
            string path = Path.Combine(gamepathconfig, filename);
            if (!File.Exists(path))
            {
                return;
            }
            string s = File.ReadAllText(path);
            this.options = (Options)x.Deserialize(new System.IO.StringReader(s));

            d_TextRenderer.NewFont = options.Font != 1;
            d_CurrentShadows.ShadowsFull = options.Shadows;
            d_Shadows.ResetShadows();
            //d_Terrain.UpdateAllTiles();
            d_Config3d.viewdistance = options.DrawDistance;
            d_Audio.Enabled = options.EnableSound;
        }
        void SaveOptions()
        {
            options.Font = d_TextRenderer.NewFont ? 0 : 1;
            options.Shadows = d_CurrentShadows.ShadowsFull;
            options.DrawDistance = (int)d_Config3d.viewdistance;
            options.EnableSound = d_Audio.Enabled;
            
            string path = Path.Combine(gamepathconfig, filename);
            MemoryStream ms = new MemoryStream();
            x.Serialize(ms, options);
            string xml = Encoding.UTF8.GetString(ms.ToArray());
            File.WriteAllText(path, xml);
        }
    }
}