


// Adventure game: Make a game with multiple option that display different pages and eventually let the player "win" or "lose".

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.ComponentModel.Com2Interop;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Xml;
using System.Security.Cryptography;
using System.Media;
using Adventure_game___Deep_Rock_Galactic.Properties;

namespace Adventure_game___Deep_Rock_Galactic
{
    //  You have been Recruited for Deep Rock Galactic's Scout team, You are taking on a new planet alone, what will you do?
    
    public partial class Form1 : Form
    {
        // initialize page number, and shop items & etc.

        int page, moreAmmo = 0, gameAmmo = 0, lightGear = 0, leechRepellant = 0, fossil = 0, pathRouter = 0;

        //random generator

        Random randGen = new Random();

        //random generator variables

        int runAway = 0, havoc = 0, hogWarts, encounter = 0;

        public Form1()
        {
            InitializeComponent();

            // set the screen to initialize page 1

            startupText.Text = $"You took on the Role of Scout  and wish to preform well for Management, You are assigned with Two Missions, Your " +
                $"First Mission is to Explore the Depths of the Planet or Collect Alien Fossils, afterwards you are to return alive." +
                $"\n\nAre you up to the task?";

            //embeded image change 

            mainDisplay.BackgroundImage = (Properties.Resources.page_1);


            // Display initial message and options

            outputLabel.Text = $"You're on your Space Rig. It is almost time for mission launch, How do you Prepare?";

            option1Label.Text = "Pursue your Mission";
            option2Label.Text = "Cause Havoc";
            option3Label.Text = "Check Shop";

        }
        private void startButton_Click(object sender, EventArgs e)
        {
            //play sound and state soundplayer

            // this is to display the game screen after reading the prompt

            startupText.Text = $"";
            page = 1;


            // hide start screen

            startScreenBox.Visible = false;
            startButton.Visible = false;
            startupText.Visible = false;
            startupText.Visible = false;

            //state sound player

            SoundPlayer player = new SoundPlayer();

            player = new SoundPlayer(Properties.Resources.buttonSound);

            player.Play();

        }
        private void option1Label_Click(object sender, EventArgs e)
        {
            // large if, else if statement to hold where the player can go

            if (page == 1)
            {
                // pursue your mission, assign ammo for the player.
                if (moreAmmo == 0)
                {
                    gameAmmo = 2;

                    page = 5;
                }
                else
                {
                    gameAmmo = 5;

                    page = 5;
                }
            }
            else if (page == 5)
            {
                //send them to encounter generator
                fossilVoid();
            }
            else if (page == 6)
            {
                gameAmmo--;
                if (gameAmmo == -1)
                {
                    page = 27;
                }
                else
                {
                    //Correctly display how many attacks you will survive if you wish to fight the enemies
                    if (gameAmmo == 0)
                    {
                        outputLabel.Text = $"You Were Able to Fight off the Glyphids. " +
                            $"Unfortuntely, you are out of Ammo now. \n\nWhats Next?";
                        page = 16;
                    }
                    else
                    {
                        outputLabel.Text = $"You Were Able to Fight off the Glyphids, at the expense of 1 Ammo," +
                            $" you have enough for {gameAmmo} more attacks. \n\nWhats Next?";
                        page = 16;
                    }
                }
            }
            else if (page == 9)
            {
                page = 17;
            }
            else if (page == 2)
            {
                // grant Leech Repellant and remove others
                lightGear = 0;
                moreAmmo = 0;
                leechRepellant = 1;
                page = 1;

                //declare sound player

                SoundPlayer player = new SoundPlayer();

                player = new SoundPlayer(Properties.Resources.leechRepellantNoise);

                player.Play();

                Refresh();
                Thread.Sleep(200);
            }

            // if leech repellant is on, you can survive this page

            else if (page == 17)
            {
                if (leechRepellant == 0)
                {
                    page = 24;
                }
                else
                {
                    page = 22;
                }
            }

            // have a chance to survive even if not enough ammo

            else if (page == 15)
            {
                // have a chance to beat the dreadnought if enough ammo
                if (gameAmmo > 4)
                {
                    page = 26;
                }
                else
                {
                    page = 18;

                }
            }
            else if (page == 18)
            {
                // you have decided to give up against the dreadnought
                outputLabel.Text = "You Kneel over and allow the dreadnought to have an early feast!";
                page = 19;
            }
            else if (page == 20)
            {
                // you decided to mine the ommarune!
                page = 21;
            }
            else if (page == 13)
            {
                // you have decided to eat more mushrooms

                page = 8;
            }
            else if (page == 16)
            {
                // send to encounter generator

                fossilVoid();
            }
            else if (page == 8)
            {
                //chance to go to hogwarts after eating mushrooms

                //hogwarts random gen

                hogWarts = randGen.Next(1, 100);

                //if statement

                if (hogWarts > 25)
                {
                    page = 12;
                }
                else
                {
                    //noise
                    SoundPlayer player = new SoundPlayer();

                    player = new SoundPlayer(Properties.Resources.eatingMushrooms);

                    player.Play();


                    //remove labels and then reshow slide until random encounter is met
                    option1Label.Text = "";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    Refresh();
                    Thread.Sleep(300);
                    page = 8;
                }
            }
            else if (page == 3)
            {
                //random generator
                havoc = randGen.Next(1, 11);

                if (havoc == 5)
                {
                    page = 4;
                }
                else
                {
                    //set options to "" to let the player know they're clicking the option

                    option1Label.Text = "";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    outputLabel.Text = "";

                    Refresh();
                    Thread.Sleep(500);
                    page = 3;
                }
            }
            else if (page == 7)
            {
                // send them to fossil void for encounter random generator
                fossilVoid();
            }

            // all below are set to go back to page 1

            else if (page == 4)
            {
                page = 1;
            }
            else if (page == 10)
            {
                page = 1;
            }
            else if (page == 11)
            {
                page = 1;
            }
            else if (page == 12)
            {
                page = 1;
            }
            else if (page == 14)
            {
                page = 1;
            }
            else if (page == 19)
            {
                page = 1;
            }
            else if (page == 21)
            {
                page = 1;
            }
            else if (page == 22)
            {
                page = 1;
            }
            else if (page == 23)
            {
                page = 1;
            }
            else if (page == 24)
            {
                page = 1;
            }
            else if (page == 25)
            {
                page = 1;
            }
            else if (page == 26)
            {
                page = 1;
            }
            else if (page == 27)
            {
                page = 1;
            }


            // Rerouting all if statements towards one large switch statement
            pageVoid();
        }
        private void option2Label_Click(object sender, EventArgs e)
        {
            // large if, else if statement to hold where the player can go

            if (page == 1)
            {
                //random chance to be disposed

                //random generator
                havoc = randGen.Next(1, 100);

                if (havoc > 35)
                {
                    page = 4;
                }
                else
                {
                    page = 3;
                }

            }
            else if (page == 2)
            {
                // grant Lighter Gear and remove others
                lightGear = 1;
                moreAmmo = 0;
                leechRepellant = 0;
                page = 1;

                //declare sound player

                SoundPlayer player = new SoundPlayer();

                player = new SoundPlayer(Properties.Resources.lightGearNoise);

                player.Play();

                Refresh();
                Thread.Sleep(200);
            }
            else if (page == 16)
            {
                //grant the option to avoid going back to collecting fossils

                page = 5;
            }
            else if (page == 5)
            {
                //route down to go mine gold

                page = 9;
            }
            else if (page == 9)
            {
                // Decide to not mine the gold and find the dreadnought Cocoon

                page = 15;
            }
            else if (page == 17)
            {
                //try to climb out of the hole you ended up in

                page = 23;
            }
            else if (page == 15)
            {
                // leave the dreadnought Cocoon alone and go find the Ommarune

                page = 20;
            }
            else if (page == 20)
            {
                // you ended up running past the ommarune and passing out

                page = 25;
            }
            else if (page == 6)
            {
                //you have encountered enemies and now have a chance to run away

                //random variable

                runAway = randGen.Next(1, 100);

                // if statement to give a higher chance to flee if lighter gear is equipped

                if (lightGear == 0)
                {
                    if (runAway < 20)
                    {
                        outputLabel.Text = $"you were able to run away! Seems like the Glyphids lost interest." +
                            $"\n\nSearch for more Fossils? ({fossil}/10)";
                        page = 16;
                    }
                    else
                    {
                        page = 11;
                    }
                }
                else
                {
                    if (runAway < 60)
                    {
                        page = 16;
                        outputLabel.Text = $"you were able to run away! maybe thanks to your light gear!" +
                            $" it also seems like the Glyphids lost interest.\n\nSearch for more Fossils? ({fossil}/10)";
                    }
                    else
                    {
                        page = 11;
                    }
                }
            }
            else if (page == 8)
            {
                page = 13;
            }
            else if (page == 13)
            {
                // you try to recover after eating mushrooms

                page = 14;
            }
            else if (page == 18)
            {
                // lighter gear lets you get away
                if (lightGear == 0)
                {
                    outputLabel.Text = "Trying to Get Away you tripped and fell, Allowing the dreadnought to have a feast!" +
                           "\n\nPlay Again?";

                    page = 19;
                }
                else
                {
                    outputLabel.Text = "you were able to run Away! but the ground cracked underneath you!";

                    option1Label.Text = "";
                    option2Label.Text = "";
                    option3Label.Text = "";

                    Refresh();
                    Thread.Sleep(1000);

                    page = 17;
                }
            }
            else if (page == 3)
            {
                // you decided not to cause havoc anymore

                page = 1;
            }

            // all below are set to go back to close the application, ie case 99

            else if (page == 4)
            {
                page = 99;
            }
            else if (page == 10)
            {
                page = 99;
            }
            else if (page == 11)
            {
                page = 99;
            }
            else if (page == 12)
            {
                page = 99;
            }
            else if (page == 14)
            {
                page = 99;
            }
            else if (page == 19)
            {
                page = 99;
            }
            else if (page == 25)
            {
                page = 99;
            }
            else if (page == 21)
            {
                page = 99;
            }
            else if (page == 22)
            {
                page = 99;
            }
            else if (page == 23)
            {
                page = 99;
            }
            else if (page == 24)
            {
                page = 99;
            }
            else if (page == 26)
            {
                page = 99;
            }
            else if (page == 27)
            {
                page = 99;
            }


            // Rerouting all if statements towards one large switch statement
            pageVoid();
        }
        private void option3Label_Click(object sender, EventArgs e)
        {
            // large if, else if statement to hold where the player can go
            if (page == 1)
            {
                // you wish to explore the shop

                page = 2;
            }
            else if (page == 2)
            {
                // grant More Ammunition from shop etc

                moreAmmo = 1;
                leechRepellant = 0;
                lightGear = 0;
                page = 1;

                //declare sound player

                SoundPlayer player = new SoundPlayer();

                player = new SoundPlayer(Properties.Resources.moreAmmoNoise);

                player.Play();

                Refresh();
                Thread.Sleep(200);
            }
            else if (page == 5)
            {
                // you wish to eat the suspicious mushrooms

                page = 8;
            }
            else if (page == 13)
            {
                // you decided to go back to eating the mushrooms

                page = 8;
            }

            // Rerouting all if statements towards one large switch statement
            pageVoid();
        }

