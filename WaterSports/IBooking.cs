using System;
using System.Collections.Generic;
using System.Text;

namespace WaterSports
{
    public interface IBooking
    {
        string AppointmentTime { get; set; }
        Client Client { get; set; }
    }
}
