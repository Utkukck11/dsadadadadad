using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace CoreCheat_Reborn.Versions.Panorama
{
    public partial class Launcher : MetroFramework.Forms.MetroForm
    {
        public static int Client;
        public Launcher()
        {
            InitializeComponent();
        }
        private void manuelButton_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    Process[] p = Process.GetProcessesByName("csgo");

                    if (p.Length > 0)
                    {
                        if (GetModuleAddress())
                        {
                            this.Hide();
                            MainOld mainfrm = new MainOld();
                            mainfrm.Show();
                        }
                        else
                        {
                            MessageBox.Show("MODULES ARE NOT LOADED YET! PLEASE TRY AFTER FEW SECONDS! ");
                        }
                    }
                    else
                    {
                        MessageBox.Show("CS:GO NOT OPENED YET!");
                    }
                }
                catch
                {
                    MessageBox.Show("OPEN PROGRAM AS ADMINISTRATOR!");
                    Environment.Exit(0);
                }
            }
        }
        public static bool GetModuleAddress()
        {

            try
            {
                Process[] p = Process.GetProcessesByName("csgo");

                if (p.Length > 0)
                {
                    foreach (ProcessModule m in p[0].Modules)
                    {
                        if (m.ModuleName == SDK.Managers.Modules.ClientDLLName)
                        {
                            Client = (int)m.BaseAddress;
                            return true;
                        }
                    }

                    return true;
                }
                else
                {
                    Console.WriteLine("not found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public void Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
