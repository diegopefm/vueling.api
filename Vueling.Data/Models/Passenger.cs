namespace Vueling.Data.Models
{
    public partial class Passenger
    {
        public Passenger() { }

        public Passenger(int id, string name, string surname, string seat, string flight)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Seat = seat;
            Flight = flight;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Seat { get; set; }
        public string Flight { get; set; }
    }
}
