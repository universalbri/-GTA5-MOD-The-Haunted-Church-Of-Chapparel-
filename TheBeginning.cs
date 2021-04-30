using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Threading;

namespace GTA
{
    public class TheBeginning : Script
    {
        // Primary Script Classes
        private TheHauntedChurch m_theHauntedChurch;

        // ALWAYS should be the last one called
        public DebugLogger DEBUG;

        private bool m_bDisplayHUD = true;
        private IniFile m_AppSettings = null;

        private readonly Random _random = new Random();

        // DEBUG WINDOW DRAWING CODE
        private const float DEBUGTEXTSCALE = 0.33f;
        private UIText m_UIText = new UIText("", new Point(5, 5), DEBUGTEXTSCALE);
        private UIRectangle m_UIRectangle = new UIRectangle(new Point(5, 5), new Size( 425, 450), Color.Black );

        // GLOBALS
        public bool m_bDebugToggled = false;
        public bool m_bIsModEnabled = false;

        // GTA related
        public Ped m_CurrentPed = null;
        public Entity m_CurrentEntity = null;

        public TheBeginning()
        {
            this.Tick += OnTick;
            this.KeyUp += OnKeyUp;

            // Read the INI file settings
            m_AppSettings = new IniFile();

            // Initialize the Supernatural functions. 
            // This class module should not have ANY OTHER CHANGES when new supernatural functions are added.
            m_theHauntedChurch = new TheHauntedChurch(this, m_AppSettings);
            // m_carAntics = new CarAntics(this, m_AppSettings);

            // Always last
            DEBUG = new DebugLogger(this, m_AppSettings);
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (!m_bDisplayHUD)
                Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME);

            /*if (m_bIsModEnabled == true)
            {
                Vector3 cp = Game.Player.Character.Position;

                if (m_AppSettings.m_bShowDebugPanel && m_bDebugToggled )
                {
                    int y = 0;
                    m_UIRectangle.Draw();
                    UIText txt;

                    String sData = String.Format("Key To Toggle Mod={0}", m_AppSettings.m_keyToggleMod.ToString());
                    txt = new UIText(sData, new Point(5, 5 + ((y += 1) * 15)), DEBUGTEXTSCALE, Color.Yellow);
                    txt = new UIText(m_AppSettings.m_sLastError, new Point(5, 5 + ((y+=1) * 15)), DEBUGTEXTSCALE, Color.Yellow);
                    txt.Draw();
                }
            } */
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.H)
                m_bDisplayHUD = !m_bDisplayHUD;

            if (e.KeyCode == m_AppSettings.m_keyToggleMod)
            {
                m_bIsModEnabled = !m_bIsModEnabled;
                UI.Notify(String.Format("HauntedChurch {0}", m_bIsModEnabled ? "Activated" : "Deactivated"));
            }

            if (m_AppSettings.m_bShowDebugPanel)
            {
                if (e.KeyCode == m_AppSettings.m_keyToggleDebug)
                {
                    m_bDebugToggled = !m_bDebugToggled;
                    UI.Notify(String.Format("HauntedChurch Debug {0}", m_bDebugToggled ? "Activated" : "Deactivated"));
                }
            }
        }
    }
}
