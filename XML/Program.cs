using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;
using XML_Processing;
using System.Net;
using System.Timers;
using RestSharp;
using RestSharp.Authenticators;

using System.IO;
using System.Security.Cryptography;
using System.Net.Http;

namespace XML
{
    class Program
    {

        private static Timer timer;
        public static void Main(string[] args)
        {
            
            
            timer = new System.Timers.Timer();
            Console.WriteLine("Please input how often that you want the app to run in milliseconds and then Enter:");
            int temp = Int32.Parse(Console.ReadLine());
            timer.Interval = temp;

            timer.Elapsed += OnTimedEvent;

            timer.AutoReset = true;


            timer.Enabled = true;

            Console.WriteLine("Press the Enter key to exit the program at any time... ");
            Console.ReadLine();


            /*
                       // getAPXML();
                        getCFPXML();
                        getSECXML();
                       // APXMLRanking();
                        CFPXMLRanking();
                        Console.WriteLine("DOWNLOADING...");
                        Console.ReadLine();
                       */
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            //getCFPXML();
            
            /*
            // if CFP is not available, use AP ranking as the Top25
                // use week # to determine whether AP or CFP should be used
            
            string weekNum = ;

            if(weekNum < "7"){
                getAPXML();
                APXMLRanking();
            
            }

           */
            getAPXML();
            APXMLRanking();

            //getCFPXML();
            //CFPXMLRanking();

            getSECXML();
            
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }
       


        //SHA256 Gnenerator
        public static string CreateSHA256Hash(string input)
        {
            // Use input string to calculate SHA-256 hash
            SHA256 mySHA256 = SHA256Managed.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = mySHA256.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();


        }

        public static void getAPXML() {
            // getStuff

            //get the timestamp value
            string timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds.ToString();

            //grab just the integer portion
            timestamp = timestamp.Substring(0, timestamp.IndexOf("."));
            
            //set the API key (note that this is to provide a valid key!
            string apikey = "d2gujudevqx32p8rd7atea8s";

            //set the shared secret key
            string secret = "5cCxUrv4yN";

            //call the function to create the hash
            string sig = CreateSHA256Hash(apikey + secret + timestamp);

            //prep the URL
            string urlBase = "http://api.stats.com";
            string urlAppend = "/v1/stats/football/cfb/events/";
            string qryStrAppend = "?top25PollId=1&accept=xml&";
            HttpClient client = new HttpClient();


            string url = urlBase + urlAppend + qryStrAppend + "api_key=" + apikey + "&sig=" + sig;

            HttpResponseMessage response = client.GetAsync(url).Result;

            string SAVE_PATH = @"Z:\XML\CFB_LIVE_AP.XML";
            string XMLCONTENT = response.Content.ReadAsStringAsync().Result;


            File.WriteAllText(SAVE_PATH, XMLCONTENT);
        
        
        }

        public static void getSECXML()
        {
            // getStuff

            //get the timestamp value
            string timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds.ToString();

            //grab just the integer portion
            timestamp = timestamp.Substring(0, timestamp.IndexOf("."));

            //set the API key (note that this is to provide a valid key!
            string apikey = "d2gujudevqx32p8rd7atea8s";

            //set the shared secret key
            string secret = "5cCxUrv4yN";

            //call the function to create the hash
            string sig = CreateSHA256Hash(apikey + secret + timestamp);

            string urlBase = "http://api.stats.com";
            string urlAppend = "/v1/stats/football/cfb/events/";
            string qryStrAppend = "?conferenceId=8&accept=xml&";
            HttpClient client = new HttpClient();


            string url = urlBase + urlAppend + qryStrAppend + "api_key=" + apikey + "&sig=" + sig;

            HttpResponseMessage response = client.GetAsync(url).Result;

            string SAVE_PATH = @"Z:\XML\CFB_LIVE_SEC.XML";
            string XMLCONTENT = response.Content.ReadAsStringAsync().Result;


            File.WriteAllText(SAVE_PATH, XMLCONTENT);
            
        
        }




