using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace GTA
{
    public class WeatherChecker : Script
    {
        // Initializer Information
        private TheBeginning m_parentScript;
        private IniFile m_AppSettings = null;

        // Basic shit.
        private readonly Random _random = new Random();

        // App specific. 
        private string url = "http://api.openweathermap.org/data/2.5/weather?q=98665&callback=test&appid=5ee2dddbe59949ba7644ebe906cb00d1";
        private WebClient client;
        private DateTime m_lastDateTime; 

        // DEBUG WINDOW DRAWING CODE
        private const float DEBUGTEXTSCALE = 0.33f;
        private UIText m_UIText = new UIText("", new Point(5, 5), DEBUGTEXTSCALE);
        private UIRectangle m_UIRectangle = new UIRectangle(new Point(5, 5), new Size(425, 450), Color.Black);

        public WeatherChecker(Script mainScript, IniFile appSettings)
        {
            mainScript.Tick += OnTick;
            mainScript.KeyUp += OnKeyUp;
        
            m_parentScript = (TheBeginning)mainScript;
            m_AppSettings = appSettings;

            client = new WebClient();

            m_lastDateTime = System.DateTime.Now;

            m_UIText.Enabled = true;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (m_AppSettings == null)
            {
                m_AppSettings = new IniFile();
            }

            if (m_parentScript.m_bIsModEnabled == true)
            {
                DateTime cur = System.DateTime.Now;
                long elapsedTicks = cur.Ticks - m_lastDateTime.Ticks;
                TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

                if (elapsedSpan.Seconds > (60 * 15)) // 15 mninute interval
                {
                    string content = client.DownloadString(url);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    var jsonContent = serializer.Deserialize<Object>(content);
                }

                //here if I use only jsonContent it returns all data, unfortunately I don t know how to get
                //the specific data
                // return jsonContent.main.humidity;
            }

            if (m_AppSettings.m_bShowDebugPanel && m_parentScript.m_bDebugToggled)
            {
                // int y = 0; UIText txt;
                /*
                m_UIRectangle.Draw();

                String sData = String.Format("Key To Toggle Mod={0}", m_AppSettings.m_keyToggleMod.ToString());
                txt = new UIText(sData, new Point(5, 5 + ((y += 1) * 15)), DEBUGTEXTSCALE, Color.Yellow);
                txt.Draw();

                sData = String.Format("Show Debug Panel = {0}", m_AppSettings.m_bShowDebugPanel.ToString());
                txt = new UIText(sData, new Point(5, 5 + ((y += 1) * 15)), DEBUGTEXTSCALE, Color.Yellow);
                txt.Draw();

                txt = new UIText(m_AppSettings.m_sLastError, new Point(5, 5 + ((y += 1) * 15)), DEBUGTEXTSCALE, Color.Yellow);
                txt.Draw();
                */
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
