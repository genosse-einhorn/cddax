using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CddaX.CddaLib
{
    class ScsiException : Exception
    {
        public byte[] SenseBuffer { get; private set; }

        public Boolean IsEmptyDrive
        {
            get 
            {
                return SenseBuffer[2] == 2 && SenseBuffer[12] == 0x3a;
            }
        }

        public ScsiException(byte[] sensebuf) : base(SenseBufToStr(sensebuf))
        {
            this.SenseBuffer = sensebuf;
        }

        private static string SenseBufToStr(byte[] sense)
        {
            StringBuilder r = new StringBuilder();

            int senseKey = sense[2] & 0xf;
            int senseAsc = sense[12];
            int senseAscQ = sense[13];

            if (senseKey < SenseKeyDb.Length)
                r.Append(SenseKeyDb[senseKey]);

            for (int i = 0; i < SenseDb.Length; ++i)
            {
                if ((SenseDb[i].Key == 0xff  || SenseDb[i].Key == senseKey)
                        && (SenseDb[i].Asc == 0xff || SenseDb[i].Asc == senseAsc)
                        && (SenseDb[i].AscQ == 0xff || SenseDb[i].AscQ == senseAscQ))
                {
                    r.Append(SenseDb[i].Desc);
                    return r.ToString();
                }
            }

            // Not found in the sense table? Emit hex for debugging
            r.Append("Unknown SCSI Error ");

            string hex = "0123456789ABCDEF";

            foreach (byte s in sense)
            {
                r.Append(hex[(int)(s >> 4)]);
                r.Append(hex[(int)(s & 0xf)]);
            }

            return r.ToString();
        }

        private struct SenseDbEntry
        {
            public byte Key;
            public byte Asc;
            public byte AscQ;
            public string Desc;

            public SenseDbEntry(byte Key, byte Asc, byte AscQ, string Desc)
            {
                this.Key = Key;
                this.Asc = Asc;
                this.AscQ = AscQ;
                this.Desc = Desc;
            }
        }

        private static string[] SenseKeyDb = new string[] {
            "No Sense: ",
            "Recovered Error: ",
            "Not Ready: ",
            "Medium Error: ",
            "Hardware Error: ",
            "Illegal Request: ",
            "Unit Attention: ",
            "Data Protect: ",
            "Blank Check: ",
            "Vendor Specific Error: ",
            "Copy Aborted: ",
            "Aborted Command: ",
            "Equal: ",
            "Volume Overflow: ",
            "Miscompare: "
        };

        private static SenseDbEntry[] SenseDb = new SenseDbEntry[] {
            // some text stolen from the MMC-5 spec
            new SenseDbEntry(0x0, 0x00, 0x00, "NO ADDITIONAL SENSE INFORMATION"),
            new SenseDbEntry(0xB, 0x00, 0x06, "I/O PROCESS TERMINATED"),
            new SenseDbEntry(0x5, 0x00, 0x16, "OPERATION IN PROGRESS"),
            new SenseDbEntry(0x4, 0x00, 0x17, "CLEANING REQUESTED"),
            new SenseDbEntry(0x3, 0x02, 0x00, "NO SEEK COMPLETE"),
            new SenseDbEntry(0x2, 0x04, 0x00, "LOGICAL UNIT NOT READY, CAUSE NOT REPORTABLE"),
            new SenseDbEntry(0x2, 0x04, 0x01, "LOGICAL UNIT IS IN PROCESS OF BECOMING READY"),
            new SenseDbEntry(0x2, 0x04, 0x02, "LOGICAL UNIT NOT READY, INITIALIZING CMD. REQUIRED"),
            new SenseDbEntry(0x2, 0x04, 0x03, "LOGICAL UNIT NOT READY, MANUAL INTERVENTION REQUIRED"),
            new SenseDbEntry(0x0, 0x04, 0x04, "LOGICAL UNIT NOT READY, FORMAT IN PROGRESS"),
            new SenseDbEntry(0x2, 0x04, 0x04, "LOGICAL UNIT NOT READY, FORMAT IN PROGRESS"),
            new SenseDbEntry(0x2, 0x04, 0x07, "LOGICAL UNIT NOT READY, OPERATION IN PROGRESS"),
            new SenseDbEntry(0x2, 0x04, 0x08, "LOGICAL UNIT NOT READY, LONG WRITE IN PROGRESS"),
            new SenseDbEntry(0x2, 0x04, 0x09, "LOGICAL UNIT NOT READY, SELF-TEST IN PROGRESS"),
            new SenseDbEntry(0x4, 0x05, 0x00, "LOGICAL UNIT DOES NOT RESPOND TO SELECTION"),
            new SenseDbEntry(0x3, 0x06, 0x00, "NO REFERENCE POSITION FOUND"),
            new SenseDbEntry(0x4, 0x08, 0x00, "LOGICAL UNIT COMMUNICATION FAILURE"),
            new SenseDbEntry(0x4, 0x08, 0x01, "LOGICAL UNIT COMMUNICATION TIMEOUT"),
            new SenseDbEntry(0x4, 0x08, 0x02, "LOGICAL UNIT COMMUNICATION PARITY ERROR"),
            new SenseDbEntry(0x4, 0x08, 0x03, "LOGICAL UNIT COMMUNICATION CRC ERROR (ULTRA-DMA/32)"),
            new SenseDbEntry(0x4, 0x09, 0x00, "TRACK FOLLOWING ERROR"),
            new SenseDbEntry(0x4, 0x09, 0x01, "TRACKING SERVO FAILURE"),
            new SenseDbEntry(0x4, 0x09, 0x02, "FOCUS SERVO FAILURE"),
            new SenseDbEntry(0x4, 0x09, 0x03, "SPINDLE SERVO FAILURE"),
            new SenseDbEntry(0x4, 0x09, 0x04, "HEAD SELECT FAULT"),
            new SenseDbEntry(0x6, 0x0A, 0x00, "ERROR LOG OVERFLOW"),
            new SenseDbEntry(0x1, 0x0B, 0x00, "WARNING"),
            new SenseDbEntry(0x1, 0x0B, 0x01, "WARNING - SPECIFIED TEMPERATURE EXCEEDED"),
            new SenseDbEntry(0x1, 0x0B, 0x02, "WARNING - ENCLOSURE DEGRADED"),
            new SenseDbEntry(0x1, 0x0B, 0x03, "WARNING - BACKGROUND SELF-TEST FAILED"),
            new SenseDbEntry(0x1, 0x0B, 0x04, "WARNING - BACKGROUND PRE-SCAN DETECTED MEDIUM ERROR"),
            new SenseDbEntry(0x1, 0x0B, 0x05, "WARNING - BACKGROUND MEDIUM SCAN DETECTED MEDIUM ERROR"),
            new SenseDbEntry(0x3, 0x0C, 0x00, "WRITE ERROR"),
            new SenseDbEntry(0x1, 0x0C, 0x01, "WRITE ERROR - RECOVERED WITH AUTO-REALLOCATION"),
            new SenseDbEntry(0x3, 0x0C, 0x02, "WRITE ERROR - AUTO-REALLOCATION FAILED"),
            new SenseDbEntry(0x3, 0x0C, 0x03, "WRITE ERROR - RECOMMEND REASSIGNMENT"),
            new SenseDbEntry(0x2, 0x0C, 0x07, "WRITE ERROR - RECOVERY NEEDED"),
            new SenseDbEntry(0x3, 0x0C, 0x07, "WRITE ERROR - RECOVERY NEEDED"),
            new SenseDbEntry(0x3, 0x0C, 0x08, "WRITE ERROR - RECOVERY FAILED"),
            new SenseDbEntry(0x3, 0x0C, 0x09, "WRITE ERROR - LOSS OF STREAMING"),
            new SenseDbEntry(0x3, 0x0C, 0x0A, "WRITE ERROR - PADDING BLOCKS ADDED"),
            new SenseDbEntry(0x2, 0x0C, 0x0F, "DEFECTS IN ERROR WINDOW"),
            new SenseDbEntry(0x3, 0x11, 0x00, "UNRECOVERED READ ERROR"),
            new SenseDbEntry(0x3, 0x11, 0x01, "READ RETRIES EXHAUSTED"),
            new SenseDbEntry(0x3, 0x11, 0x02, "ERROR TOO LONG TO CORRECT"),
            new SenseDbEntry(0x3, 0x11, 0x05, "L-EC UNCORRECTABLE ERROR"),
            new SenseDbEntry(0x3, 0x11, 0x06, "CIRC UNRECOVERED ERROR"),
            new SenseDbEntry(0x3, 0x11, 0x0F, "ERROR READING UPC/EAN NUMBER"),
            new SenseDbEntry(0x3, 0x11, 0x10, "ERROR READING ISRC NUMBER"),
            new SenseDbEntry(0xB, 0x11, 0x11, "READ ERROR - LOSS OF STREAMING"),
            new SenseDbEntry(0x3, 0x15, 0x00, "RANDOM POSITIONING ERROR"),
            new SenseDbEntry(0x4, 0x15, 0x00, "RANDOM POSITIONING ERROR"),
            new SenseDbEntry(0x3, 0x15, 0x01, "MECHANICAL POSITIONING ERROR"),
            new SenseDbEntry(0x4, 0x15, 0x01, "MECHANICAL POSITIONING ERROR"),
            new SenseDbEntry(0x3, 0x15, 0x02, "POSITIONING ERROR DETECTED BY READ OF MEDIUM"),
            new SenseDbEntry(0x1, 0x17, 0x00, "RECOVERED DATA WITH NO ERROR CORRECTION APPLIED"),
            new SenseDbEntry(0x1, 0x17, 0x01, "RECOVERED DATA WITH RETRIES"),
            new SenseDbEntry(0x1, 0x17, 0x02, "RECOVERED DATA WITH POSITIVE HEAD OFFSET"),
            new SenseDbEntry(0x1, 0x17, 0x03, "RECOVERED DATA WITH NEGATIVE HEAD OFFSET"),
            new SenseDbEntry(0x1, 0x17, 0x04, "RECOVERED DATA WITH RETRIES AND/OR CIRC APPLIED"),
            new SenseDbEntry(0x1, 0x17, 0x05, "RECOVERED DATA USING PREVIOUS SECTOR ID"),
            new SenseDbEntry(0x1, 0x17, 0x07, "RECOVERED DATA WITHOUT ECC - RECOMMEND REASSIGNMENT"),
            new SenseDbEntry(0x1, 0x17, 0x08, "RECOVERED DATA WITHOUT ECC - RECOMMEND REWRITE"),
            new SenseDbEntry(0x1, 0x17, 0x09, "RECOVERED DATA WITHOUT ECC - DATA REWRITTEN"),
            new SenseDbEntry(0x1, 0x18, 0x00, "RECOVERED DATA WITH ERROR CORRECTION APPLIED"),
            new SenseDbEntry(0x1, 0x18, 0x01, "RECOVERED DATA WITH ERROR CORR. & RETRIES APPLIED"),
            new SenseDbEntry(0x1, 0x18, 0x02, "RECOVERED DATA - DATA AUTO-REALLOCATED"),
            new SenseDbEntry(0x1, 0x18, 0x03, "RECOVERED DATA WITH CIRC"),
            new SenseDbEntry(0x1, 0x18, 0x04, "RECOVERED DATA WITH L-EC"),
            new SenseDbEntry(0x1, 0x18, 0x05, "RECOVERED DATA - RECOMMEND REASSIGNMENT"),
            new SenseDbEntry(0x1, 0x18, 0x06, "RECOVERED DATA - RECOMMEND REWRITE"),
            new SenseDbEntry(0x1, 0x18, 0x08, "RECOVERED DATA WITH LINKING"),
            new SenseDbEntry(0x5, 0x1A, 0x00, "PARAMETER LIST LENGTH ERROR"),
            new SenseDbEntry(0x4, 0x1B, 0x00, "SYNCHRONOUS DATA TRANSFER ERROR"),
            new SenseDbEntry(0xA, 0x1D, 0x00, "MISCOMPARE DURING VERIFY OPERATION"),
            new SenseDbEntry(0x5, 0x20, 0x00, "INVALID COMMAND OPERATION CODE"),
            new SenseDbEntry(0x5, 0x21, 0x00, "LOGICAL BLOCK ADDRESS OUT OF RANGE"),
            new SenseDbEntry(0x5, 0x21, 0x01, "INVALID ELEMENT ADDRESS"),
            new SenseDbEntry(0x5, 0x21, 0x02, "INVALID ADDRESS FOR WRITE"),
            new SenseDbEntry(0x5, 0x21, 0x03, "INVALID WRITE CROSSING LAYER JUMP"),
            new SenseDbEntry(0x5, 0x22, 0x00, "ILLEGAL FUNCTION"),
            new SenseDbEntry(0x5, 0x24, 0x00, "INVALID FIELD IN CDB"),
            new SenseDbEntry(0x5, 0x25, 0x00, "LOGICAL UNIT NOT SUPPORTED"),
            new SenseDbEntry(0x5, 0x26, 0x00, "INVALID FIELD IN PARAMETER LIST"),
            new SenseDbEntry(0x5, 0x26, 0x01, "PARAMETER NOT SUPPORTED"),
            new SenseDbEntry(0x5, 0x26, 0x02, "PARAMETER VALUE INVALID"),
            new SenseDbEntry(0x5, 0x26, 0x03, "THRESHOLD PARAMETERS NOT SUPPORTED"),
            new SenseDbEntry(0x5, 0x26, 0x04, "INVALID RELEASE OF PERSISTENT RESERVATION"),
            new SenseDbEntry(0x7, 0x27, 0x00, "WRITE PROTECTED"),
            new SenseDbEntry(0x7, 0x27, 0x01, "HARDWARE WRITE PROTECTED"),
            new SenseDbEntry(0x7, 0x27, 0x02, "LOGICAL UNIT SOFTWARE WRITE PROTECTED"),
            new SenseDbEntry(0x7, 0x27, 0x03, "ASSOCIATED WRITE PROTECT"),
            new SenseDbEntry(0x7, 0x27, 0x04, "PERSISTENT WRITE PROTECT"),
            new SenseDbEntry(0x7, 0x27, 0x05, "PERMANENT WRITE PROTECT"),
            new SenseDbEntry(0x7, 0x27, 0x06, "CONDITIONAL WRITE PROTECT"),
            new SenseDbEntry(0x6, 0x28, 0x00, "NOT READY TO READY CHANGE, MEDIUM MAY HAVE CHANGED"),
            new SenseDbEntry(0x6, 0x28, 0x01, "IMPORT OR EXPORT ELEMENT ACCESSED"),
            new SenseDbEntry(0x6, 0x28, 0x02, "FORMAT-LAYER MAY HAVE CHANGED"),
            new SenseDbEntry(0x6, 0x29, 0x00, "POWER ON, RESET, OR BUS DEVICE RESET OCCURRED"),
            new SenseDbEntry(0x6, 0x29, 0x01, "POWER ON OCCURRED"),
            new SenseDbEntry(0x6, 0x29, 0x02, "BUS RESET OCCURRED"),
            new SenseDbEntry(0x6, 0x29, 0x03, "BUS DEVICE RESET FUNCTION OCCURRED"),
            new SenseDbEntry(0x6, 0x29, 0x04, "DEVICE INTERNAL RESET"),
            new SenseDbEntry(0x6, 0x2A, 0x00, "PARAMETERS CHANGED"),
            new SenseDbEntry(0x6, 0x2A, 0x01, "MODE PARAMETERS CHANGED"),
            new SenseDbEntry(0x6, 0x2A, 0x02, "LOG PARAMETERS CHANGED"),
            new SenseDbEntry(0x6, 0x2A, 0x03, "RESERVATIONS PREEMPTED"),
            new SenseDbEntry(0x5, 0x2C, 0x00, "COMMAND SEQUENCE ERROR"),
            new SenseDbEntry(0x5, 0x2C, 0x03, "CURRENT PROGRAM AREA IS NOT EMPTY"),
            new SenseDbEntry(0x5, 0x2C, 0x04, "CURRENT PROGRAM AREA IS EMPTY"),
            new SenseDbEntry(0x6, 0x2E, 0x00, "INSUFFICIENT TIME FOR OPERATION"),
            new SenseDbEntry(0x6, 0x2F, 0x00, "COMMANDS CLEARED BY ANOTHER INITIATOR"),
            new SenseDbEntry(0x2, 0x30, 0x00, "INCOMPATIBLE MEDIUM INSTALLED"),
            new SenseDbEntry(0x5, 0x30, 0x00, "INCOMPATIBLE MEDIUM INSTALLED"),
            new SenseDbEntry(0x2, 0x30, 0x01, "CANNOT READ MEDIUM - UNKNOWN FORMAT"),
            new SenseDbEntry(0x5, 0x30, 0x01, "CANNOT READ MEDIUM - UNKNOWN FORMAT"),
            new SenseDbEntry(0x2, 0x30, 0x02, "CANNOT READ MEDIUM - INCOMPATIBLE FORMAT"),
            new SenseDbEntry(0x5, 0x30, 0x02, "CANNOT READ MEDIUM - INCOMPATIBLE FORMAT"),
            new SenseDbEntry(0x2, 0x30, 0x03, "CLEANING CARTRIDGE INSTALLED"),
            new SenseDbEntry(0x5, 0x30, 0x03, "CLEANING CARTRIDGE INSTALLED"),
            new SenseDbEntry(0x2, 0x30, 0x04, "CANNOT WRITE MEDIUM - UNKNOWN FORMAT"),
            new SenseDbEntry(0x5, 0x30, 0x04, "CANNOT WRITE MEDIUM - UNKNOWN FORMAT"),
            new SenseDbEntry(0x2, 0x30, 0x05, "CANNOT WRITE MEDIUM - INCOMPATIBLE FORMAT"),
            new SenseDbEntry(0x5, 0x30, 0x05, "CANNOT WRITE MEDIUM - INCOMPATIBLE FORMAT"),
            new SenseDbEntry(0x2, 0x30, 0x06, "CANNOT FORMAT MEDIUM - INCOMPATIBLE MEDIUM"),
            new SenseDbEntry(0x5, 0x30, 0x06, "CANNOT FORMAT MEDIUM - INCOMPATIBLE MEDIUM"),
            new SenseDbEntry(0x2, 0x30, 0x07, "CLEANING FAILURE"),
            new SenseDbEntry(0x5, 0x30, 0x07, "CLEANING FAILURE"),
            new SenseDbEntry(0x5, 0x30, 0x08, "CANNOT WRITE - APPLICATION CODE MISMATCH"),
            new SenseDbEntry(0x5, 0x30, 0x09, "CURRENT SESSION NOT FIXATED FOR APPEND"),
            new SenseDbEntry(0x5, 0x30, 0x10, "MEDIUM NOT FORMATTED"),
            new SenseDbEntry(0x2, 0x30, 0x11, "CANNOT WRITE MEDIUM - UNSUPPORTED MEDIUM VERSION"),
            new SenseDbEntry(0x5, 0x30, 0x11, "CANNOT WRITE MEDIUM - UNSUPPORTED MEDIUM VERSION"),
            new SenseDbEntry(0x3, 0x31, 0x00, "MEDIUM FORMAT CORRUPTED"),
            new SenseDbEntry(0x3, 0x31, 0x01, "FORMAT COMMAND FAILED"),
            new SenseDbEntry(0x3, 0x31, 0x02, "ZONED FORMATTING FAILED DUE TO SPARE LINKING"),
            new SenseDbEntry(0x3, 0x32, 0x00, "NO DEFECT SPARE LOCATION AVAILABLE"),
            new SenseDbEntry(255, 0x34, 0x00, "ENCLOSURE FAILURE"),
            new SenseDbEntry(255, 0x35, 0x00, "ENCLOSURE SERVICES FAILURE"),
            new SenseDbEntry(255, 0x35, 0x01, "UNSUPPORTED ENCLOSURE FUNCTION"),
            new SenseDbEntry(255, 0x35, 0x02, "ENCLOSURE SERVICES UNAVAILABLE"),
            new SenseDbEntry(255, 0x35, 0x03, "ENCLOSURE SERVICES TRANSFER FAILURE"),
            new SenseDbEntry(255, 0x35, 0x04, "RNCLOSURE SERVICES TRANSFER REFUSED"),
            new SenseDbEntry(255, 0x35, 0x05, "ENCLOSURE SERVICES CHECKSUM ERROR"),
            new SenseDbEntry(0x1, 0x37, 0x00, "ROUNDED PARAMETER"),
            new SenseDbEntry(0x5, 0x39, 0x00, "SAVING PARAMETERS NOT SUPPORTED"),
            new SenseDbEntry(0x2, 0x3A, 0x00, "MEDIUM NOT PRESENT"),
            new SenseDbEntry(0x2, 0x3A, 0x01, "MEDIUM NOT PRESENT - TRAY CLOSED"),
            new SenseDbEntry(0x2, 0x3A, 0x02, "MEDIUM NOT PRESENT - TRAY OPEN"),
            new SenseDbEntry(0x2, 0x3A, 0x03, "MEDIUM NOT PRESENT - LOADABLE"),
            new SenseDbEntry(0x6, 0x3B, 0x0D, "MEDIUM DESTINATION ELEMENT FULL"),
            new SenseDbEntry(0x6, 0x3B, 0x0E, "MEDIUM SOURCE ELEMENT EMPTY"),
            new SenseDbEntry(0x6, 0x3B, 0x0F, "END OF MEDIUM REACHED"),
            new SenseDbEntry(0x6, 0x3B, 0x11, "MEDIUM MAGAZINE NOT ACCESSIBLE"),
            new SenseDbEntry(0x6, 0x3B, 0x12, "MEDIUM MAGAZINE REMOVED"),
            new SenseDbEntry(0x6, 0x3B, 0x13, "MEDIUM MAGAZINE INSERTED"),
            new SenseDbEntry(0x6, 0x3B, 0x14, "MEDIUM MAGAZINE LOCKED"),
            new SenseDbEntry(0x6, 0x3B, 0x15, "MEDIUM MAGAZINE UNLOCKED"),
            new SenseDbEntry(0x4, 0x3B, 0x16, "MECHANICAL POSITIONING OR CHANGER ERROR"),
            new SenseDbEntry(0x5, 0x3D, 0x00, "INVALID BITS IN IDENTIFY MESSAGE"),
            new SenseDbEntry(0x2, 0x3E, 0x00, "LOGICAL UNIT HAS NOT SELF-CONFIGURED YET"),
            new SenseDbEntry(0x4, 0x3E, 0x01, "LOGICAL UNIT FAILURE"),
            new SenseDbEntry(0x4, 0x3E, 0x02, "TIMEOUT ON LOGICAL UNIT"),
            new SenseDbEntry(0x6, 0x3F, 0x00, "TARGET OPERATING CONDITIONS HAVE CHANGED"),
            new SenseDbEntry(0x6, 0x3F, 0x01, "MICROCODE HAS BEEN CHANGED"),
            new SenseDbEntry(0x6, 0x3F, 0x02, "CHANGED OPERATING DEFINITION"),
            new SenseDbEntry(0x6, 0x3F, 0x03, "INQUIRY DATA HAS CHANGED"),
            new SenseDbEntry(0x4, 0x40, 0xff, "DIAGNOSTIC FAILURE ON COMPONENT"),
            new SenseDbEntry(0x5, 0x43, 0x00, "MESSAGE ERROR"),
            new SenseDbEntry(0x4, 0x44, 0x00, "INTERNAL TARGET FAILURE"),
            new SenseDbEntry(0xB, 0x45, 0x00, "SELECT OR RESELECT FAILURE"),
            new SenseDbEntry(0x4, 0x46, 0x00, "UNSUCCESSFUL SOFT RESET"),
            new SenseDbEntry(0x4, 0x47, 0x00, "SCSI PARITY ERROR"),
            new SenseDbEntry(0xB, 0x48, 0x00, "INITIATOR DETECTED ERROR MESSAGE RECEIVED"),
            new SenseDbEntry(0xB, 0x49, 0x00, "INVALID MESSAGE ERROR"),
            new SenseDbEntry(0x4, 0x4A, 0x00, "COMMAND PHASE ERROR"),
            new SenseDbEntry(0x4, 0x4B, 0x00, "DATA PHASE ERROR"),
            new SenseDbEntry(0x4, 0x4C, 0x00, "LOGICAL UNIT FAILED SELF-CONFIGURATION"),
            new SenseDbEntry(0xB, 0x4D, 0xff, "TAGGED OVERLAPPED COMMANDS"),
            new SenseDbEntry(0xB, 0x4E, 0x00, "OVERLAPPED COMMANDS ATTEMPTED"),
            new SenseDbEntry(0x3, 0x51, 0x00, "ERASE FAILURE"),
            new SenseDbEntry(0x3, 0x51, 0x01, "ERASE FAILURE - INCOMPLETE ERASE OPERATION DETECTED"),
            new SenseDbEntry(0x4, 0x53, 0x00, "MEDIA LOAD OR EJECT FAILED"),
            new SenseDbEntry(0x5, 0x53, 0x02, "MEDIUM REMOVAL PREVENTED"),
            new SenseDbEntry(0x5, 0x55, 0x00, "SYSTEM RESOURCE FAILURE"),
            new SenseDbEntry(0x3, 0x57, 0x00, "UNABLE TO RECOVER TABLE-OF-CONTENTS"),
            new SenseDbEntry(0x6, 0x5A, 0x00, "OPERATOR REQUEST OR STATE CHANGE INPUT"),
            new SenseDbEntry(0x6, 0x5A, 0x01, "OPERATOR MEDIUM REMOVAL REQUEST"),
            new SenseDbEntry(0x6, 0x5A, 0x02, "OPERATOR SELECTED WRITE PROTECT"),
            new SenseDbEntry(0x6, 0x5A, 0x03, "OPERATOR SELECTED WRITE PERMIT"),
            new SenseDbEntry(0x6, 0x5B, 0x00, "LOG EXCEPTION"),
            new SenseDbEntry(0x6, 0x5B, 0x01, "THRESHOLD CONDITION MET"),
            new SenseDbEntry(0x6, 0x5B, 0x02, "LOG COUNTER AT MAXIMUM"),
            new SenseDbEntry(0x6, 0x5B, 0x03, "LOG LIST CODES EXHAUSTED"),
            new SenseDbEntry(0x1, 0x5D, 0x00, "FAILURE PREDICTION THRESHOLD EXCEEDED"),
            new SenseDbEntry(0x1, 0x5D, 0x01, "MEDIA FAILURE PREDICTION THRESHOLD EXCEEDED"),
            new SenseDbEntry(0x1, 0x5D, 0x02, "LOGICAL UNIT FAILURE PREDICTION THRESHOLD EXCEEDED"),
            new SenseDbEntry(0x1, 0x5D, 0x03, "SPARE AREA EXHAUSTION FAILURE PREDICTION THRESHOLD EXCEEDED"),
            new SenseDbEntry(0x1, 0x5D, 0xFF, "FAILURE PREDICTION THRESHOLD EXCEEDED (FALSE)"),
            new SenseDbEntry(0x6, 0x5E, 0x00, "LOW POWER CONDITION ON"),
            new SenseDbEntry(0x6, 0x5E, 0x01, "IDLE CONDITION ACTIVATED BY TIMER"),
            new SenseDbEntry(0x6, 0x5E, 0x02, "STANDBY CONDITION ACTIVATED BY TIMER"),
            new SenseDbEntry(0x6, 0x5E, 0x03, "IDLE CONDITION ACTIVATED BY COMMAND"),
            new SenseDbEntry(0x6, 0x5E, 0x04, "STANDBY CONDITION ACTIVATED BY COMMAND"),
            new SenseDbEntry(0x5, 0x63, 0x00, "END OF USER AREA ENCOUNTERED ON THIS TRACK"),
            new SenseDbEntry(0x5, 0x63, 0x01, "PACKET DOES NOT FIT IN AVAILABLE SPACE"),
            new SenseDbEntry(0x5, 0x64, 0x00, "ILLEGAL MODE FOR THIS TRACK"),
            new SenseDbEntry(0x5, 0x64, 0x01, "INVALID PACKET SIZE"),
            new SenseDbEntry(0x4, 0x65, 0x00, "VOLTAGE FAULT"),
            new SenseDbEntry(0x5, 0x6F, 0x00, "COPY PROTECTION KEY EXCHANGE FAILURE - AUTHENTICATION FAILURE"),
            new SenseDbEntry(0x5, 0x6F, 0x01, "COPY PROTECTION KEY EXCHANGE FAILURE - KEY NOT PRESENT"),
            new SenseDbEntry(0x5, 0x6F, 0x02, "COPY PROTECTION KEY EXCHANGE FAILURE - KEY NOT ESTABLISHED"),
            new SenseDbEntry(0x5, 0x6F, 0x03, "READ OF SCRAMBLED SECTOR WITHOUT AUTHENTICATION"),
            new SenseDbEntry(0x5, 0x6F, 0x04, "MEDIA REGION CODE IS MISMATCHED TO LOGICAL UNIT REGION"),
            new SenseDbEntry(0x5, 0x6F, 0x05, "LOGICAL UNIT REGION MUST BE PERMANENT/REGION RESET COUNT ERROR"),
            new SenseDbEntry(0x5, 0x6F, 0x06, "INSUFFICIENT BLOCK COUNT FOR BINDING NONCE RECORDING"),
            new SenseDbEntry(0x5, 0x6F, 0x07, "CONFLICT IN BINDING NONCE RECORDING"),
            new SenseDbEntry(0x3, 0x72, 0x00, "SESSION FIXATION ERROR"),
            new SenseDbEntry(0x3, 0x72, 0x01, "SESSION FIXATION ERROR WRITING LEAD-IN"),
            new SenseDbEntry(0x3, 0x72, 0x02, "SESSION FIXATION ERROR WRITING LEAD-OUT"),
            new SenseDbEntry(0x5, 0x72, 0x03, "SESSION FIXATION ERROR - INCOMPLETE TRACK IN SESSION"),
            new SenseDbEntry(0x5, 0x72, 0x04, "EMPTY OR PARTIALLY WRITTEN RESERVED TRACK"),
            new SenseDbEntry(0x5, 0x72, 0x05, "NO MORE TRACK RESERVATIONS ALLOWED"),
            new SenseDbEntry(0x5, 0x72, 0x06, "RMZ EXTENSION IS NOT ALLOWED"),
            new SenseDbEntry(0x5, 0x72, 0x07, "NO MORE TEST ZONE EXTENSIONS ARE ALLOWED"),
            new SenseDbEntry(0x3, 0x73, 0x00, "CD CONTROL ERROR"),
            new SenseDbEntry(0x1, 0x73, 0x01, "POWER CALIBRATION AREA ALMOST FULL"),
            new SenseDbEntry(0x3, 0x73, 0x02, "POWER CALIBRATION AREA IS FULL"),
            new SenseDbEntry(0x3, 0x73, 0x03, "POWER CALIBRATION AREA ERROR"),
            new SenseDbEntry(0x3, 0x73, 0x04, "PROGRAM MEMORY AREA UPDATE FAILURE"),
            new SenseDbEntry(0x3, 0x73, 0x05, "PROGRAM MEMORY AREA IS FULL"),
            new SenseDbEntry(0x1, 0x73, 0x06, "RMA/PMA IS ALMOST FULL"),
            new SenseDbEntry(0x3, 0x73, 0x10, "CURRENT POWER CALIBRATION AREA IS ALMOST FULL"),
            new SenseDbEntry(0x3, 0x73, 0x11, "CURRENT POWER CALIBRATION AREA IS FULL"),
            new SenseDbEntry(0x5, 0x73, 0x17, "RDZ IS FULL"),
            new SenseDbEntry(0x8, 0xff, 0xff, "BLANK CHECK")
        };
    }
}