        public static void getCFPXML()
        {
            // getStuff
            //get the timestamp value
            string timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds.ToString();

            //grab just the integer portion
            timestamp = timestamp.Substring(0, timestamp.IndexOf("."));

            //set the API key (note that this is to provide a valid key!
            string apikey = "d2gujudevqx32p8rd7atea8s";

            //set the shared secret key
            string secret = "5cCxUrv4yN";

            //call the function to create the hash
            string sig = CreateSHA256Hash(apikey + secret + timestamp);

            string urlBase = "http://api.stats.com";
            string urlAppend = "/v1/stats/football/cfb/events/";
            string qryStrAppend = "?top25PollId=17&accept=xml&";
            HttpClient client = new HttpClient();


            string url = urlBase + urlAppend + qryStrAppend + "api_key=" + apikey + "&sig=" + sig;

            HttpResponseMessage response = client.GetAsync(url).Result;

            string SAVE_PATH = @"Z:\XML\CFB_LIVE_CFP.XML";
            string XMLCONTENT = response.Content.ReadAsStringAsync().Result;


            File.WriteAllText(SAVE_PATH, XMLCONTENT);
            

        }




        // Async download
        /*
            
        WebClient webdload = new WebClient();
        webdload.DownloadFileAsync();
        */

        // rank the unranked "CFB_LIVE_AP.XML" on the hard drive
        private static void APXMLRanking()
        {

            

             // load the input XML into the program after user click "upload" button
             XElement Org_doc1 = XElement.Load(@"Z:\XML\CFB_LIVE_AP.XML");



             //var APrank = (from node in Org_doc.Elements("sports-statistics").Elements("cfb-scores")
             //              where node.Element("visiting-team").Element("team-rank").Element("rank").Value == "25"
             //              select node).SingleOrDefault();

             //var APrank = Org_doc.GetHashCode();

             //return des_doc


             //XmlDocument Org_doc = new XmlDocument();
             //try
             //{
             //    Org_doc.Load(@"C:\XML\CFB_LIVE.XML");
             //}
             //catch (Exception e)
             //{
             //    System.Diagnostics.Debug.WriteLine(e.ToString());
             //};



             // XmlNode xnlist = Org_doc.SelectSingleNode("//cfb-score[.//team-rank/@rank = '1']");




             XDocument des_doc;
             XDocument des_doc1;

             //des_doc = XDocument.Load(@"C:\XML\CFB_LIVE_APRANK.XML");
             des_doc = new XDocument(
               new XDeclaration("1.0", "utf-8", "yes"),
               new XElement("events"));

             des_doc1 = new XDocument(
               new XDeclaration("1.0", "utf-8", "yes"),
               new XElement("events"));

            // navigate to the node that contains all the games
             XElement parent = des_doc.XPathSelectElement("//events");

             XElement parent1 = des_doc1.XPathSelectElement("//cfb-scores");
             XElement child;
             XElement lchild;
             XElement rchild;
             XElement child1;


            // old XML document structure
            // string part1 = "//event[.//team-rank/@rank = '";
            // string part11 = "//cfb-score[.//team-rank/@playoff-rank = '";

            // navigate the new API XML document to the ranking attribute
             string part1 = "//event[.//rank = '";
             string part11 = "";
             string part2 = "']";

             bool[] list = new bool[26];
             list[1] = false;
             bool[] list1 = new bool[26];
             list1[1] = false;

             // initialization test
             //bool test1 = list[0];
             //test1 = list[1];
             //test1 = list[2];
             //test1 = list[3];
             //test1 = list[4];
             //test1 = list[5];
             //test1 = list[6];
             //test1 = list[7];
             //test1 = list[8];
             //test1 = list[9];
             //test1 = list[10];
             //test1 = list[11];

            // loop for ranking the top25
             for (int i = 1; i <= 25; i++)
             {
                 // string builder for navigating to a specific game
                 string aprank = part1 + i.ToString() + part2;
                 string cfprank = part11 + i.ToString() + part2;

                 //XElement xlist = Org_doc.XPathSelectElement("//cfb-score[.//team-rank/@rank='1']");

                 if (Org_doc1.XPathSelectElement(aprank) != null)
                 {
                     XElement xlist = Org_doc1.XPathSelectElement(aprank);
                     //XElement xlist1 = Org_doc1.XPathSelectElement(cfprank);

                     child = xlist.Element("teams");
                     //lchild = xlist.Descendants().First();
                     //rchild = xlist.Descendants().Last();

                     string lchildPath = "//team[.//text() = 'home' and .//rank = '" + i.ToString() + part2;
                     string rchildPath = "//team[.//text() = 'away' and .//rank = '" + i.ToString() + part2;
                     //team[.//text() = 'home' and .//rank = '1']
                     lchild = Org_doc1.XPathSelectElement(lchildPath);
                     rchild = Org_doc1.XPathSelectElement(rchildPath);
                     //child1 = xlist1;
                     XElement RankedChild = child.Element("team");

                     XElement OtherChild = RankedChild.ElementsAfterSelf().First();



                     // get the rankings of both teams for this game
                     string homerank;
                     string awayrank;





                     // Bye-week CHECK for this all the teams but not Bama and duplicate check
                     if (child != null && !list[i])
                     {
                         // <home/visiting-name><team-name>  name ="Crimson Tide"
                         //bool homeBama = (child.Element("teams").Element("team").Element("nickname").Value == "Crimson Tide");
                         //bool awayBama = (child.Element("teams").Element("team").Attribute("nickname").Value == "Crimson Tide");

                         bool homeBama = (RankedChild.Element("nickname").Value == "Crimson Tide");
                         bool awayBama = (OtherChild.Element("nickname").Value == "Crimson Tide");

                         // no bama game showing on the big screen
                         if (!homeBama && !awayBama)
                         {
                             //homerank = child.Element("home-team").Element("team-rank").Attribute("rank").Value;
                             //awayrank = child.Element("visiting-team").Element("team-rank").Attribute("rank").Value;
                             int home = 0;
                             int away = 0;

                             if (RankedChild.Element("rankings") != null)
                             {
                                 homerank = RankedChild.Element("rankings").Element("ranking").Element("rank").Value;
                                 home = Int32.Parse(homerank); 
                             }
                             
                             if (OtherChild.Element("rankings") != null)
                             {
                                 awayrank = OtherChild.Element("rankings").Element("ranking").Element("rank").Value; ;
                                 away = Int32.Parse(awayrank);
                             }






                             // test of get the rankings of both teams
                             //var home = xlist.Descendants("//home-team")
                             //homerank = xlist.Descendants("home-team/team-rank").Attributes("rank").ToString();
                             // homerank = child.XPathSelectElement("//home-team[.//team-rank/@rank='1']").Element("team");
                             //homerank = child.XPathSelectElement("//home-team/team-rank[.//team-rank/@rank='1']").Attribute("rank").Value;
                             //homerank = homenode.XPathSelectElement("//team-rank").Attribute("rank").Value;
                             //awaynode = child.XPathSelectElement("//visiting-team");
                             //awayrank = awaynode.XPathSelectElement("//team-rank").Attribute("rank").Value;



                             // tag the other team that appear in this rank if the other team is also top 25
                             if (home <= 25 && home >= 1 && away <= 25 && away >= 1)
                             {
                                 if (home == i)
                                 {
                                     list[away] = true;
                                 }
                                 if (away == i)
                                 {
                                     list[home] = true;
                                 }
                             }// end of tag the other team

                             parent.Add(child);
                             des_doc.Save(AppConfig.XmlFile);

                         } // end of no bama games check


                     }// end of bye-week check for AP Rank 
                 }

                 // bye-week check for CPF Rank and duplicate check
                 /*
                 if (child1 != null && !list1[i])
                 {   
                    
                     bool homeBama1 = (child1.Element("home-team").Element("team-name").Attribute("name").Value == "Crimson Tide");
                     bool awayBama1 = (child1.Element("visiting-team").Element("team-name").Attribute("name").Value == "Crimson Tide");
                     // CPF no bama game on big screen check
                     if(!homeBama1 && !awayBama1)
                     {
                         homerank = child1.Element("home-team").Element("team-rank").Attribute("playoff-rank").Value;
                         awayrank = child1.Element("visiting-team").Element("team-rank").Attribute("playoff-rank").Value;
                         int home = Int32.Parse(homerank);
                         int away = Int32.Parse(awayrank);



                         // tag the other team that appear in this rank if the other team is also top 25
                         if (home <= 25 && home >= 1 && away <= 25 && away >= 1)
                         {
                             if (home == i)
                             {
                                 list1[away] = true;
                             }
                             if (away == i)
                             {
                                 list1[home] = true;
                             }
                         }// end of tag the other team
                         parent1.Add(child1);
                         des_doc1.Save(AppConfig.XmlFile1);

                     }// end of CPF no bama game check
                 } // end of CPF rank and duplicate check
                  * 
                  */ 

                 else
                 {   // ranking of this game has been added to the output file earlier, so just move on to the next game

                     // DO NOTHING
                 }


             } // end of for loop

             

        }// end of APRanking()

