using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace zmDataTypes.Material
{
    /// <summary>
    /// Represents a thread size in Unified Thread Standard (UTS) format.
    /// </summary>
    /// <remarks>
    /// see https://en.wikipedia.org/wiki/Unified_Thread_Standard
    /// </remarks>
    public class UtsThreadSize {
        private double _majorDiameterInInches;
        private double _threadDensityInThreadsPerInch;

        private static readonly Dictionary<string, double> _diameterNumberSizesMap = new Dictionary<string, double>
        {
            {  "#0", 0.060 },
            {  "#1", 0.073 },
            {  "#2", 0.086 },
            {  "#3", 0.099 },
            {  "#4", 0.112 },
            {  "#5", 0.125 },
            {  "#6", 0.138 },
            {  "#8", 0.164 },
            { "#10", 0.190 },
            { "#12", 0.216 }
        };

        public double GetMajorDiameter()
        {
            return _majorDiameterInInches;
        }

        public double SetMajorDiameter(double majorDiameter)
        {
            if (majorDiameter <= 0)
                throw new ArgumentException("Major diameter must be greater than zero.");
            _majorDiameterInInches = majorDiameter;
            return _majorDiameterInInches;
        }

        public double GetThreadDensity()
        {
            return _threadDensityInThreadsPerInch;
        }

        public double SetThreadDensity(double threadDensity)
        {
            if (threadDensity <= 0)
                throw new ArgumentException("Thread density must be greater than zero.");
            _threadDensityInThreadsPerInch = threadDensity;
            return _threadDensityInThreadsPerInch;
        }
    }
}