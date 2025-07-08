using System;
using System.Collections.Generic;
using UnitsNet;

Bare Wire
* Material: Copper/Aluminum/Copper-Clad Aluminum
* Stranding
* Size

namespace zmDataTypes.Material
{
    public class Conductor
    {
        private class ConductorMaterial
        {
            public string Name { get; }
        }

        private struct InsulationProperties
        {
            public string Type { get; }
            public string Color { get; }
            public object TemperatureRating { get; }
        }

        private struct ConductorProperties
        {
            public ConductorMaterial Material { get; }
            public WireGauge WireGauge { get; }
            public InsulationProperties Insulation { get; }
        }

        private struct AmpsKey
        {
            public string ConductorMaterial { get; }
            public int InsulationTempRatingInCelcius { get; }
            public WireGauge ConductorSize { get; }

            public AmpsKey(string material, int insulationTempRating, WireGauge conductorSize)
            {
                ConductorMaterial = material;
                InsulationTempRatingInCelcius = insulationTempRating;
                ConductorSize = conductorSize;
            }
        }

        private static readonly Dictionary<ConductorProperties, double> _conductorAmpacities = new Dictionary<ConductorProperties, double>
        {
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("12 AWG")     ),  15 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("10 AWG")     ),  25 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("8 AWG")      ),  35 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("6 AWG")      ),  40 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("4 AWG")      ),  55 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("3 AWG")      ),  65 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("2 AWG")      ),  75 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("1 AWG")      ),  85 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("1/0 AWG")    ), 100 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("2/0 AWG")    ), 115 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("3/0 AWG")    ), 130 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("4/0 AWG")    ), 150 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("250 kcmil")  ), 170 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("300 kcmil")  ), 195 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("350 kcmil")  ), 210 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("400 kcmil")  ), 225 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("500 kcmil")  ), 260 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("600 kcmil")  ), 285 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("700 kcmil")  ), 315 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("750 kcmil")  ), 320 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("800 kcmil")  ), 330 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("900 kcmil")  ), 355 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("1000 kcmil") ), 375 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("1250 kcmil") ), 405 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("1500 kcmil") ), 435 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("1750 kcmil") ), 455 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 60, WireGauge.FromLookup("2000 kcmil") ), 470 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("12 AWG")     ),  20 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("10 AWG")     ),  30 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("8 AWG")      ),  40 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("6 AWG")      ),  50 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("4 AWG")      ),  65 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("3 AWG")      ),  75 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("2 AWG")      ),  90 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("1 AWG")      ), 100 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("1/0 AWG")    ), 120 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("2/0 AWG")    ), 135 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("3/0 AWG")    ), 155 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("4/0 AWG")    ), 180 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("250 kcmil")  ), 205 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("300 kcmil")  ), 230 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("350 kcmil")  ), 250 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("400 kcmil")  ), 270 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("500 kcmil")  ), 310 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("600 kcmil")  ), 340 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("700 kcmil")  ), 375 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("750 kcmil")  ), 385 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("800 kcmil")  ), 395 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("900 kcmil")  ), 425 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("1000 kcmil") ), 445 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("1250 kcmil") ), 485 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("1500 kcmil") ), 520 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("1750 kcmil") ), 545 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 75, WireGauge.FromLookup("2000 kcmil") ), 560 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("12 AWG")     ),  25 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("10 AWG")     ),  35 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("8 AWG")      ),  45 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("6 AWG")      ),  55 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("4 AWG")      ),  75 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("3 AWG")      ),  85 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("2 AWG")      ), 100 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("1 AWG")      ), 115 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("1/0 AWG")    ), 135 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("2/0 AWG")    ), 150 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("3/0 AWG")    ), 175 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("4/0 AWG")    ), 205 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("250 kcmil")  ), 230 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("300 kcmil")  ), 260 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("350 kcmil")  ), 280 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("400 kcmil")  ), 305 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("500 kcmil")  ), 350 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("600 kcmil")  ), 385 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("700 kcmil")  ), 425 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("750 kcmil")  ), 435 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("800 kcmil")  ), 445 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("900 kcmil")  ), 480 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("1000 kcmil") ), 500 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("1250 kcmil") ), 545 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("1500 kcmil") ), 585 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("1750 kcmil") ), 615 },
            { new AmpsKey( "Aluminum/Copper-Clad Aluminum", 90, WireGauge.FromLookup("2000 kcmil") ), 630 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("14 AWG")     ),  15 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("12 AWG")     ),  20 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("10 AWG")     ),  30 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("8 AWG")      ),  40 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("6 AWG")      ),  55 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("4 AWG")      ),  70 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("3 AWG")      ),  85 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("2 AWG")      ),  95 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("1 AWG")      ), 110 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("1/0 AWG")    ), 125 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("2/0 AWG")    ), 145 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("3/0 AWG")    ), 165 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("4/0 AWG")    ), 195 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("250 kcmil")  ), 215 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("300 kcmil")  ), 240 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("350 kcmil")  ), 260 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("400 kcmil")  ), 280 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("500 kcmil")  ), 320 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("600 kcmil")  ), 350 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("700 kcmil")  ), 385 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("750 kcmil")  ), 400 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("800 kcmil")  ), 410 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("900 kcmil")  ), 435 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("1000 kcmil") ), 455 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("1250 kcmil") ), 495 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("1500 kcmil") ), 525 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("1750 kcmil") ), 545 },
            { new AmpsKey( "Copper",                        60, WireGauge.FromLookup("2000 kcmil") ), 555 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("14 AWG")     ),  20 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("12 AWG")     ),  25 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("10 AWG")     ),  35 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("8 AWG")      ),  50 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("6 AWG")      ),  65 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("4 AWG")      ),  85 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("3 AWG")      ), 100 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("2 AWG")      ), 115 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("1 AWG")      ), 130 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("1/0 AWG")    ), 150 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("2/0 AWG")    ), 175 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("3/0 AWG")    ), 200 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("4/0 AWG")    ), 230 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("250 kcmil")  ), 255 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("300 kcmil")  ), 285 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("350 kcmil")  ), 310 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("400 kcmil")  ), 335 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("500 kcmil")  ), 380 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("600 kcmil")  ), 420 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("700 kcmil")  ), 460 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("750 kcmil")  ), 475 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("800 kcmil")  ), 490 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("900 kcmil")  ), 520 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("1000 kcmil") ), 545 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("1250 kcmil") ), 590 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("1500 kcmil") ), 625 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("1750 kcmil") ), 650 },
            { new AmpsKey( "Copper",                        75, WireGauge.FromLookup("2000 kcmil") ), 665 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("18 AWG")     ),  14 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("16 AWG")     ),  18 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("14 AWG")     ),  25 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("12 AWG")     ),  30 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("10 AWG")     ),  40 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("8 AWG")      ),  55 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("6 AWG")      ),  75 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("4 AWG")      ),  95 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("3 AWG")      ), 115 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("2 AWG")      ), 130 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("1 AWG")      ), 145 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("1/0 AWG")    ), 170 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("2/0 AWG")    ), 195 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("3/0 AWG")    ), 225 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("4/0 AWG")    ), 260 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("250 kcmil")  ), 290 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("300 kcmil")  ), 320 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("350 kcmil")  ), 350 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("400 kcmil")  ), 380 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("500 kcmil")  ), 430 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("600 kcmil")  ), 475 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("700 kcmil")  ), 520 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("750 kcmil")  ), 535 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("800 kcmil")  ), 555 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("900 kcmil")  ), 585 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("1000 kcmil") ), 615 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("1250 kcmil") ), 665 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("1500 kcmil") ), 705 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("1750 kcmil") ), 735 },
            { new AmpsKey( "Copper",                        90, WireGauge.FromLookup("2000 kcmil") ), 750 }
        };

        // aluminumAmpacity = copperAmpacity * 1.19928023912308 + 7.79646886522369
        // copperAmpacity = (aluminumAmpacity - 7.79646886522369) / 1.19928023912308

        // x = conductorArea
        // a = 942.87786 // for 60 degree rating
        // b = 1.08209
        // c = 0.779443
        // ampacity = a * (1 - 1/( b * Math.Pow(x,c) + 1) )

        // x = ampacity
        // conductorArea = Math.Pow(-b * (x-a)/x, -1/c)

    }
}

