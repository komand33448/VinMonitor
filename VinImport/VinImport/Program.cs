using System.Threading;
using System;

namespace VinImport
{
    internal class Program
    {
        static DVIService.monitorSoapClient client = new DVIService.monitorSoapClient();
        static void Main()
        {
            //Denne variable bliver brugt til at måle tiden imellem updates
            int updateCounter = 0;

            //En string der indholder alt data der skal blive printet ud
            string[] data = {
                "                                                          ", "|", "",
                "                  Temperatur og fugtighed                 ", "|","                         Lagerstatus",
                "                                                          ", "|","",
                "  Lager:                                                  ", "|","  Varer under minimum:",
                "  Temp: " + GetLagerTemp(),                                  "|","------------------------------------------------------------",
                "  Fugt: " + GetLagerFugt(),                                  "|"," " + GetMinStock(0),
                "                                                          ", "|"," " + GetMinStock(1),
                "  Udenfor:                                                ", "|"," " + GetMinStock(2),
                "  Temp: " + GetOutdoorTemp(),                                "|"," " + GetMinStock(3),
                "  Fugt: " + GetOutdoorFugt(),                                "|"," " + GetMinStock(4),
                "                                                          ", "|"," " + GetMinStock(5),
                "                                                          ", "|"," " + GetMinStock(6),
                "----------------------------------------------------------", "|","  Varer over maksimun:",
                "                                                          ", "|","------------------------------------------------------------",
                "                        Tid / Dato                        ", "|"," " + GetMaxStock(0),
                "                                                          ", "|"," " + GetMaxStock(1),
                "  København:                                              ", "|"," " + GetMaxStock(2),
                "  " + GetTimeZone(0),                                        "|"," " + GetMaxStock(3),
                "                                                          ", "|"," " + GetMaxStock(4),
                "  London:                                                 ", "|"," " + GetMaxStock(5),
                "  " + GetTimeZone(1),                                        "|","  Mest solgt i dag:",
                "                                                          ", "|","------------------------------------------------------------",
                "  Singapore:                                              ", "|"," " + GetMostSold(0),
                "  " + GetTimeZone(2),                                        "|"," " + GetMostSold(1),
                "                                                          ", "|"," " + GetMostSold(2),
                "                                                          ", "|"," " + GetMostSold(3),
                "                                                          ", "|","",
                "                                                          ", "|",""
            };

            //counter bliver brugt til at tælle hvor i "data" arrayet vi er nået til
            int counter = 0;
            while(true)
            {
                //2 for loops så man kan print både på x og y koordinater
                for (int i = 0; i < 27; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        //switch statement der tjekker hvor vi er henne i "data" arrayet og tilføjer farver
                        switch (counter)
                        {
                            case 17:case 20:case 23:case 26:case 29:case 32:case 35:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                break;
                            case 44:case 47:case 50:case 53:case 56:case 59:
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                            case 3:case 5:case 9:case 11:case 14:case 21:case 38:case 41:case 42:case 48:case 57:case 66:case 62:case 65:
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                        }

                        Console.Write(data[counter]);
                        counter++;
                    }
                    Console.WriteLine();
                }
                //Sleep i et sek så update uret, set "counter" til 0, plus 1 til "updateCounter" og clear terminallen
                Thread.Sleep(1000);
                counter = 0;
                Console.Clear();
                data[51] = "  " + GetTimeZone(0);
                data[60] = "  " + GetTimeZone(1);
                data[69] = "  " + GetTimeZone(2);
                updateCounter++;

                //hvis "updateCounter" er 300 (5 Min) så update alt input og set "updateCounter" til 0
                if(updateCounter == 300)
                {
                    data = new string[] {"                                                          ","|","","                  Temperatur og fugtighed                 ", "|","                         Lagerstatus                         ","                                                          ", "|","","  Lager:                                                  ", "|","  Varer under minimum:","  Temp: " + GetLagerTemp(),"|","------------------------------------------------------------","  Fugt: " + GetLagerFugt(),"|"," " + GetMinStock(0),"                                                          ", "|"," " + GetMinStock(1),"  Udenfor:                                                ", "|"," " + GetMinStock(2),"  Temp: " + GetOutdoorTemp(),                                "|"," " + GetMinStock(3),"  Fugt: " + GetOutdoorFugt(),                                "|"," " + GetMinStock(4),"                                                          ", "|"," " + GetMinStock(5),"                                                          ", "|"," " + GetMinStock(6),"----------------------------------------------------------", "|","  Varer over maksimun:","                                                          ", "|","------------------------------------------------------------","                        Tid / Dato                        ", "|"," " + GetMaxStock(0),"                                                          ", "|"," " + GetMaxStock(1),"  København:                                              ", "|"," " + GetMaxStock(2),"  " + GetTimeZone(0),                                        "|"," " + GetMaxStock(3),"                                                          ", "|"," " + GetMaxStock(4),"  London:                                                 ", "|"," " + GetMaxStock(5),"  " + GetTimeZone(1),                                        "|","  Mest solgt i dag:","                                                          ", "|","------------------------------------------------------------","  Singapore:                                              ", "|"," " + GetMostSold(0),"  " + GetTimeZone(2),                                        "|"," " + GetMostSold(1),"                                                          ", "|"," " + GetMostSold(2),"                                                          ", "|"," " + GetMostSold(3),"                                                          ", "|","","                                                          ", "|",""};
                    updateCounter = 0;
                }
            }
        }

