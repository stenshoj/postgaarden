using Postgaarden;
using Postgaarden.Model.Equipments;
using Postgaarden.Model.Persons;
using Postgaarden.Model.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgaardenUnitTest
{
    public class Data
    {
        public IEnumerable<Booking> Bookings { get; private set; }
        public IDictionary<string, Customer> Customers { get; private set; }
        public IDictionary<int, Employee> Employees { get; private set; }
        public IDictionary<int, Equipment> Equipments { get; private set; }
        public List<Room> Rooms { get; private set; }

        public Data()
        {
            InitEquipment();
            InitEmployees();
            InitCustomers();
            InitRooms();
            InitBookings();
        }

        private void InitBookings()
        {
            Bookings = new List<Booking>
            {
               new Booking {
                    Id = 1,
                    StartTime = new DateTime(2015, 08, 02, 08, 00, 00),
                    EndTime = new DateTime(2015, 08, 02, 10, 00, 00),
                    Price = 500,
                    Customer = Customers["12345678"],
                    Employee = Employees[1],
                    Room = Rooms.ElementAt(0)
                },
                new Booking {
                    Id = 2,
                    StartTime = new DateTime(2015, 09, 02, 08, 00, 00),
                    EndTime = new DateTime(2015, 09, 02, 10, 00, 00),
                    Price = 400,
                    Customer = Customers["55522266"],
                    Employee = Employees[4],
                    Room = Rooms.ElementAt(1)
                },
                new Booking {
                    Id = 3,
                    StartTime = new DateTime(2015, 08, 02, 08, 00, 00),
                    EndTime = new DateTime(2015, 08, 02, 10, 00, 00),
                    Price = 800,
                    Customer = Customers["36987412"],
                    Employee = Employees[1],
                    Room = Rooms.ElementAt(1)
                },
                new Booking {
                    Id = 4,
                    StartTime = new DateTime(2015, 08, 03, 08, 00, 00),
                    EndTime = new DateTime(2015, 08, 03, 10, 00, 00),
                    Price = 550,
                    Customer = Customers["32165498"],
                    Employee = Employees[5],
                    Room = Rooms.ElementAt(3)
                },
                new Booking {
                    Id = 5,
                    StartTime = new DateTime(2015, 08, 03, 08, 00, 00),
                    EndTime = new DateTime(2015, 08, 03, 10, 00, 00),
                    Price = 450,
                    Customer = Customers["12345678"],
                    Employee = Employees[4],
                    Room = Rooms.ElementAt(4)
                },
                new Booking {
                    Id = 6,
                    StartTime = new DateTime(2015, 08, 02, 08, 00, 00),
                    EndTime = new DateTime(2015, 08, 02, 10, 00, 00),
                    Price = 700,
                    Customer = Customers["14725836"],
                    Employee = Employees[6],
                    Room = Rooms.ElementAt(5)
                }
            };
        }

        private void InitEmployees()
        {
            Employees = new Dictionary<int, Employee>();
            Employees[1] = new Employee { Id = 1, Name = "Lone Wolf", EmailAddress = "Lone@Wolf.com" };
            Employees[2] = new Employee { Id = 2, Name = "Hans Andersen", EmailAddress = "Hans@Wolf.com" };
            Employees[3] = new Employee { Id = 3, Name = "Peder Emil", EmailAddress = "Peder@Wolf.com" };
            Employees[4] = new Employee { Id = 4, Name = "Anders Jensen", EmailAddress = "Anders@Wolf.com" };
            Employees[5] = new Employee { Id = 5, Name = "Bent Sen", EmailAddress = "Bent@Wolf.com" };
            Employees[6] = new Employee { Id = 6, Name = "Flemming Flemse", EmailAddress = "Flemming@Wolf.com" };
        }

        public void InitCustomers()
        {
            Customers = new Dictionary<string, Customer>();
            Customers["12345678"] = new Customer { Cvr = "12345678", CompanyName = "Global Corp", EmailAddress = "Global@Corp.com", Name = "Ulla Tulla" };
            Customers["32165498"] = new Customer { Cvr = "32165498", CompanyName = "Local Corp", EmailAddress = "Local@Corp.com", Name = "Kalle Tulla" };
            Customers["14725836"] = new Customer { Cvr = "14725836", CompanyName = "Global Inc", EmailAddress = "Global@Inc.com", Name = "Henrik Larsen" };
            Customers["36987412"] = new Customer { Cvr = "36987412", CompanyName = "Local Enterprise", EmailAddress = "Local@Enterprise.com", Name = "Grethe Jensen" };
            Customers["55522266"] = new Customer { Cvr = "55522266", CompanyName = "Global Enterprise", EmailAddress = "Global@Enterprise.com", Name = "Mr. Boss" };
        }

        public void InitEquipment()
        {
            Equipments = new Dictionary<int, Equipment>();
            Equipments[1] = new Equipment("Chair") { Id = 1 };
            Equipments[2] = new Equipment("Whiteboard") { Id = 2 };
            Equipments[3] = new Equipment("Table") { Id = 3 };
            Equipments[4] = new Equipment("Coffee maker") { Id = 4 };
            Equipments[5] = new Equipment("Projector") { Id = 5 };
            Equipments[6] = new Equipment("TV") { Id = 6 };
        }

        public void InitRooms()
        {
            Rooms = new List<Room>();
            var room = new ConferenceRoom { Id = 1, Size = 8 };
            room.Equipments.Add(Equipments[1]);
            room.Equipments.Add(Equipments[3]);
            room.Equipments.Add(Equipments[5]);
            Rooms.Add(room);

            var room2 = new ConferenceRoom { Id = 2, Size = 4 };
            room2.Equipments.Add(Equipments[2]);
            room2.Equipments.Add(Equipments[3]);
            room2.Equipments.Add(Equipments[4]);
            Rooms.Add(room2);

            var room3 = new ConferenceRoom { Id = 3, Size = 8 };
            room3.Equipments.Add(Equipments[1]);
            room3.Equipments.Add(Equipments[6]);
            room3.Equipments.Add(Equipments[5]);
            Rooms.Add(room3);

            var room4 = new ConferenceRoom { Id = 4, Size = 12 };
            room4.Equipments.Add(Equipments[4]);
            room4.Equipments.Add(Equipments[3]);
            room4.Equipments.Add(Equipments[5]);
            Rooms.Add(room4);

            var room5 = new ConferenceRoom { Id = 5, Size = 6 };
            room5.Equipments.Add(Equipments[1]);
            room5.Equipments.Add(Equipments[3]);
            room5.Equipments.Add(Equipments[2]);
            Rooms.Add(room5);

            var room6 = new ConferenceRoom { Id = 6, Size = 6 };
            room6.Equipments.Add(Equipments[2]);
            room6.Equipments.Add(Equipments[3]);
            room6.Equipments.Add(Equipments[5]);
            Rooms.Add(room6);
        }
    }
}