// "Aluminum"
// "Copper-Clad Aluminum"
// "Copper"


// # 310.14 Ampacities for Conductors Rated 0 Volts — 2000 Volts

// ## (B) Engineering Supervision

// Under engineering supervision, conductor ampacities shall be permitted to be calculated by means of Equation 310.14(B).

// $$
// I = \sqrt{\frac{T_c - T_a}{R_{dc}(1 + Y_c)R_{ca}}} \times 10^{3}\text{amperes}
// $$

// where:
// * $T_c$  = conductor temperature in degrees Celsius (°C)
// * $T_a$  = ambient temperature in degrees Celsius (°C)
// * $R_{dc}$ = dc resistance of 305 mm (1 ft) of conductor in microohms at temperature, Tc
// * $Y_c$  = component ac resistance resulting from skin effect and proximity effect
// * $R_{ca}$ = effective thermal resistance between conductor and surrounding ambient

// # 310.15

// ## (B) Ambient Temperature Correction Factors

// ### (1) General

// Ampacities for ambient temperatures other than those shown in the ampacity tables
// shall be corrected in accordance with Table 310.15(B)(1) or Table 310.15(B)(2),
// or shall be permitted to be calculated using Equation 310.15(B).

// $$
// I' = I\sqrt{\frac{T_c-T'_a}{T_c-T_a}}
// $$

// where:
// * $I'$   = ampacity corrected for ambient temperature
// * $I$    = ampacity shown in the tables
// * $T_c$  = temperature rating of conductor (°C)
// * $T'_a$ = new ambient temperature (°C)
// * $T_a$  = ambient temperature used in the table (°C)