        //Void to hold the Fossil section of the code and correctly reroute all fossil actions etc...
        private void fossilVoid()

        {
            //embeded image change 

            mainDisplay.BackgroundImage = (Properties.Resources.fossil_grab);

            //state sound player

            SoundPlayer player = new SoundPlayer();

            player = new SoundPlayer(Properties.Resources.rocks);

            player.Play();

            //add two fossils to the player count and reset label text to show effect
            fossil++;
            fossil++;
            outputLabel.Text = $"You Found some Fossils! You Currently have\n( {fossil}/10 )";

            option1Label.Text = "";
            option2Label.Text = "";
            option3Label.Text = "";

            Refresh();
            Thread.Sleep(800);


            //check if player has 10 fossils to grant them a victory
            if (fossil == 10)
            {
                page = 10;
            }
            else
            {
                // if they dont have enough fossils, give a chance of getting a encounter

                //declare random generator and if statement to sort answer
                encounter = randGen.Next(1, 100);

                if (encounter < 33)
                {
                    page = 6;
                }
                else
                {
                    page = 7;
                }
            }

        }
        //void to hold switch statements for standby
        private void pageVoid()
        {
            //refresh, sleep and option labels set to "" to add the effect of the prompts switching

            if (page == 16)
            {
                option1Box.Visible = false;
                option2Box.Visible = false;
                option3Box.Visible = false;
                option1Label.Text = "";
                option2Label.Text = "";
                option3Label.Text = "";

                Refresh();
                Thread.Sleep(300);
            }
            else if (page == 8 || page == 3 || page == 19)
            {
                // if page = 8 or page = 3, or page = 18 do not remove outputlabel. (they have their own delays for their sounds)
                option1Box.Visible = false;
                option2Box.Visible = false;
                option3Box.Visible = false;
                option1Label.Text = "";
                option2Label.Text = "";
                option3Label.Text = "";
            }
            else
            {
                // else reset everything and have a delay

                option1Box.Visible = false;
                option2Box.Visible = false;
                option3Box.Visible = false;
                outputLabel.Text = "";
                option1Label.Text = "";
                option2Label.Text = "";
                option3Label.Text = "";

                Refresh();
                Thread.Sleep(300);
            }

            switch (page)
            {

                case 1:
                    //need to restate the start label to reset the prompt when paths are redirected to case 1
                    fossil = 0;

                    outputLabel.Text = $"You're on your Space Rig. It is almost time for mission launch, How do you Prepare?";

                    option1Box.Visible = true;
                    option2Box.Visible = true;
                    option3Box.Visible = true;

                    option1Label.Text = "Pursue your Mission";
                    option2Label.Text = "Cause Havoc";
                    option3Label.Text = "Check Shop";

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_1);

                    //state sound player

                    SoundPlayer player = new SoundPlayer();

                    player = new SoundPlayer(Properties.Resources.buttonSound);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);

