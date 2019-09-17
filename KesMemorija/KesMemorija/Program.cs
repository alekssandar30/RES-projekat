using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KesMemorija
{
    public enum Codes
    {
        CODE_ANALOG, CODE_DIGITAL, CODE_CUSTOM, CODE_LIMITSET,
        CODE_SINGLENODE, CODE_MULTIPLENODE, CODE_CONSUMER,
        CODE_SOURCE, CODE_MOTION, CODE_SENSOR
    }


    class Program
    {
        public static readonly object _locker = new object();

        static void Main(string[] args)
        {

            Client client = new Client();
            int izbor = 0;
            //object _locker = new object();

            Console.ForegroundColor = ConsoleColor.Green;

            #region drugi nacin
            //Thread fileThread = new Thread(() =>
            //{
            //    lock (_locker)
            //        FillFile();
            //}); //popuni fajl random podacima
            //fileThread.Start();
            #endregion

            new Thread(() => FillFile()).Start();
            new Thread(() => WriteToBuffer(client.Writter)).Start();

            #region drugi nacin
            /*
            try
            {
                
                Thread t2 = new Thread(() =>
                {
                    lock(_locker)
                        writter.ReadDataFromFile();
                });
                t2.Start();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("InputData.txt error.\n" + e.Message);
            }
            */
            #endregion

            Console.WriteLine(@"
                                 _____        _                     _           _ _ _ 
                                |  __ \      | |                   | |         | (_) |
                                | |  | | ___ | |__  _ __ ___     __| | ___  ___| |_| |
                                | |  | |/ _ \| '_ \| '__/ _ \   / _` |/ _ \/ __| | | |
                                | |__| | (_) | |_) | | | (_) | | (_| | (_) \__ \ | |_|
                                |_____/ \___/|_.__/|_|  \___/   \__,_|\___/|___/_|_(_)
");
            Console.WriteLine("\n\n");
            do
            {
                Console.WriteLine("Unesite komandu:");
                Console.WriteLine("> 1 za novi upis podatka ~ 2 za citanje podataka ~ 3 za direktan unos na historical");
                Console.Write("> ");
                Int32.TryParse(Console.ReadLine(), out izbor);


                switch (izbor)
                {
                    case 1:
                        Menu(client);
                        izbor = 0;
                        break;
                    case 2:
                        ReadData(client);
                        izbor = 0;
                        break;
                    case 3:
                        DirectInput(client);
                        break;
                    default:
                        Console.WriteLine("Uneli ste pogresnu komandu.\n");
                        break;

                }
            } while (izbor != 2);

        }


        private static void DirectInput(Client client)
        {
            PrintMenu();

            int izbor = -1;
            string code = "";
            string idgeo;
            double p = 0;
            Value value;
            string brisanje = "";
            bool bflag = false;


            //Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nIzaberite opciju:\n");


            PrintMenu();

            Console.Write("> ");
            try
            {
                izbor = Int32.Parse(Console.ReadLine());

            }
            catch (Exception e)
            {
                Console.WriteLine("Greska! Izabrali ste nepostojecu opciju.\n" + e.Message);
            }


            #region unos
            switch (izbor)
            {
                case 1:
                    code = "CODE_ANALOG";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    client.ManualWriteToHistory(code, value);

                    break;
                case 2:
                    code = "CODE_DIGITAL";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    client.ManualWriteToHistory(code, value);
                    break;
                case 3:
                    code = "CODE_CUSTOM";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    client.ManualWriteToHistory(code, value);
                    break;
                case 4:
                    code = "CODE_LIMITSET";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    client.ManualWriteToHistory(code, value);
                    break;
                case 5:
                    code = "CODE_SINGLENODE";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    client.ManualWriteToHistory(code, value);
                    break;
                case 6:
                    code = "CODE_MULTIPLENODE";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    client.ManualWriteToHistory(code, value);
                    break;
                case 7:
                    code = "CODE_CONSUMER";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    client.ManualWriteToHistory(code, value);
                    break;
                case 8:
                    code = "CODE_SOURCE";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    client.ManualWriteToHistory(code, value);
                    break;
                case 9:
                    code = "CODE_MOTION";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    client.ManualWriteToHistory(code, value);
                    break;
                case 10:
                    code = "CODE_SENSOR";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    client.ManualWriteToHistory(code, value);
                    break;

                case 11:
                    break;



            }
        }
            #endregion

            #region citanje

            public static void ReadData(Client client)
        {
            string code = "";
          

            Console.Clear();


            Console.WriteLine("Unesite kod: ");
            Console.Write("> ");
            try
            {
                code = Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw new Exception("code mora biti tekst.\n" + ex.Message);
            }

            Console.Clear();
            Console.WriteLine("************Podaci************");
            client.Reader.DisplayData(code);
            Console.WriteLine("**************************************\n");
           

        }

      

        #endregion

        #region pisanje
        public static void Menu(Client writer)
        {
            int izbor = -1;
            string code = "";
            string idgeo;
            double p = 0;
            Value value;
            string brisanje = "";
            bool bflag = false;


            //Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nIzaberite opciju:\n");


            PrintMenu();

            Console.Write("> ");
            try
            {
                izbor = Int32.Parse(Console.ReadLine());

            }
            catch (Exception e)
            {
                Console.WriteLine("Greska! Izabrali ste nepostojecu opciju.\n" + e.Message);
            }


            #region unos
            switch (izbor)
            {
                case 1:
                    code = "CODE_ANALOG";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    writer.WriteToDumpingBuffer(code, value, bflag);

                    break;
                case 2:
                    code = "CODE_DIGITAL";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    writer.WriteToDumpingBuffer(code, value, bflag);
                    break;
                case 3:
                    code = "CODE_CUSTOM";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    writer.WriteToDumpingBuffer(code, value, bflag);
                    break;
                case 4:
                    code = "CODE_LIMITSET";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    writer.WriteToDumpingBuffer(code, value, bflag);
                    break;
                case 5:
                    code = "CODE_SINGLENODE";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    writer.WriteToDumpingBuffer(code, value, bflag);
                    break;
                case 6:
                    code = "CODE_MULTIPLENODE";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    writer.WriteToDumpingBuffer(code, value, bflag);
                    break;
                case 7:
                    code = "CODE_CONSUMER";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    writer.WriteToDumpingBuffer(code, value, bflag);
                    break;
                case 8:
                    code = "CODE_SOURCE";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    writer.WriteToDumpingBuffer(code, value, bflag);
                    break;
                case 9:
                    code = "CODE_MOTION";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    writer.WriteToDumpingBuffer(code, value, bflag);
                    break;
                case 10:
                    code = "CODE_SENSOR";
                    Console.WriteLine("Unesite id geografskog polozaja: ");
                    Console.Write("> ");
                    idgeo = Console.ReadLine();
                    if (CheckGeoId(idgeo))
                        break;
                    Console.WriteLine("Unesite potrosnju u mW/h: ");
                    Console.Write("> ");
                    p = double.Parse(Console.ReadLine());
                    value = new Value(idgeo, p);

                    Console.WriteLine("Da li je ovaj podatak za brisanje? Y/N");
                    brisanje = Console.ReadLine();

                    if (brisanje.ToUpper().Equals("Y"))
                        bflag = true;
                    else
                        bflag = false;


                    writer.WriteToDumpingBuffer(code, value, bflag);
                    break;

                case 11:
                    break;

            }


            #endregion


            #endregion

        }

        public static void PrintMenu()
        {
            Console.WriteLine("1. CODE_ANALOG\n");
            Console.WriteLine("2. CODE_DIGITAL\n ");
            Console.WriteLine("3. CODE_CUSTOM \n");
            Console.WriteLine("4. CODE_LIMITSET \n");
            Console.WriteLine("5. CODE_SINGLENODE \n");
            Console.WriteLine("6. CODE_MULTIPLENODE \n");
            Console.WriteLine("7. CODE_CONSUMER\n ");
            Console.WriteLine("8. CODE_SOURCE\n ");
            Console.WriteLine("9. CODE_MOTION\n ");
            Console.WriteLine("10. CODE_SENSOR\n ");
            Console.WriteLine("11. Return to the main menu\n");
        }

        #region Punjenje fajla
        public static void FillFile()
        {
            string file = "InputData.txt";
            var chars = "0123456789";

            var stringChars = new char[4];
            Codes code;
            Array codes = Enum.GetValues(typeof(Codes));

            Dictionary<string, Value> data = new Dictionary<string, Value>();
            Random random = new Random();


            for (int k = 0; k < 5; k++)
            {

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                var finalString = new String(stringChars);

                Value value = new Value()
                {
                    IDGeoPolozaja = finalString,
                    Potrosnja = random.Next(1000, 2000),
                    Timestamp = DateTime.Now
                };

                code = (Codes)codes.GetValue(random.Next(codes.Length)); //pick random from enum

                if (!data.ContainsKey(code.ToString()))
                    data.Add(code.ToString(), value); //ovaj dict upisujem


                using (Mutex mutex = new Mutex())
                {
                    mutex.WaitOne();
                    lock (_locker)
                    {
                        using (StreamWriter fileStream = new StreamWriter(file, true))
                        {
                            foreach (var el in data)
                            {
                                fileStream.WriteLine("[{0} {1}]", el.Key, el.Value);

                            }

                            fileStream.Flush();
                            fileStream.Close();
                        }
                    }
                    mutex.ReleaseMutex();
                }

            }

        }

        #endregion

        #region slanje dumping bufferu automatski iz fajla
        private static void WriteToBuffer(Writter writter)
        {
            using (var mutex = new Mutex())
            {
                mutex.WaitOne();
                lock (_locker)
                {
                    writter.ReadDataFromFile();
                    //Thread.Sleep(2000);
                }

                mutex.ReleaseMutex();
            }


        }

        #endregion

        private static bool CheckGeoId(string id)
        {
            if (id.Length != 4)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("ID geografskog podrucja mora imati 4 karaktera");
                Console.ResetColor();
                return true;
            }

            return false;
        }
    }


}



