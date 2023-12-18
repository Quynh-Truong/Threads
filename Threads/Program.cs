using System.Diagnostics;

namespace Threads
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Car car1 = new Car("Scraps On Wheels");
            Car car2 = new Car("Fender Bender");

            List<Car> carList = new List<Car> { car1, car2 };


            await Console.Out.WriteLineAsync("The race between the kings of barely " +
                "working race cars, will now insue!");
            await Console.Out.WriteLineAsync("Press enter to get nerve-wracking updates.");

            Task task1 = Task.Run(() => RaceAsync(car1));
            Task task2 = Task.Run(() => RaceAsync(car2));

            //while (!task1.IsCompleted && !task2.IsCompleted)
            while (!Task.WhenAll(task1, task2).IsCompleted)
            {
                await Task.Delay(0);

                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(intercept: true).Key == ConsoleKey.Enter)
                    {
                        await Race.GetUpdatesAsync(car1);
                        await Race.GetUpdatesAsync(car2);
                    }
                }
            }


            Race.RaceWinnerAsync(task1, task2);

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
                await Task.Delay(0);

                if (car.DistanceTraveled >= race.RaceDistance)
                {
                    lock (distanceLock)
                    {
                    car.DistanceTraveled -= race.RaceDistance;
                    }

                    await Console.Out.WriteLineAsync($"{car.Name} finished!");
                    raceIsRunning = false;
                }

                if (stopwatch.Elapsed.TotalSeconds >= 15)
                {
                    await Race.RaceSetbacksAsync(car);

                    int distanceCovered = (int)(car.SpeedPerHour * stopwatch.Elapsed.TotalSeconds);
                    lock (distanceLock)
                    {

                    car.DistanceTraveled += distanceCovered;
                    }
                    stopwatch.Reset();
                }



            }
        }

    }
}
