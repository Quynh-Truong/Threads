using System.Diagnostics;

namespace Threads
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Car car1 = new Car("Scraps On Wheels");
            Car car2 = new Car("Fender Bender");

            await Console.Out.WriteLineAsync("The race between the kings of barely " +
                "working race cars, will now insue!");
            await Console.Out.WriteLineAsync("Press enter to get nerve-wracking updates.");

            Task task1 = Task.Run(() => RaceAsync(car1));
            Task task2 = Task.Run(() => RaceAsync(car2));


            while (!Task.WhenAll(task1, task2).IsCompleted)
            {
                await Task.Delay(0);

                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(intercept: true).Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        await Task.WhenAll(Race.GetUpdatesAsync(car1), Race.GetUpdatesAsync(car2));
                    }
                }
            }


            await Race.RaceWinnerAsync(task1, task2);

            Console.ReadLine();

        }


        public static async Task RaceAsync(Car car)
        {
            object distanceLock = new object();


            Race race = new Race();
            bool raceIsRunning = true;

            Stopwatch stopwatch = new Stopwatch();

            await Console.Out.WriteLineAsync($"{car.Name} has started its treacherous race!");



            while (raceIsRunning)
            {
                stopwatch.Start();
 
                await Task.Delay(500);

                car.DistanceTraveled = car.DistanceTraveled + car.SpeedPerHour;

                if (car.DistanceTraveled >= race.RaceDistance)
                {
                    car.DistanceTraveled -= race.RaceDistance;
                    await Console.Out.WriteLineAsync($"{car.Name} finished!");
                    raceIsRunning = false;
                    break;
                }
 

                if (stopwatch.Elapsed.TotalSeconds >= 15)
                {
                    await Race.RaceSetbacksAsync(car);
                    stopwatch.Restart();
                }
            }
          
        }

    }
}