                    break;

                case 2:
                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_2);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.shop);

                    player.Play();

                    Refresh();
                    Thread.Sleep(50);
                    
                    //enable orb options to be visible

                    option1Box.Visible = true;
                    option2Box.Visible = true;
                    option3Box.Visible = true;

                    outputLabel.Text = "Welcome to the Abyss Shop, There are some special items for sale, but due to the lack" +
                        "of products you can only choose one.\n\nWhich one do you want?";

                    // if statement to reasure the player that they have recieved their item in the shop

                    if (leechRepellant > 0)
                    {
                        option1Label.Text = "Leech Repellant is Currently Selected";
                        option2Label.Text = "Lighter Gear";
                        option3Label.Text = "More Ammunition";
                    }
                    else if (lightGear > 0)
                    {
                        option1Label.Text = "Leech Repellant";
                        option2Label.Text = "Lighter Gear is Currently Selected";
                        option3Label.Text = "More Ammunition";
                    }
                    else if (moreAmmo > 0)
                    {
                        option1Label.Text = "Leech Repellant";
                        option2Label.Text = "Lighter Gear";
                        option3Label.Text = "More Ammunition is Currently Selected";
                    }
                    else
                    {
                        option1Label.Text = "Leech Repellant";
                        option2Label.Text = "Lighter Gear";
                        option3Label.Text = "More Ammunition";
                    }

                    break;
                case 3:
                    outputLabel.Text = "Continue Causing Havoc?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";


                    //enable orb options to be visible except option3

                    option1Box.Visible = true;
                    option2Box.Visible = true;
                    

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_3);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.havocNoise);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 4:
                    // random gen

                    outputLabel.Text = "Mission control has decided to Dispose of you, How sad.\nPlay Again?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";

                    //enable orb options to be visible except option3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_4);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.shop);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);

                    break;
                case 5:
                    outputLabel.Text = "you Have Successfully Landed on the Planet. It is Extremely Important that you complete your " +
                        "missions.\n\nWhat Should You Do?";

                    option1Label.Text = $"Collect Fossils ({fossil}/10)";
                    option2Label.Text = "Explore the Nearby Tunnel";
                    option3Label.Text = "Eat the Suspicious Mushrooms";

                    //enable orb options to be visible 

                    option1Box.Visible = true;
                    option2Box.Visible = true;
                    option3Box.Visible = true;  

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_5);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.Landing);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);

                    break;
                case 6:
                    outputLabel.Text = "Oh No! A swarm of Glyphids has heard all noise!\n\nWhat Should You Do?";

                    option1Label.Text = "Fight the Bugs";
                    option2Label.Text = "Run Away";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;
                   

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_6);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.glyphidRoar);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);

                    break;
                case 7:

                    outputLabel.Text = "Its scarcely Quiet, But you have a mission to do.\n\nWhat Now?";

                    option1Label.Text = $"Collect More fossils ({fossil}/10)";
                    option2Label.Text = "Maybe Later";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_7);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.buttonSound);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);

                    break;
                case 8:
                    outputLabel.Text = "Eat More?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_8);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.eatingMushrooms);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 9:
                    outputLabel.Text = "While Persuing the tunnel you ended up finding some gold!\n\nMine it?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_9);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.glisteningGold);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 10:
                    outputLabel.Text = "You Collected enough Fossils For Management, They Called in a pickup for you." +
                        " Good Job Miner!\n\nPlay Again?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_10);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.Landing);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 11:
                    outputLabel.Text = "You Tripped While Running Away, and now, you are Glyphid Food\n\nPlay Again?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.lose_4);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.glyphidRoar);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 12:
                    outputLabel.Text = "You... Ended up in Hogwarts? What? Whatever you win.\n\nPlay Again?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.hogWarts);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.hogWartsNoise);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 13:
                    outputLabel.Text = "you lay there on the ground, wondering why, you would eat those mushrooms. What did you gain?" +
                        "\n\nUpset at yourself for indulging in those Mushrooms, What Should you Do Now?";

                    option1Label.Text = "Eat More?";
                    option2Label.Text = "try to recover";
                    option3Label.Text = "Eat More?";

                    //enable orb options to be visible

                    option1Box.Visible = true;
                    option2Box.Visible = true;
                    option3Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_13);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.cough);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 14:
                    outputLabel.Text = "you realize that the mushrooms are causing you pain so you stop, " +
                        "only to see that your legs are gone\n\n Play Again?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_14);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.fallingOver);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 15:
                    outputLabel.Text = "Deciding to leave the gold behind and tread on, you end up running into a Large Dreadnought Egg" +
                        "\n\nWhat now?";

                    option1Label.Text = "Destroy the Egg";
                    option2Label.Text = "Leave it alone";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_15);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.dreadnoughtCocoon);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 16:


                    option1Label.Text = $"Find more Fossils ({fossil}/10)";
                    option2Label.Text = "Maybe Later";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;


                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_7);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.glyphidDeath);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 17:
                    outputLabel.Text = "The Ground Cracked Underneath you! After a long fall, it seems you landed on the inner " +
                        "layer of the planet, but at least you have your gold...\n\nWhat Now?";

                    option1Label.Text = "Move Forward";
                    option2Label.Text = "Try To Climb out";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_17);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.ground_cracks);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 18:
                    outputLabel.Text = "The Egg Hatched and the Powerful Dreadnought overwhelms you, you try to fight back but end up " +
                        "running out ammo.\n\nWhat Should you Do?";

                    option1Label.Text = "Give Up";
                    option2Label.Text = "Run Away!";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_18);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.dreadnoughtRoar);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 19:
                    // this one has two connected endings, resulting in the same thing. change the text with an if statement
                    //earlier in the optionlabels


                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_19);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.dreadnoughtRoar);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 20:
                    outputLabel.Text = "you leave the egg alone, and carry on only to run into a Ommarune! Riches!\n\nWhat Now?";

                    option1Label.Text = "Call Heavy duty Equipment?";
                    option2Label.Text = "Ignore and go Explore";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_20);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.buttonSound);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 21:
                    outputLabel.Text = "After Calling Heavy Equipment Down you have extracted the Ommarune! Managment will be Proud.\n\nPlay Again?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_21);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.buttonSound);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 22:
                    outputLabel.Text = "The Cave leech almost Had You! but you had LeechRepellant! Close One! After progressing through " +
                        "The Cave you ended up finding a Buried Database! Congrats! Management will be proud!\n\nPlay Again?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_22);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.buttonSound);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);

                    break;
                case 23:
                    outputLabel.Text = "While Trying to Climb back up you were swarmed by enemies from, you ended up falling " +
                        "quite the distance and your body is unresponsive.\n\nPlay Again?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_23);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.buttonSound);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 24:
                    outputLabel.Text = "while walking forward you are caught by a cave leech! Wish you had repellant?" +
                        "\n\nPlay Again?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_24);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.glyphidRoar);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 25:
                    outputLabel.Text = "you are tired from all the running and collapse," +
                        " not long after you awake with a missing leg!\n\nPlay Again?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.lose_2);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.fallingOver);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 26:
                    outputLabel.Text = "the powerful dreadnought almost overwhelms you, but you were victorious! " +
                        "Management will be Pleased.\n\nPlay Again?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.page_26);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.dreadnoughtDeath);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 27:
                    outputLabel.Text = "You ended up running out of Ammo, without anymore bullets, you fought hard but ended up being eaten." +
                        "\n\nPlay Again?";

                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";

                    //enable orb options to be visible except 3

                    option1Box.Visible = true;
                    option2Box.Visible = true;

                    //embeded image change 

                    mainDisplay.BackgroundImage = (Properties.Resources.lose_4__2_);

                    //state sound player

                    player = new SoundPlayer(Properties.Resources.glyphidRoar);

                    player.Play();

                    Refresh();
                    Thread.Sleep(300);
                    break;
                case 99:
                    //screen goes black and displays "Thanks for Playing" while making sure the application closes afterwards

                    mainDisplay.Visible = false;
                    outputLabel.Text = "";
                    startupText.Text = "Thanks for Playing!";

                    startupText.Visible = true;
                    startScreenBox.Visible = true;

                    option1Label.Enabled = false;
                    option2Label.Enabled = false;
                    option3Label.Enabled = false;

                    //enable orb options to be invisible

                    option1Box.Visible = false;
                    option2Box.Visible = false;
                    option3Box.Visible = false;  
                    //state sound player

                    player = new SoundPlayer(Properties.Resources.buttonSound);

                    player.Play();

                    Refresh();
                    Thread.Sleep(1500);

                    //close application

                    Application.Exit();
                    break;
            }
        }
    }
}