        static string GetLagerTemp()
        {
            //get data
            string tempHolder = "";
            try
            {
                tempHolder = Convert.ToString(client.StockTemp()) + "ºC";
            }
            catch // Hvis server fejl skriv n/a
            {
                tempHolder = "n/a";
            }

            //Add et mellemrum til string'en ind til den bliver 50 lang
            while (tempHolder.Length != 50)
            {
                tempHolder = tempHolder + " ";
            }

            return tempHolder;
        }

        static string GetLagerFugt()
        {
            //get data
            string fugtHolder = "";
            try
            {
                fugtHolder = Convert.ToString(client.StockHumidity()) + "%";
            }
            catch // Hvis server fejl skriv n/a
            {
                fugtHolder = "n/a";
            }

            //Add et mellemrum til string'en ind til den bliver 50 lang
            while (fugtHolder.Length != 50)
            {
                fugtHolder = fugtHolder + " ";
            }

            return fugtHolder;
        }

        static string GetOutdoorTemp()
        {
            //get data
            string tempHolder = "";
            try
            {
                tempHolder = Convert.ToString(client.OutdoorTemp()) + "ºC";
            }
            catch
            {
                tempHolder = "n/a";
            }

            //Add et mellemrum til string'en ind til den bliver 50 lang
            while (tempHolder.Length != 50)
            {
                tempHolder = tempHolder + " ";
            }

            return tempHolder;
        }

        static string GetOutdoorFugt()
        {
            //get data
            string fugtHolder = "n/a";
            try
            {
                fugtHolder = Convert.ToString(client.OutdoorHumidity()) + "%";
            }
            catch //Hvis server fejl skriv n/a
            {
                fugtHolder = "n/a";
            }

            //Add et mellemrum til string'en ind til den bliver 50 lang
            while (fugtHolder.Length != 50)
            {
                fugtHolder = fugtHolder + " ";
            }

            return fugtHolder;
        }

        //zoneinfo (0 = Copenhagen, 1 = London, 2 = Singapore)
        static string GetTimeZone(int zoneInfo) //Denne funtion har et argument der signalere hvilken tidszone den skla gice videre
        {
            string usedzone = "";
            //vælger hvilken tidszone den skal return
            switch (zoneInfo)
            {
                case 0:
                    usedzone = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Romance Standard Time").ToString();
                    break;
                case 1:
                    usedzone = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "GMT Standard Time").ToString(); ;
                    break;
                case 2:
                    usedzone = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Singapore Standard Time").ToString();
                    break;
            }

            //Adder mellemrum ind til den er 56 lang
            while(usedzone.Length != 56)
            {
                usedzone = usedzone + " ";
            }

            return usedzone;
        }

        static string GetMinStock(int row) //argument der signalere hvilken varer den skal give videre
        {
            string item = "";

            // Try/catch ser om der er data i varer arrayet, hvis der ikke er så skriver den intet, hvis der er så skriver den data'et 
            try
            {
                item = client.StockItemsUnderMin()[row];
            }
            catch
            {
                item = "";
            }
            
            return item;
        }

        static string GetMaxStock(int row) 
        {
            string item = "";

            try
            {
                item = client.StockItemsOverMax()[row];
            }
            catch
            {
                item = "";
            }

            return item;
        }

        static string GetMostSold(int row)
        {
            string item = "";

            try
            {
                item = client.StockItemsMostSold()[row];
            }
            catch
            {
                item = "";
            }

            return item;
        }
    }
}

//http://dvimonitor.pilotdrift.dk/monitor.asmx