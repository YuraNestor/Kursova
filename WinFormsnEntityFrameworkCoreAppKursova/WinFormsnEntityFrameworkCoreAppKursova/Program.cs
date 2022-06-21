using WinFormsnEntityFrameworkCoreAppKursova.Models;

namespace WinFormsnEntityFrameworkCoreAppKursova
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //using (ExcursionContext excursionContext = new ExcursionContext())
            //{

            //    excursionContext.Database.EnsureDeleted();
            //    excursionContext.Database.EnsureCreated();


            //    Customer customer1 = new Customer() { Name = "Zamovnik", Deposit = 23 };
            //    Customer customer2 = new Customer() { Name = "Zamovnik2", Deposit = 24 };
            //    excursionContext.Customers.AddRange(customer1, customer2);
            //    ExcursionType excursionType = new ExcursionType() { Name = "prosta" };
            //    ExcursionType excursionType1 = new ExcursionType() { Name = "dlia tovstix" };
            //    excursionContext.ExcursionTypes.AddRange(excursionType, excursionType1);
            //    Excursion microsoft = new Excursion { Name = "Microsoft", ExcType = excursionType, ExcCustomer = customer1, Destination = "Xarkiv", DateOfExcursions = DateTime.Now, Duration = 20, Distance = 10, NumberOfTourists = 12, Price = 25 };
            //    Excursion google = new Excursion { Name = "Google", ExcType = excursionType, ExcCustomer = customer2, Destination = "Lviv", DateOfExcursions = DateTime.Now, Duration = 70, Distance = 100, NumberOfTourists = 16, Price = 26 };
            //    excursionContext.Excursions.AddRange(microsoft, google);
            //    List<Excursion> Companies = new List<Excursion>();
            //    Companies.Add(google);
            //    List<Excursion> Companies2 = new List<Excursion>();
            //    List<Excursion> Companies3 = new List<Excursion>();
            //    Driver driverIvan = new Driver() { Name = "Ivan" };
            //    Driver driverOleg = new Driver() { Name = "Oleg" };
            //    Driver driverTaras = new Driver() { Name = "Taras" };
            //    Driver driverVova = new Driver() { Name = "Vova" };
            //    excursionContext.Drivers.AddRange(driverIvan, driverOleg, driverTaras, driverVova);
            //    Bus tom = new Bus { Brand = "Audi", Model = "Q7", Capacity = 10, FuelConsumption = 5, Excursions = Companies, BDriver = driverIvan, ExcursionTypes = new List<ExcursionType> { excursionType } };
            //    Bus bob = new Bus { Brand = "Lada", Model = "Vesta", Capacity = 10, FuelConsumption = 5, Excursions = Companies, BDriver = driverOleg, ExcursionTypes = new List<ExcursionType> { excursionType } };
            //    Companies2.Add(microsoft);
            //    Bus alice = new Bus { Brand = "Bogdan", Model = "Pro", Capacity = 10, FuelConsumption = 5, Excursions = Companies2, BDriver = driverTaras, ExcursionTypes = new List<ExcursionType> { excursionType } };
            //    Companies3.Add(google);
            //    Companies3.Add(microsoft);
            //    Bus kate = new Bus { Brand = "Mercedes", Model = "c63AMG", Capacity = 10, FuelConsumption = 5, Excursions = Companies3, BDriver = driverVova, ExcursionTypes = new List<ExcursionType> { excursionType1 } };
            //    excursionContext.Buses.AddRange(tom, bob, alice, kate);

            //    excursionContext.SaveChanges();
            //}



            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormLogin());
            
        }
    }
}