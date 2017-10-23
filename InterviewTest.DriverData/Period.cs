using System;
using System.Diagnostics;

namespace InterviewTest.DriverData
{
	[DebuggerDisplay("{_DebuggerDisplay,nq}")]
	public class Period
	{
        // BONUS: What's the difference between DateTime and DateTimeOffset?
        /* A DateTime value defines a particular calendar date and time which may vary for different timezones.
         * Information about the time zone to which that date and time belongs can be determined from Kind property of DateTime.
         * 
         * The DateTimeOffset represents a date and time value along with an offset indicating how much that value differs from UTC DateTime.
         * So, the DateTimeOffset value always unambiguously identifies an absolute point in time which is universal to everyone.
         */
        public DateTimeOffset Start;
		public DateTimeOffset End;

        // BONUS: What's the difference between decimal and double?
        /*
         * Decimal is 128 bit (28-29 significant digits) and will store the value as a floating decimal point type. E.g: 123.456
         * Double is 64 bit (15-16 digits) and will store the value as binary floating point type. E.g: 10001.100101
         * Decimals have much higher precision than Double which makes it important to be used in finacial applications.
         */
        public decimal AverageSpeed;

		private string _DebuggerDisplay => $"{Start:t} - {End:t}: {AverageSpeed}";
	}
}
