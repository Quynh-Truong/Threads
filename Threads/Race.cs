using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
    internal class Race
    {
        public int RaceDistance { get; set; }

        public Race()
        {
            RaceDistance = 8000;
        }


        static public async Task GetUpdatesAsync(Car car)
        {
            await Console.Out.WriteLineAsync($"{car.Name} has driven {car.DistanceTraveled} meters " +
                $"and its speed is {car.SpeedPerHour}!");
        }

        static public async Task RaceSetbacksAsync(Car car)
        {
            Random random = new Random();

            int randomSetback = random.Next(1, 51);


            if (randomSetback == 1)
            {
                await Console.Out.WriteLineAsync($"{car.Name}: Flipping hell! Ran out of fuel! 15 second " +
                    "delay for refueling.");
                await Task.Delay(15000);
            }
            else if (randomSetback == 2 || randomSetback == 3)
            {
                await Console.Out.WriteLineAsync($"{car.Name}: Drat! A flat tire! 10 second delay for a " +
                    "change of tire.");
                await Task.Delay(10000);
            }
            else if (randomSetback >= 4 && randomSetback <= 8)
            {
                await Console.Out.WriteLineAsync($"{car.Name}: Darnation! A bird landed on the windshield " +
                    "and left an unwanted gift! Eight second delay to clean up the mess.");
                await Task.Delay(8000);
            }
            else if (randomSetback >= 9 && randomSetback <= 18)
            {
                await Console.Out.WriteLineAsync($"{car.Name}: Rats! An engine malfunction! The car's " +
                    "speed decreases with one kilometer/h.");
                car.SpeedPerHour -= 1;
            }
            else if (randomSetback >= 19 && randomSetback <= 27)
            {
                await Console.Out.WriteLineAsync($"{car.Name}: For crying out loud?! Distracted by a " +
                    "fly! Two second delay.");
                await Task.Delay(2000);
            }
            else if (randomSetback >= 28 && randomSetback <= 35)
            {
                await Console.Out.WriteLineAsync($"{car.Name}: Fiddlesticks! A " +
                    "jamming gear stick! Five second delay.");
                await Task.Delay(5000);
            }
            else if (randomSetback >= 36 && randomSetback <= 44)
            {
                await Console.Out.WriteLineAsync($"{car.Name}: Farts! Speakers suddenly " +
                    "start blasting music! Three second delay.");
                await Task.Delay(3000);
            }
            else if (randomSetback >= 45 && randomSetback <= 50)
            {
                await Console.Out.WriteLineAsync($"{car.Name}: Cheesenrice! The windshield" +
                    "wipers are acting up! Five second delay.");
                await Task.Delay(5000);
            }

        }
        static public async Task RaceWinnerAsync(Task task1, Task task2)
        {
            Task winningTask = await Task.WhenAny(task1, task2);

            if (winningTask == task1)
            {
                await Console.Out.WriteLineAsync($"Scraps On Wheels is the winner!");
            }
            else if (winningTask == task2)
            {
                await Console.Out.WriteLineAsync($"Fender Bender is the winner!");
            }


        }
    }
}
