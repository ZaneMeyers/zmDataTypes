# NFPA70: "250.4(A)(1)"" = @(250,4,1,1)
@(
   @{
      Value = 250
      Name  = "Grounding and Bonding"
   },
   @{
      Value = 1
      Name  = "General Requirements for Grounding and Bonding"
   },
   @{
      Value = 1
      Name  = "Grounded Systems"
   },
   @{
      Value = 1
      Name  = "Electrical System Grounding"
   }
)

# COE Specs: "26 12 19.10 1.5.3.1"
# COE Specs: "26 12 19.10 1.6.1 (a.)"
@(
   @{
      Value = 26
      Name  = "ELECTRICAL"
   },
   ### begin sub-DIV meta hierarchy
   @{
      Value = 12
      Name  = "THREE-PHASE, LIQUID-FILLED PAD-MOUNTED TRANSFORMERS"
   },
   @{
      Value = 19
      Name  = ""
   },
   @{
      Value = 10
      Name  = ""
   },
   ### This part is sort of a meta hierarchy with {2,4} members corresponding to a single name.
   ##  This nuance does not affect sorting, however retaining intrinsic names would be far more complex.
   @{
      Value = 1
      Name  = "GENERAL" # this mapping appears to be universal. each subdivision has PART 1 GENERAL, PART 2 PRODUCTS, PART 3 EXECUTION
   },
   @{
      Value = 6
      Name  = "MAINTENANCE" # this mapping is not universal
   },
   @{
      Value = 1
      Name  = "Additions to Operation and Maintenance Data"
   },
   @{
      Value = 1
      Name  = ""
   }
)

# SCAR Project Manual: "1.3.2.38"
# Yakult Exhibit A: B. Section A - Codes and Standards/01 - 00 Construction/2./iv.
@(
   @{
      Value       = 2
      ValueFormat = "BB26,Upper"
      Delimiter   = ". "
      Name        = "Section A - Codes and Standards"
   },
   @{
      Value       = 1
      ValueFormat = "Int,Pad2"
      Delimiter   = " - "
      Name        = "00 Construction"
   },
   @{
      Value       = 2
      ValueFormat = "Int,NoPad"
      Delimiter   = "."
      Name        = ""
   },
   @{
      Value       = 4
      ValueFormat = "Roman,Lower"
      Delimiter   = "."
      Name        = ""
   }
)

# Current determination: hierarchies should be simple ordered[int[]] types. a join table can be provided for additional info.