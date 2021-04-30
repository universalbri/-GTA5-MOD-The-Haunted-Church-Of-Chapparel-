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
    public class TheHauntedChurch : Script
    {
        // Initializer Information
        private TheBeginning m_parentScript;
        private IniFile m_AppSettings = null;
        public Dictionary<string, int> m_keyValueRandoms = new Dictionary<string, int>();
        private DateTime m_curDate;

        // Haunted Church Variables >>> abstract this out to a class instance when it gets working. 
        private enum CHURCHHAUNTSTATE
        {
            OFF = 0,
            INTRANSITIONTOON = 1,
            INTRANSITIONTOOFF = 2,
            ON = 3
        }
        private CHURCHHAUNTSTATE m_churchHauntState = CHURCHHAUNTSTATE.OFF;
        private Weather? m_lastWeather = null;
        private List<Ped> m_ZombiePeds = null;

        private readonly Random _random = new Random();

        // DEBUG WINDOW DRAWING CODE
        private const float DEBUGTEXTSCALE = 0.33f;
        private UIText m_UIText = new UIText("", new Point(5, 5), DEBUGTEXTSCALE);
        private UIRectangle m_UIRectangle = new UIRectangle(new Point(5, 5), new Size(425, 450), Color.Black);

        public TheHauntedChurch( Script mainScript, IniFile appSettings )
        {
            mainScript.Tick += OnTick;
            mainScript.KeyUp += OnKeyUp;

            m_parentScript = (TheBeginning)mainScript;

            m_keyValueRandoms.Add("WEATHERRANDOMVALUE", _random.Next(60, 60 * 10));
            m_AppSettings = appSettings;

            m_UIText.Enabled = true;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (m_parentScript.m_bIsModEnabled == true)
            {
                Vector3 cp = Game.Player.Character.Position;

                // The Haunted Church. 
                if ((cp.X < -262.0f && cp.X > -360.0f) &&
                     (cp.Y > 2759.0f && cp.Y < 2865.0f) &&
                     (cp.Z > 51.0f && cp.X < 100.0f))
                {
                    if (m_churchHauntState == CHURCHHAUNTSTATE.OFF)
                    {
                        m_lastWeather = World.Weather;
                        m_curDate = World.CurrentDate;
                        

                        if (m_ZombiePeds == null)
                        {
                            // First, get rid of all the regular NPCs in the area...
                            foreach( Ped ped in World.GetAllPeds() )
                            {
                                if (!ped.IsPlayer)
                                    ped.IsVisible = false;
                            }
                            m_ZombiePeds = new List<Ped>();
                            Ped p;

                            // Top of Shed
                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-295.6023f, 2785.98f, 63.4000f), 64.07613f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;

                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-292.9159f, 2854.712f, 52.99431f), 137.7195f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;
                            m_ZombiePeds.Add(p);

                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-290.808f, 2853.063f, 52.99591f), 146.7446f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;
                            m_ZombiePeds.Add(p);

                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-288.7825f, 2851.981f, 52.95246f), 141.6553f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;
                            m_ZombiePeds.Add(p);

                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-287.0506f, 2851.151f, 52.92916f), 146.0109f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;
                            m_ZombiePeds.Add(p);

                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-286.2937f, 2847.49f, 53.12967f), 155.3977f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;
                            m_ZombiePeds.Add(p);

                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-284.6186f, 2846.301f, 53.11302f), 150.2611f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;
                            m_ZombiePeds.Add(p);

                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-281.8645f, 2844.328f, 53.08746f), 153.7967f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;
                            m_ZombiePeds.Add(p);

                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-278.2011f, 2844.804f, 52.93188f), 155.3977f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;
                            m_ZombiePeds.Add(p);

                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-276.4149f, 2843.145f, 52.79333f), 153.7299f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;
                            m_ZombiePeds.Add(p);

                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-287.1728f, 2837.185f, 54.15338f), 152.2624f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;
                            m_ZombiePeds.Add(p);

                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-289.9335f, 2838.839f, 54.22795f), 146.0584f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;
                            m_ZombiePeds.Add(p);

                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-291.7415f, 2839.834f, 54.2709f), 142.1892f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;
                            m_ZombiePeds.Add(p);

                            p = World.CreatePed(new Model("U_M_Y_Zombie_01"), new Vector3(-293.9919f, 2840.825f, 54.43989f), 145.3246f);
                            p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;
                            m_ZombiePeds.Add(p);

                            /// TODO: NEED TO FIGURE OUT HOW TO GET ZOMBIE / GHOST TO STAY IN SPOOOKY 
                            /// FIXED POSITION AND NOT BE TARGETable. 

                        }

                        World.TransitionToWeather(Weather.ThunderStorm, 5.0f);
                        m_churchHauntState = CHURCHHAUNTSTATE.INTRANSITIONTOON;
                    }
                    else if (m_churchHauntState == CHURCHHAUNTSTATE.INTRANSITIONTOON)
                    {
                        if (World.Weather == Weather.ThunderStorm)
                        {
                            m_churchHauntState = CHURCHHAUNTSTATE.ON;
                            World.CurrentDate = new DateTime(1969, 11, 1, 0, 0, 0);
                        }
                    }
                    // Player steps outside then inside real quick. 
                    else if (m_churchHauntState == CHURCHHAUNTSTATE.INTRANSITIONTOOFF)
                    {
                        if (World.Weather == m_lastWeather)
                        {
                            m_churchHauntState = CHURCHHAUNTSTATE.INTRANSITIONTOON;
                            World.TransitionToWeather(Weather.ThunderStorm, 5.0f);
                        }
                    }
                    else if (m_churchHauntState == CHURCHHAUNTSTATE.ON)
                    {
                        // while in the on state, there's no cars around, no planes, no nothing.... 
                        World.CurrentDate = new DateTime(1969, 11, 1, 0, 0, 0);

                        foreach (Vehicle v in World.GetAllVehicles())
                        {
                            if (v.NumberPlate != Game.Player.LastVehicle.NumberPlate)
                                v.IsVisible = false;
                        }
                        World.SetBlackout(true);
                    }


                }
                else if (m_churchHauntState != CHURCHHAUNTSTATE.OFF)
                {
                    if (m_churchHauntState == CHURCHHAUNTSTATE.ON)
                    {
                        World.TransitionToWeather((Weather)m_lastWeather, 5.0f);
                        m_churchHauntState = CHURCHHAUNTSTATE.INTRANSITIONTOOFF;
                    }
                    else if (m_churchHauntState == CHURCHHAUNTSTATE.INTRANSITIONTOOFF)
                    {
                        if (World.Weather == m_lastWeather)
                        {
                            m_lastWeather = null;
                            if (m_ZombiePeds != null)
                            {
                                foreach (Ped p in m_ZombiePeds)
                                    p.Delete();
                                m_ZombiePeds.Clear();
                                m_ZombiePeds = null;
                                m_churchHauntState = CHURCHHAUNTSTATE.OFF;
                            }
                        }
                        foreach (Vehicle v in World.GetAllVehicles())
                        {
                            if (v.NumberPlate != Game.Player.LastVehicle.NumberPlate)
                                v.IsVisible = true;
                        }

                        // First, get rid of all the regular NPCs in the area...
                        foreach (Ped ped in World.GetAllPeds())
                        {
                            if (!ped.IsPlayer)
                                ped.IsVisible = false;
                        }

                        World.SetBlackout(false);
                        World.CurrentDate = m_curDate;
                    }
                    else if (m_churchHauntState == CHURCHHAUNTSTATE.INTRANSITIONTOON)
                    {
                        if (World.Weather == Weather.ThunderStorm)
                        {
                            World.TransitionToWeather((Weather)m_lastWeather, 5.0f);
                            m_churchHauntState = CHURCHHAUNTSTATE.INTRANSITIONTOOFF;
                        }
                    }
                }

                if (m_AppSettings.m_bShowDebugPanel && m_parentScript.m_bDebugToggled)
                {
                    String sData;
                    sData = "The Haunted Church";
                    m_parentScript.DEBUG.OUT(sData, Color.LightBlue);

                    sData = String.Format("Position(XYZ)=( {0}, {1}, {2} )", Game.Player.Character.Position.X,
                                                                                Game.Player.Character.Position.Y,
                                                                                Game.Player.Character.Position.Z);
                    m_parentScript.DEBUG.OUT(sData, Color.LightBlue);

                    sData = String.Format("Church Haunt State = {0}", m_churchHauntState);
                    m_parentScript.DEBUG.OUT(sData, Color.LightBlue);


                    sData = String.Format("Forward Vector = {0}", Game.Player.Character.ForwardVector);
                    m_parentScript.DEBUG.OUT(sData, Color.LightBlue);
                    m_parentScript.DEBUG.OUT("", Color.LightBlue);
                }
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.O)
            {
                using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(@"D:\\GTA Mods\\Haunted Church\\PointVectors.txt", true))
                {
                    String s; Vector3 p = Game.Player.Character.Position;
                    s = "p = World.CreatePed(new Model(\"U_M_Y_Zombie_01\"), new Vector3({0}f, {1}f, {2}f), {3}f);";
                    file.WriteLine(String.Format(s, p.X, p.Y, p.Z, Game.Player.Character.Heading));
                    file.WriteLine("p.Alpha = 60; p.CanBeTargetted = false; p.FreezePosition = true; p.BlockPermanentEvents = true;");
                    file.WriteLine("m_ZombiePeds.Add(p);");
                    file.WriteLine("");
                }
            }


        }
    }
}
