﻿using System;
using System.ComponentModel;
using uBeac.Core;

namespace uBeac.IoT.Models
{
    [Enum("this is enum desc")]
    public enum SensorUnit
    {
        [Description("Undefined sensor unit.")]
        NA = 0,
        Percent = 1,
        Centigrade = 2,
        Fahrenheit = 3,
        Kelvin = 4,
        Meter = 5,
        Count = 6,
        RadiansPerSecond = 7,
        Mile = 8,
        Inch = 9,
        Foot = 10,
        Yard = 11,
        KilometrePerHour = 12,
        MeterPerSecond = 13,
        MilePerHour = 14,
        Knot = 15,
        Atmosphere = 16,
        Bar = 17,
        GravitationalForce = 18,
        Pascal = 19,
        AbsoluteHumidity = 20,
        Torr = 21,
        Decibel = 22,
        DecibelMilliwatts = 23,
        Lux = 24,
        Volt = 25,
        MeterPerSecondSquared = 26,
        Tesla = 27,
        RadiansPerSecondSquared = 28,
        Radian = 29,
        Degree = 30,
        DegreePerSecond = 31,
        bit = 32,
        Byte = 33,
        BytePerSecond = 34,
        bitPerSecond = 35,
        RevolutionsPerMinute = 36,
        Hertz = 37
    }
}
