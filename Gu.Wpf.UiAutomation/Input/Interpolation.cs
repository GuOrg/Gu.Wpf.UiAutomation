// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
#pragma warning disable SA1600 // Elements must be documented
#pragma warning disable GU0060 // Enum member value conflict. According to Roman this is ok.
#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Diagnostics;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    /// <summary>
    /// Helper for interpolating mouse and touch drag.
    /// </summary>
    public class Interpolation
    {
        private enum Status
        {
            NotStarted,
            Running,
            Done,
        }

        /// <summary>
        /// Using a shared <see cref="Stopwatch"/> here as for example drag should not be called from multiple threads.
        /// </summary>
        public static readonly Stopwatch Stopwatch = Stopwatch.StartNew();

        private Status status = Status.NotStarted;

        public Interpolation(POINT @from, POINT to, TimeSpan time)
        {
            Stopwatch.Restart();
            this.From = @from;
            this.To = to;
            this.Time = time;
        }

        /// <summary>
        /// The starting point of the interpolation.
        /// </summary>
        public POINT From { get; }

        /// <summary>
        /// The end point of the interpolation.
        /// </summary>
        public POINT To { get; }

        /// <summary>
        /// The duration of the interpolation.
        /// </summary>
        public TimeSpan Time { get; }

        /// <summary>
        /// Try get the interpolated position at <paramref name="elapsed"/>.
        /// </summary>
        /// <param name="elapsed">The time elapsed since the start.</param>
        /// <param name="position">The current position.</param>
        /// <returns>True if a position could be calculated.</returns>
        public bool TryCurrent(TimeSpan elapsed, out POINT position)
        {
            if (this.status == Status.Done)
            {
                position = default(POINT);
                return false;
            }

            if (this.status == Status.NotStarted)
            {
                position = this.From;
                this.status = Status.Running;
            }
            else if (this.Time.TotalMilliseconds - elapsed.TotalMilliseconds < 10)
            {
                position = this.To;
                this.status = Status.Done;
            }
            else
            {
                var s = elapsed.TotalMilliseconds / this.Time.TotalMilliseconds;
                position = new POINT
                {
                    X = (int)(this.From.X + (s * (this.To.X - this.From.X))),
                    Y = (int)(this.From.Y + (s * (this.To.Y - this.From.Y))),
                };
            }

            return true;
        }
    }
}
