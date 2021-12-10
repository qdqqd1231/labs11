using System;

namespace lab
{
    public class House
    {

        public House(uint height, uint levels, uint countEntrances, uint countFlats)
        {
            guid = Guid.NewGuid();
            this.levels = levels;
            this.height = height;
            this.countEntrances = countEntrances;
            this.countFlats = countFlats;
        }
        public uint GetAvgFlatsOnLevel()
        {
            return GetAvgFlatsInEntarances() / levels;
        }
        public uint GetAvgFlatsInEntarances()
        {
            return countFlats / countEntrances;
        }

        public uint GetHeightLevel()
        {
            return height / levels;
        }
        public void GetInfo()
        {
            Console.WriteLine(string.Format("Уникальный номер дома : {0}, высота дома : {1}, количество этажей в доме : {2}, количество квартир : {3}, количество подъездов {4}", new object[]
            {
                guid,
                height,
                levels,
                countFlats,
                countEntrances
            }));
        }

        private Guid guid;
        private uint height;
        private uint levels;
        private uint countFlats;
        private uint countEntrances;

    }
}
