﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using MediaPlayer;
using System.Speech.Synthesis;
using System.Diagnostics;

namespace BatchPlus
{
    class Program
    {
        static int counter = 0;
        static string line;
        static List<Int32> integers = new List<Int32>();
        static List<string> integerName = new List<string>();
        static List<string> strings = new List<string>();
        static List<string> stringName = new List<string>();
        static string input;
        static int compint;
        static string output;
        static string fin;
        static SpeechSynthesizer speech = new SpeechSynthesizer();
        static OpenFileDialog ofd = new OpenFileDialog();

        static System.Media.SoundPlayer player = new System.Media.SoundPlayer(); //makes new sound player

        static void Main(string[] args)
        {
            Start();
        }

        static void Start()
        {
            Console.ForegroundColor = ConsoleColor.White;
            counter = 0;
            line = "";
            integers.Clear();
            integerName.Clear();
            input = "";
            compint = 0;
            Exec();
        }

        static void Exec()
        {
            Console.Title = "OctoScript (Beta v0.6.1)";
            Console.WriteLine("Welcome to");
            Console.WriteLine("The OctoScript Project");
            Console.WriteLine("Created by John Spahr and Porter Parent");

            int close = 0;

            var t = new Thread((ThreadStart)(() =>
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    input = ofd.FileName;
                }
                else
                {
                    close = 1;
                }
            }));

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            Console.Clear();

            if (close == 1)
            {
                Environment.Exit(0);
            }

