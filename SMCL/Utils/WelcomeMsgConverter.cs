using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace SMCL.Utils
{
    public class WelcomeMsgConverter : IValueConverter
    {
        private List<string> Greetings = new List<string>()
        {
            "#time#",
            "Hello , # !",
            "Hi , # !",
            "Hey , # !",
            "Yo ! # ",
            "Heyyy , # !",
            "Alright, # ?",
            "Hiya ! # ",
            "Ahoy ! # ",
            "Hello stranger !",
            "Howdy ! # ",
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var username = value as string;
                username = username.Replace("_", "__");

                var greeting = Greetings[new Random().Next(Greetings.Count)];
                if (greeting == "#time#")
                {
                    if (DateTime.Now.Hour < 5 || DateTime.Now.Hour > 20)
                    {
                        greeting = "Night , # !";
                    }
                    else if (DateTime.Now.Hour >= 5 && DateTime.Now.Hour <= 11)
                    {
                        greeting = "Morning , # !";
                    }
                    else if (DateTime.Now.Hour > 11 && DateTime.Now.Hour <= 18)
                    {
                        greeting = "Afternoon , # !";
                    }
                    else
                    {
                        greeting = "Evening , # !";
                    }
                }

                return greeting.Replace("#", username);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}