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
    public class Template : Script
    {
        // Initializer Information
        private TheBeginning m_parentScript;
        private IniFile m_AppSettings = null;

        // Basic shit.
        private readonly Random _random = new Random();

        public Template(Script mainScript, IniFile appSettings)
        {
            mainScript.Tick += OnTick;
            mainScript.KeyUp += OnKeyUp;

            m_parentScript = (TheBeginning)mainScript;
            m_AppSettings = appSettings;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (m_AppSettings == null)
            {
                m_AppSettings = new IniFile();
            }

            if (m_parentScript.m_bIsModEnabled == true)
            {
                // Do Supernatural functions here. 
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
            }
        }
    }
}
