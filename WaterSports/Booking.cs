using System.Xml.Serialization;

namespace WaterSports
{
    [XmlRoot("AppointmentLists")]
    [XmlInclude(typeof(Snorkeling))]
    [XmlInclude(typeof(InflatableWaterpark))]
    [XmlInclude(typeof(Scuba))]
    public class Booking
    {
        private string appointmentTime;
        private Client client;

        public string AppointmentTime { get => appointmentTime; set => appointmentTime = value; }
        public Client Client { get => client; set => client = value; }
    }
}