            ReadLines();
        }

        static void ReadLines()
        {
            StreamReader sr = new StreamReader(input);

            while ((line = sr.ReadLine()) != null) //reads each line
            {
                if (line.Contains("Image"))
                {
                    string text = line.Replace("Image", " ");
                    text = text.Trim();
                    if (text.Contains(":") == false)
                    {
                        text = Path.GetDirectoryName(ofd.FileName) + text;
                    }
                    var picture = new PictureBox()
                    {
                        Name = "img",
                        Dock = DockStyle.Fill,
                        Image = System.Drawing.Image.FromFile(text),
                    };

                    Form f1 = new Form();

                    f1.Controls.Add(picture);
                    f1.SuspendLayout();
                    f1.Size = picture.Size;
                    f1.Text = text;
                    f1.ResumeLayout(false);
                    f1.ShowIcon = false;
                    f1.ShowInTaskbar = false;
                    f1.MinimizeBox = false;
                    f1.FormBorderStyle = FormBorderStyle.Sizable;
                    
                    Application.EnableVisualStyles();
                    Application.Run(f1);
                    
                    //feel free to improve this feature
                }

                if (line.Contains("Beep"))
                {
                    Console.Beep(); //beep
                }

                if (line.Contains("Rickroll"))
                {
                    Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ"); //Rickroll
                }

                if (line.Contains("Title"))
                {
                    string text = line.Replace("Title", " ");
                    text = text.Trim();
                    Console.Title = text;
                }

                if (line.Contains("Speak"))
                {
                    string text = line.Replace("Speak", " ");
                    text = text.Trim();
                    speech.Speak(text); //reads input
                }

                if (line.Contains("Message"))
                {
                    string text = line.Replace("Message", " ");
                    text = text.Trim();
                    MessageBox.Show(text, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information); //shows message
                    //feel free to improve this feature
                }

                if (line.Contains("Start"))
                {
                    string text = line.Replace("Start", " ");
                    text = text.Trim();
                    if (text.Contains(":") == false)
                    {
                        text = AppDomain.CurrentDomain.BaseDirectory.ToString() + text;
                    }
                    Process.Start(text); //starts process
                }

                if (line.Contains("Time"))
                {
                    //gets time
                    Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt"));
                }

                if (line.Contains("Date"))
                {
                    //gets time
                    Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy"));
                }

                if (line.Contains("Stopsound"))
                {
                    //stops player
                    player.Stop();
                }

                if (line.Contains("Setstring:"))
                {
                    string text = line.Replace("Setstring:", " "); //Replaces Setstring: with blank space
                    text = text.Trim();
                    strings.Add(text); //adds string
                    string name = sr.ReadLine(); //gets name
                    stringName.Add(name); //adds name to list
                    counter++;
                }

                if (line.Contains("Getstring"))
                {
                    string text = line.Replace("Getstring", " "); //Replaces Delay with blank space
                    text = text.Trim();
                    if (stringName.Contains(text))
                    {
                        //displays string
                        int select = stringName.IndexOf(text);
                        Console.WriteLine(strings[select]);
                    }
                }

                if (line.Contains("Playsound"))
                {
                    player.Stop(); //stops current sound

                    string text = line.Replace("Playsound", " "); //Replaces Playsound with blank space
                    text = text.Trim();
                    if (text.Contains(":") == false)
                    {
                        text = Path.GetDirectoryName(ofd.FileName) + text;
                    }
                    player.SoundLocation = text; //sets file path
                    player.Play(); //plays
                }

                if (line.Contains("Softreset") == true)
                {
                    //softreset
                    ReadLines();
                }

                if (line.Contains("Hardreset") == true)
                {
                    //hardreset
                    Start();
                }

                if (line.Contains("Clear") == true)
                {
                    //clears console
                    Console.Clear();
                }

                if (line.Contains("Echo.") == true)
                {
                    line.Replace("Echo.", " ");
                    //blank line
                    Console.WriteLine();
                }

                if (line.Contains("Wait") == true)
                {
                    //wait
                    Console.ReadKey(true);
                }

                if (line.Contains("Backcolor") == true) //background color
                {
                    if (line.Contains("Blue") == true) //blue
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }

                    else if (line.Contains("Red") == true) //red
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }

                    else if (line.Contains("Green") == true) //green
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }

                    else if (line.Contains("Purple") == true) //purple
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    }

                    else if (line.Contains("Yellow") == true) //yellow
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }

                    else if (line.Contains("Pink") == true) //pink
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                    }
                    else if (line.Contains("Cyan") == true) //cyan
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                    }
                    else if (line.Contains("Black") == true) //black
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (line.Contains("Gray") == true) //gray
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    else if (line.Contains("Darkgray") == true) //dark gray
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                }

                if (line.Contains("Forecolor") == true) //fore color
                {
                    if (line.Contains("Blue") == true) //blue
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    else if (line.Contains("Red") == true) //red
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    else if (line.Contains("Green") == true) //green
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    else if (line.Contains("Yellow") == true) //yellow
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    else if (line.Contains("Purple") == true) //purple
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    }

                    else if (line.Contains("Pink") == true) //pink
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else if (line.Contains("Cyan") == true) //cyan
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (line.Contains("Black") == true) //black
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else if (line.Contains("Gray") == true) //gray
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else if (line.Contains("Darkgray") == true) //dark gray
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                }
               
                
                
                if (line.Contains("Echo") == true)
                {
                    if (line.Contains(".") == false)
                    {
                        string text = line.Replace("Echo", " "); //Replaces Echo with blank space
                        text = text.Trim();
                        Console.WriteLine(text); //Writes text
                    }
                }

                if (line.Contains("Delay") == true)
                {
                    string text = line.Replace("Delay", " "); //Replaces Delay with blank space
                    text = text.Trim();
                    int delayTime = Convert.ToInt32(text);
                    Thread.Sleep(delayTime);
                }

                if (line.Contains("Ifint:") == true)
                {
                    string text = line.Replace("Ifint:", " "); //Replaces Ifint: with blank space
                    text = text.Trim();
                    if (integerName.Contains(text))
                    {
                        //displays integer
                        int select = integerName.IndexOf(text);
                        compint = integers[select];
                    }
                    int num = Convert.ToInt32(sr.ReadLine());
                    counter++;
                    if (compint == num)
                    {
                        while (sr.ReadLine() != "Endif")
                        {
                            counter++;
                        }
                    }
                }

                if (line.Contains("Setint:") == true)
                {
                    string text = line.Replace("Setint:", " "); //Replaces Setint: with blank space
                    text = text.Trim();
                    int num = Convert.ToInt32(text); //converts number to int32
                    integers.Add(num); //add number to list of integers
                    string name = sr.ReadLine(); //gets name
                    integerName.Add(name); //adds name to list
                    counter++;
                }

                if (line.Contains("Getint") == true)
                {
                    string text = line.Replace("Getint", " "); //Replaces Delay with blank space
                    text = text.Trim();
                    if (integerName.Contains(text))
                    {
                        //displays integer
                        int select = integerName.IndexOf(text);
                        Console.WriteLine(integers[select]);
                    }
                }

                if (line.Contains("Ifinput") == true)
                {
                    string text = line.Replace("Ifinput", " "); //Replaces Delay with blank space
                    text = text.Trim();
                    if (Console.ReadLine() != text)
                    {
                        while (sr.ReadLine() != "Endif")
                        {
                            counter++;
                        }
                    }
                    
                }

                if (line.Contains("Intrandom:"))
                {
                    string text = line.Replace("Intrandom:", " "); //Replaces Intrandom: with blank space
                    text = text.Trim();
                    int firstNum = Convert.ToInt32(text); //converts number to int32
                    int secondNum = Convert.ToInt32(sr.ReadLine());
                    Random random = new Random();
                    int num = random.Next(firstNum, secondNum + 1);
                    integers.Add(num); //add number to list of integers
                    string name = sr.ReadLine(); //gets name
                    integerName.Add(name); //adds name to list
                    counter = counter + 2;
                }

                if (line.Contains("Domath:")) 
                {
                    string text = line.Replace("Domath:", " "); //Replaces Delay with blank space
                    text = text.Trim();
                    int max = Convert.ToInt32(sr.ReadLine());
                    int min = Convert.ToInt32(sr.ReadLine());
                    if (text == "/")
                    {
                        int result = max / min;
                        Console.WriteLine(result.ToString());
                    }
                    if (text == "*")
                    {
                        int result = max * min;
                        Console.WriteLine(result.ToString());
                    }
                    if (text == "+")
                    {
                        int result = max + min;
                        Console.WriteLine(result.ToString());
                    }
                    if (text == "-")
                    {
                        int result = max - min;
                        Console.WriteLine(result.ToString());
                    }
                }

                if (line.Contains("Random"))
                {
                    //gets values
                    var min = sr.ReadLine();
                    var max = sr.ReadLine();
                    //converts to int32
                    int minimum = Convert.ToInt32(min);
                    int maximum = Convert.ToInt32(max);
                    //creates new random
                    Random random = new Random();
                    //gets random number
                    int RandNum = random.Next(minimum, maximum + 1);
                    Console.WriteLine(RandNum);
                    counter++;
                    counter++;
                }

                counter++; //Counter is the line number thingy mabobber
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to finish program.");
            Console.ReadKey(true);
            player.Stop();
            sr.Close();
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Exec();
        }
    }
}
