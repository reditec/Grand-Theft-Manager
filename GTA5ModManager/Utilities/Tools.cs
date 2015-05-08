using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using GTA5ModManager.Properties;
using ThreadState = System.Threading.ThreadState;

namespace GTA5ModManager.Utilities
{
    public static class Tools
    {
        // A sort of nullable boolean
        public enum Boolstate
        {
            True,
            False,
            Indeterminate
        }

        private static bool HasInternetConnection
        {
            //There is absolutely no way you can reliably check if there is an internet connection
            get
            {
                var result = false;

                try
                {
                    if (NetworkInterface.GetIsNetworkAvailable())
                    {
                        using (var p = new Ping())
                        {
                            result = (p.Send("8.8.8.8", 15000).Status == IPStatus.Success) ||
                                     (p.Send("8.8.4.4", 15000).Status == IPStatus.Success) ||
                                     (p.Send("4.2.2.1", 15000).Status == IPStatus.Success);
                        }
                    }
                }
                catch
                {
                   
                }

                return result;
            }
        }

        public static List<string> StartupParameters
        {
            get
            {
                try
                {
                    var startup_parameters_mixed = new List<string>();
                    startup_parameters_mixed.AddRange(Environment.GetCommandLineArgs());

                    var startup_parameters_lower = new List<string>();
                    foreach (var s in startup_parameters_mixed)
                        startup_parameters_lower.Add(s.Trim().ToLower());

                    startup_parameters_mixed.Clear();

                    return startup_parameters_lower;
                }
                catch
                {
                    try
                    {
                        return new List<string>(Environment.GetCommandLineArgs());
                    }
                    catch
                    {
                    }
                }

                return new List<string>();
            }
        }

        public static void GotoSite(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
            }
        }

        public static void CheckForUpdates()
        {
            if (HasInternetConnection)
            {
                try
                {
                    var _releasePageURL = "";
                    Version _newVersion = null;
                    const string _versionConfig =
                        "https://raw.githubusercontent.com/Codeusa/Grand-Theft-Manager/master/version.xml";
                    var _reader = new XmlTextReader(_versionConfig);
                    _reader.MoveToContent();
                    var _elementName = "";
                    try
                    {
                        if ((_reader.NodeType == XmlNodeType.Element) && (_reader.Name == "gta5"))
                        {
                            while (_reader.Read())
                            {
                                switch (_reader.NodeType)
                                {
                                    case XmlNodeType.Element:
                                        _elementName = _reader.Name;
                                        break;
                                    default:
                                        if ((_reader.NodeType == XmlNodeType.Text) && (_reader.HasValue))
                                        {
                                            switch (_elementName)
                                            {
                                                case "version":
                                                    _newVersion = new Version(_reader.Value);
                                                    break;
                                                case "url":
                                                    _releasePageURL = _reader.Value;
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(Resources.ErrorUpdates, Resources.ErrorHeader, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    finally
                    {
                        _reader.Close();
                    }

                    var applicationVersion = Assembly.GetExecutingAssembly().GetName().Version;
                    Console.WriteLine(applicationVersion);
                    Console.WriteLine(_newVersion);
                    if (applicationVersion.CompareTo(_newVersion) < 0)
                    {
                        if (MessageBox.Show(Resources.InfoUpdateAvailable, Resources.InfoUpdatesHeader,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            GotoSite(_releasePageURL);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        ///     Gets the smallest Rectangle containing two input Rectangles
        /// </summary>
        public static Rectangle GetContainingRectangle(Rectangle a, Rectangle b)
        {
            var amin = new Point(a.X, a.Y);
            var amax = new Point(a.X + a.Width, a.Y + a.Height);
            var bmin = new Point(b.X, b.Y);
            var bmax = new Point(b.X + b.Width, b.Y + b.Height);
            var nmin = new Point(0, 0);
            var nmax = new Point(0, 0);

            nmin.X = (amin.X < bmin.X) ? amin.X : bmin.X;
            nmin.Y = (amin.Y < bmin.Y) ? amin.Y : bmin.Y;
            nmax.X = (amax.X > bmax.X) ? amax.X : bmax.X;
            nmax.Y = (amax.Y > bmax.Y) ? amax.Y : bmax.Y;

            return new Rectangle(nmin, new Size(nmax.X - nmin.X, nmax.Y - nmin.Y));
        }

        public static void StartMethodAndWait(Action target, double dHowLongToWait)
        {
            try
            {
                ThreadStart tsGenericMethod = () =>
                {
                    try
                    {
                        target();
                    }
                    catch
                    {
                    }
                };
                var trdGenericThread = new Thread(tsGenericMethod);
                trdGenericThread.IsBackground = false;
                trdGenericThread.Start();

                var dtStartTime = DateTime.Now;

                for (;;)
                {
                    if (dHowLongToWait > 0.0)
                    {
                        if ((DateTime.Now - dtStartTime).TotalMilliseconds > dHowLongToWait)
                        {
                            try
                            {
                                trdGenericThread.Abort();
                            }
                            catch
                            {
                            }
                            break;
                        }
                    }

                    if (trdGenericThread.ThreadState == ThreadState.Stopped) break;
                    if (trdGenericThread.ThreadState == ThreadState.StopRequested) break;
                    if (trdGenericThread.ThreadState == ThreadState.Aborted) break;
                    if (trdGenericThread.ThreadState == ThreadState.AbortRequested) break;

                    Thread.Sleep(15);
                }
            }
            catch
            {
            }
        }

        public static void StartMethodMultithreaded(Action target)
        {
            ThreadStart tsGenericMethod = () =>
            {
                try
                {
                    target();
                }
                catch
                {
                }
            };
            var trdGenericThread = new Thread(tsGenericMethod);
            trdGenericThread.IsBackground = true;
            trdGenericThread.Start();
        }
    }
}