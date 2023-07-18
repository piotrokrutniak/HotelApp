using Core.Models;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public static class SeedDefaultRooms
    {
        private static int nextRoomNumber = 1;
        private static readonly Random random = new Random();

        private static string[] standards = { "Basic", "Gold", "Premium" };

        public static void Seed(ApplicationDbContext context)
        {
            List<Room> rooms = new List<Room>();

            // Generate 10 rooms
            for (int i = 0; i < 10; i++)
            {
                int roomNumber = GenerateRandomRoomNumber();
                int floor = GenerateRandomFloor();
                decimal price = GenerateRandomPrice();
                string standard = GenerateRandomStandard();

                Room room = new Room
                {
                    RoomNumber = roomNumber,
                    Floor = floor,
                    Price = price,
                    Standard = standard
                };

                // Add room to the list
                rooms.Add(room);
            }

            // Save the rooms to the database
            context.Rooms.AddRange(rooms);
            context.SaveChanges();
        }

        private static int GenerateRandomRoomNumber()
        {
            // Generate a unique room number
            return nextRoomNumber++;
        }

        private static int GenerateRandomFloor()
        {
            // Generate a random floor number between 1 and 10
            return random.Next(1, 11);
        }

        private static decimal GenerateRandomPrice()
        {
            // Generate a random price between 50 and 300
            return random.Next(5000, 30001) / 100.0m;
        }

        private static string GenerateRandomStandard()
        {
            // Generate a random index to get a random standard
            int index = random.Next(0, standards.Length);
            return standards[index];
        }

    }
}
