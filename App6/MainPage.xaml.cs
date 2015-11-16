using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App6
{
    // mercury, venus, earth, mars, jupiter, saturn, uranus, neptune, pluto
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<string> _planetDesc;
        public MainPage()
        {
            int value;
            this.InitializeComponent();
            setupListsOfDescription();

            try
            {
                // retrieve the selected index and set if it is available
                // 1. get a link to the localsettings container for this app
                ApplicationDataContainer localSettings =
                            ApplicationData.Current.LocalSettings;
                // 2. check for the key value stored under the name
                //    "selectedPlanet"
                //
                // high score
                // name/value pair
                // int i      i = 9;   i/9
                // planet last looked at - "lastPlanet"
                // create a name/value pair for that.
                // only stores a number - selectedIndex from the pivot
                // use that to set the selectedIndex on startup.

                // returned as generic, so cast
                value = (int)localSettings.Values["selectedPlanets"];
                // 3.  if it is there, then set the SelectedIndex of the pivot page
                //     using the value
                pvtPlanets.SelectedIndex = value;





            }
            catch (Exception exc)
            {
                string localMsg = exc.Message;
                try
                {
                    // to get roaming settings.....
                    ApplicationDataContainer roamingSettings =
                        ApplicationData.Current.RoamingSettings;
                    value = (int)roamingSettings.Values["selectedPlanet"];
                    pvtPlanets.SelectedIndex = value;
                }
                catch (Exception exRoaming)
                {
                    // if the value key is not set, then the exception will cause
                    // this code to execute, which just sets the value to 0 (default)
                    string errMsg = exRoaming.Message;
                    pvtPlanets.SelectedIndex = 0;
                } // end inner try for roaming settings
            } // end first try for local settings
            // read list from file
            readFileListcontents();
        }

        private async void readFileListcontents()
        {
            // files for the app are stored in the local folder
            // 
            StorageFolder storageFolder = 
                ApplicationData.Current.LocalFolder;
            
            // create the file and append
            StorageFile sampleFile;
            try
            {
                sampleFile = await storageFolder.GetFileAsync("sample.txt");
            }
            catch (Exception myE)
            {
                string message = myE.Message;
                return;
            }
            string fileText = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            tblVisited.Text = tblVisited.Text + fileText;
        }

        private void setupListsOfDescription()
        {
            //StorageFolder localFolder =
            //    ApplicationData.Current.LocalFolder;
            if (_planetDesc != null)
            {
                return;
            }
            _planetDesc = new List<string>();

            _planetDesc.Add("Mercury is named for the Greco-Roman messenger of the gods. He was very fast, and Mercury has the shortest and fastest orbit around the sun. Mercury orbits the Sun roughly every 88 Earth days. It rotates every 58.6 days, giving it one full day and a fraction of a rotation every Mercury year. Mercury is small.");
            _planetDesc.Add("Venus is the second planet from our sun. Venus was named for the Roman goddess of love. Because it is so close to the Sun and has a shorter orbital time, it is always visible from Earth, usually around sunrise or sunset. Venus is sometimes called Earth’s twin. Its mass is about 80% that of Earth and it has about 85% of Earth’s volume, making it a little less dense than Earth. ");
            _planetDesc.Add("Earth is the third planet from the sun. Earth is the only planet known to harbor life. About 70% of Earth is covered by water. Earth is the only planet known to currently have active plate tectonics. Earth has one moon, sometimes called Luna. Earth’s moon is the largest moon of an inner planet.  The distance of Earth from the sun called one astronomical unit or AU.");
            _planetDesc.Add("Mars is the second smallest planet in our solar system since the demotion of Pluto from planet-hood. Mars is named for the Greco-Roman god of war. Mars has two moons, Phobos and Diemos. Both of these moons are thought to be captured asteroids from the asteroid belt between Jupiter and Mars. Mars does not have liquid water on its surface, but it has deeply etched canyon systems that show that it did once have liquid water.");
            _planetDesc.Add("Jupiter is the largest planet in the solar system, and it was named for the king of the Roman gods. If you combined all of the other planets in the solar system together, Jupiter would still have 2½ times their mass. Jupiter is the closest gas giant to the sun.  Jupiter is called a gas giant, but may have a rocky or solid hydrogen core beneath the never ending storm clouds.");
            _planetDesc.Add("Saturn is the next most massive planet in the solar system after Jupiter. Saturn’s days are just under eleven hours long. However, its year is twenty nine and a half Earth years long.  Saturn is perhaps most famous for its rings. Its rings are held in formation due to the orbits of two small moons.These two moons are said to shepherd the ring material.The gravitational tug of war between the two moons prevents it from coalescing into another moon.");
            _planetDesc.Add("The planet Uranus is named for the father of the god Jupiter. Uranus orbits the sun every 84 Earth years. Uranus is mostly composed of hydrogen with a large percentage of helium. The 2% of its atmosphere that is methane gives it the blue color we see. Uranus rotates on its side relative to the rest of the solar system. The planet’s pole is not parallel to the poles of the Sun and other planets – Uranus’ pole actually points toward the Sun. ");
            _planetDesc.Add("Neptune is the last of the official planets in our solar system and named for the ancient Greco-Roman god of the sea. Methane in Neptune’s atmosphere gives it its deep, dark blue color. Neptune has a blue Great Dark Spot similar to Jupiter’s Great Red Spot. Voyager recorded the fastest winds in the solar system on Neptune. Neptune’s winds near the Great Dark Spot approach 1,200 miles per hour. Neptune’s core is probably a mixture of rock, water, methane and other chemicals.");
            _planetDesc.Add("Pluto was predicted before it was first spotted based on its gravitational effect on Neptune. It was quickly classified as the ninth planet. In the early 2000s, the discovery of a number of dwarf-planets almost as close to the Sun as Pluto caused a major rift in the field of astronomy.  If Pluto was a planet, then dwarf planets such as the newly discovered Eris, Sedna, Quaoar, Makemake and Haumea would also have to be considered planets.");


            int i;
            TextBlock curr; 
            for( i = 0; i <= 8; i++)
            {
                curr = (TextBlock)pvtPlanets.FindName("tblAbout" + i.ToString());
                if ( curr != null )
                {
                    curr.Text = _planetDesc[i];
                }
            }
       }

        private async void pvtPlanets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // when the user changes the planet they are looking at, then reset the
            // key value to store it for when the app starts again.
            //if("Planets Viewed" != pvtPlanets.header)
            if (pvtPlanets.SelectedIndex == 9)
                return;


            // get the current item 
            Pivot curr = (Pivot)sender;

            // 1. get the link to the settings container
            ApplicationDataContainer localSettings =
                        ApplicationData.Current.LocalSettings;
            // 2. just write the value. This overwrites the value, but in this
            //    case, that is not a problem.  If it is, then you may want to check 
            //    whether the value exists or not.
            localSettings.Values["selectedPlanet"] = pvtPlanets.SelectedIndex;


            // to get roaming settings.....
            ApplicationDataContainer roamingSettings =
                ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["selectedPlanet"] = pvtPlanets.SelectedIndex;

            // write out the name of the planet picked as well, just to keep an order on things
            // 
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            // create the file and append
            StorageFile sampleFile;
            string fileText = "";
            try
            {
                sampleFile = await storageFolder.GetFileAsync("sample.txt");
                fileText = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);

            }
            catch (Exception myE)
            {
                string message = myE.Message;
                sampleFile = await storageFolder.CreateFileAsync("sample.txt");
            }
            string myH;

            PivotItem pvtItem = curr.Items[curr.SelectedIndex] as PivotItem;

            myH = pvtItem.Header.ToString();

            
            // file open, now write to it using the writeTextAsync
            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, fileText + myH + System.Environment.NewLine);


            tblVisited.Text = tblVisited.Text + myH + System.Environment.NewLine;


        }
    }
}
