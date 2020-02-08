namespace Api.Service
{
    using System;
    using System.Globalization;
    using System.Threading;
    using Microsoft.Owin.Hosting;

    public class Program
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {

                Console.ReadLine();
            }
        }
    }
}