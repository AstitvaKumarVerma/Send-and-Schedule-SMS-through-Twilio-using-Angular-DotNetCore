﻿namespace Twilio_Web_Api_Project.Model
{
    public class ScheduledSmsRequestModel
    {
        public string ToPhoneNumber { get; set; }
        public string Message { get; set; }
        public string ScheduledTime { get; set; } // Use string for date and time

    }
}