        private static void CFPXMLRanking()
        {



            // load the input XML into the program after user click "upload" button
            XElement Org_doc1 = XElement.Load(@"Z:\XML\CFB_LIVE_CFP.XML");



            //var APrank = (from node in Org_doc.Elements("sports-statistics").Elements("cfb-scores")
            //              where node.Element("visiting-team").Element("team-rank").Element("rank").Value == "25"
            //              select node).SingleOrDefault();

            //var APrank = Org_doc.GetHashCode();

            //return des_doc


            //XmlDocument Org_doc = new XmlDocument();
            //try
            //{
            //    Org_doc.Load(@"C:\XML\CFB_LIVE.XML");
            //}
            //catch (Exception e)
            //{
            //    System.Diagnostics.Debug.WriteLine(e.ToString());
            //};



            // XmlNode xnlist = Org_doc.SelectSingleNode("//cfb-score[.//team-rank/@rank = '1']");




            XDocument des_doc;
            XDocument des_doc1;

            //des_doc = XDocument.Load(@"C:\XML\CFB_LIVE_APRANK.XML");
            des_doc = new XDocument(
              new XDeclaration("1.0", "utf-8", "yes"),
              new XElement("events"));

            des_doc1 = new XDocument(
              new XDeclaration("1.0", "utf-8", "yes"),
              new XElement("events"));

            // navigate to the node that contains all the games
            XElement parent = des_doc.XPathSelectElement("//events");

            XElement parent1 = des_doc1.XPathSelectElement("//cfb-scores");
            XElement child;
            XElement lchild;
            XElement rchild;
            XElement child1;


            // old XML document structure
            // string part1 = "//event[.//team-rank/@rank = '";
            // string part11 = "//cfb-score[.//team-rank/@playoff-rank = '";

            // navigate the new API XML document to the ranking attribute
            string part1 = "//event[.//rank = '";
            string part11 = "";
            string part2 = "']";

            bool[] list = new bool[26];
            list[1] = false;
            bool[] list1 = new bool[26];
            list1[1] = false;

            // initialization test
            //bool test1 = list[0];
            //test1 = list[1];
            //test1 = list[2];
            //test1 = list[3];
            //test1 = list[4];
            //test1 = list[5];
            //test1 = list[6];
            //test1 = list[7];
            //test1 = list[8];
            //test1 = list[9];
            //test1 = list[10];
            //test1 = list[11];

            // loop for ranking the top25
            for (int i = 1; i <= 25; i++)
            {
                // string builder for navigating to a specific game
                string aprank = part1 + i.ToString() + part2;
                string cfprank = part11 + i.ToString() + part2;

                //XElement xlist = Org_doc.XPathSelectElement("//cfb-score[.//team-rank/@rank='1']");

                if (Org_doc1.XPathSelectElement(aprank) != null)
                {
                    XElement xlist = Org_doc1.XPathSelectElement(aprank);
                    //XElement xlist1 = Org_doc1.XPathSelectElement(cfprank);

                    child = xlist.Element("teams");
                    //lchild = xlist.Descendants().First();
                    //rchild = xlist.Descendants().Last();

                    string lchildPath = "//team[.//text() = 'home' and .//rank = '" + i.ToString() + part2;
                    string rchildPath = "//team[.//text() = 'away' and .//rank = '" + i.ToString() + part2;
                    //team[.//text() = 'home' and .//rank = '1']
                    lchild = Org_doc1.XPathSelectElement(lchildPath);
                    rchild = Org_doc1.XPathSelectElement(rchildPath);
                    //child1 = xlist1;
                    XElement RankedChild = child.Element("team");

                    XElement OtherChild = RankedChild.ElementsAfterSelf().First();



                    // get the rankings of both teams for this game
                    string homerank;
                    string awayrank;





                    // Bye-week CHECK for this all the teams but not Bama and duplicate check
                    if (child != null && !list[i])
                    {
                        // <home/visiting-name><team-name>  name ="Crimson Tide"
                        //bool homeBama = (child.Element("teams").Element("team").Element("nickname").Value == "Crimson Tide");
                        //bool awayBama = (child.Element("teams").Element("team").Attribute("nickname").Value == "Crimson Tide");

                        bool homeBama = (RankedChild.Element("nickname").Value == "Crimson Tide");
                        bool awayBama = (OtherChild.Element("nickname").Value == "Crimson Tide");

                        // no bama game showing on the big screen
                        if (!homeBama && !awayBama)
                        {
                            //homerank = child.Element("home-team").Element("team-rank").Attribute("rank").Value;
                            //awayrank = child.Element("visiting-team").Element("team-rank").Attribute("rank").Value;
                            int home = 0;
                            int away = 0;

                            if (RankedChild.Element("rankings") != null)
                            {
                                homerank = RankedChild.Element("rankings").Element("ranking").Element("rank").Value;
                                home = Int32.Parse(homerank);
                            }

                            if (OtherChild.Element("rankings") != null)
                            {
                                awayrank = OtherChild.Element("rankings").Element("ranking").Element("rank").Value; ;
                                away = Int32.Parse(awayrank);
                            }






                            // test of get the rankings of both teams
                            //var home = xlist.Descendants("//home-team")
                            //homerank = xlist.Descendants("home-team/team-rank").Attributes("rank").ToString();
                            // homerank = child.XPathSelectElement("//home-team[.//team-rank/@rank='1']").Element("team");
                            //homerank = child.XPathSelectElement("//home-team/team-rank[.//team-rank/@rank='1']").Attribute("rank").Value;
                            //homerank = homenode.XPathSelectElement("//team-rank").Attribute("rank").Value;
                            //awaynode = child.XPathSelectElement("//visiting-team");
                            //awayrank = awaynode.XPathSelectElement("//team-rank").Attribute("rank").Value;



                            // tag the other team that appear in this rank if the other team is also top 25
                            if (home <= 25 && home >= 1 && away <= 25 && away >= 1)
                            {
                                if (home == i)
                                {
                                    list[away] = true;
                                }
                                if (away == i)
                                {
                                    list[home] = true;
                                }
                            }// end of tag the other team

                            parent.Add(child);
                            des_doc.Save(AppConfig.XmlFile1);

                        } // end of no bama games check


                    }// end of bye-week check for AP Rank 
                }

                // bye-week check for CPF Rank and duplicate check
                /*
                if (child1 != null && !list1[i])
                {   
                    
                    bool homeBama1 = (child1.Element("home-team").Element("team-name").Attribute("name").Value == "Crimson Tide");
                    bool awayBama1 = (child1.Element("visiting-team").Element("team-name").Attribute("name").Value == "Crimson Tide");
                    // CPF no bama game on big screen check
                    if(!homeBama1 && !awayBama1)
                    {
                        homerank = child1.Element("home-team").Element("team-rank").Attribute("playoff-rank").Value;
                        awayrank = child1.Element("visiting-team").Element("team-rank").Attribute("playoff-rank").Value;
                        int home = Int32.Parse(homerank);
                        int away = Int32.Parse(awayrank);



                        // tag the other team that appear in this rank if the other team is also top 25
                        if (home <= 25 && home >= 1 && away <= 25 && away >= 1)
                        {
                            if (home == i)
                            {
                                list1[away] = true;
                            }
                            if (away == i)
                            {
                                list1[home] = true;
                            }
                        }// end of tag the other team
                        parent1.Add(child1);
                        des_doc1.Save(AppConfig.XmlFile1);

                    }// end of CPF no bama game check
                } // end of CPF rank and duplicate check
                 * 
                 */

                else
                {   // ranking of this game has been added to the output file earlier, so just move on to the next game

                    // DO NOTHING
                }


            } // end of for loop



        }// end of main
       

    } // end of Program Class

} // end of XML namespace
