using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Day20
    {
        public static void GetResult()
        {
            var data = "p=<3,0,0>, v=<2,0,0>, a=<-1,0,0>\r\np=<4,0,0>, v=<0,0,0>, a=<-2,0,0>";
            Debug.Assert(GetClosestParticleInTheLongRun(data) == 0);

            data = Helpers.GetDataFromFile("day20.txt");
            var result = "";

            var stopWatch = Stopwatch.StartNew();
            result = GetClosestParticleInTheLongRun(data).ToString();
            stopWatch.Stop();
            Helpers.DisplayDailyResult("20 - 1", result, stopWatch.ElapsedMilliseconds);

            stopWatch = Stopwatch.StartNew();
            //result = GetLettersFoundInMaze(data);
            stopWatch.Stop();
            Helpers.DisplayDailyResult("20 - 2", result, stopWatch.ElapsedMilliseconds);
        }

        private static int GetClosestParticleInTheLongRun(string data)
        {
            var particles = GetParticleList(data);

            var ticks = 0;

            while (ticks++ < 1000)
            {
                foreach (var particle in particles)
                {
                    particle.UpdatePosition();
                }
            }

            var closestParticle = GetClosestParticle(particles);

            return closestParticle.Id;
        }

        private static Particle GetClosestParticle(List<Particle> particles)
        {
            var origo = 0;

            // https://stackoverflow.com/questions/5953552/how-to-get-the-closest-number-from-a-listint-with-linq
            Particle closest = particles.Aggregate((x, y) => Math.Abs(x.Distance - origo) < Math.Abs(y.Distance - origo) ? x : y);

            return closest;
        }

        private static List<Particle> GetParticleList(string data)
        {
            var dataRows = data.Split("\r\n");
            var particles = new List<Particle>();

            for (int i = 0; i < dataRows.Length; i++)
            {
                particles.Add(new Particle(dataRows[i], i));
            }

            return particles;
        }

    }

    class Particle
    {
        public int Id { get; set; }
        public Tuple<int, int, int> Position { get; set; }
        public Tuple<int, int, int> Velocity { get; set; }
        public Tuple<int, int, int> Acceleration { get; set; }

        public int Distance
        {
            get
            {
                return Math.Abs(Position.Item1) + Math.Abs(Position.Item2) + Math.Abs(Position.Item3);
            }

            private set { }
        }

        public Particle(string dataRow, int id)
        {
            Id = id;

            var splitDataRow = dataRow.Split(new string[] { "p=<", ">, v=<", ">, a=<" }, StringSplitOptions.RemoveEmptyEntries);

            var positions = splitDataRow[0].Split(',').Select(int.Parse).ToArray();
            Position = Tuple.Create(positions[0], positions[1], positions[2]);

            var velocities = splitDataRow[1].Split(',').Select(int.Parse).ToArray();
            Velocity = Tuple.Create(velocities[0], velocities[1], velocities[2]);

            var accelerations = splitDataRow[2].Replace(">", "").Split(',').Select(int.Parse).ToArray();
            Acceleration = Tuple.Create(accelerations[0], accelerations[1], accelerations[2]);
        }

        public void UpdatePosition()
        {
            var newVelocity = Tuple.Create(
                Velocity.Item1 + Acceleration.Item1,
                Velocity.Item2 + Acceleration.Item2,
                Velocity.Item3 + Acceleration.Item3
            );

            Velocity = newVelocity;

            var newPosition = Tuple.Create(
                Position.Item1 + Velocity.Item1,
                Position.Item2 + Velocity.Item2,
                Position.Item3 + Velocity.Item3
            );

            Position = newPosition;
        }
    }

}