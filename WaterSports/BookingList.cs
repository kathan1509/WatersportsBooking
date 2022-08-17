using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WaterSports
{
    [XmlRoot("AppointmentList")]
    [XmlInclude(typeof(Snorkeling))]
    [XmlInclude(typeof(InflatableWaterpark))]
    [XmlInclude(typeof(Scuba))]
    public class BookingList: IDisposable
    {
            private List<Booking> appointments;

            [XmlArray("Appointments")]
            [XmlArrayItem("Appointment", typeof(Booking))]

            public List<Booking> Appointments { get => appointments; set => appointments = value; }

            public BookingList()
            {
                Appointments = new List<Booking>();
            }

            public void Add(Booking ap)
            {
                Appointments.Add(ap);
            }
            public void Remove(Booking ap)
            {
                Appointments.Remove(ap);
            }

            public void Sort()
            {
                Appointments.Sort();
            }

            public int Count
            {
                get
                {
                    return Appointments.Count;
                }
            }

            public Booking this[int i]
            {
                get
                {
                    return Appointments[i];
                }
                set
                {
                    Appointments[i] = value;
                }
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
}
