// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global
#pragma warning disable SA1307 // Accessible fields must begin with upper-case letter
#pragma warning disable GU0060 // Enum member value conflict. According to Roman this is ok.
#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace Gu.Wpf.UiAutomation
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using Gu.Wpf.UiAutomation.WindowsAPI;

    /// <summary>
    /// Helper for interpolating mouse and touch drag.
    /// </summary>
    public class Interpolation
    {
        private readonly Stopwatch stopwatch = Stopwatch.StartNew();
        private readonly POINT from;
        private readonly POINT to;
        private readonly TimeSpan time;

        private Status status = Status.NotStarted;

        private Interpolation(POINT @from, POINT to, TimeSpan time)
        {
            this.from = @from;
            this.to = to;
            this.time = time;
        }

        private enum Status
        {
            NotStarted,
            Running,
            Done,
        }

        /// <summary>
        /// Create an instance of <see cref="Interpolation"/>.
        /// </summary>
        /// <param name="from">The start position.</param>
        /// <param name="to">The end position.</param>
        /// <param name="speed">The speed in pixels / s.</param>
        /// <returns>An instance of <see cref="Interpolation"/>.</returns>
        public static Interpolation Start(Point from, Point to, double speed) => Start(POINT.Create(from), POINT.Create(to), speed);

        /// <summary>
        /// Create an instance of <see cref="Interpolation"/>.
        /// </summary>
        /// <param name="from">The start position.</param>
        /// <param name="to">The end position.</param>
        /// <param name="speed">The speed in pixels / s.</param>
        /// <returns>An instance of <see cref="Interpolation"/>.</returns>
        public static Interpolation Start(POINT from, POINT to, double speed)
        {
            var dx = from.X - to.X;
            var dy = from.Y - to.Y;
            var dist = Math.Sqrt((dx * dx) + (dy * dy));
            return new Interpolation(from, to, TimeSpan.FromSeconds(dist / speed));
        }

        /// <summary>
        /// Create an instance of the class.
        /// </summary>
        /// <param name="from">The start position.</param>
        /// <param name="to">The end position.</param>
        /// <param name="time">The total time of the interpolation.</param>
        public static Interpolation Start(Point from, Point to, TimeSpan time) => new Interpolation(POINT.Create(from), POINT.Create(to), time);

        /// <summary>
        /// Create an instance of the class.
        /// </summary>
        /// <remarks>
        /// Calls Restart() on <see cref="stopwatch"/>.
        /// </remarks>
        /// <param name="from">The start position.</param>
        /// <param name="to">The end position.</param>
        /// <param name="time">The total time of the interpolation.</param>
        public static Interpolation Start(POINT @from, POINT to, TimeSpan time) => new Interpolation(from, to, time);

        /// <summary>
        /// Try get the interpolated position at current time.
        /// </summary>
        /// <param name="position">The current position.</param>
        /// <returns>True until arrived at end position.</returns>
        public bool TryGetPosition(out POINT position) => this.TryGetPosition(this.stopwatch.Elapsed, out position);

        /// <summary>
        /// Try get the interpolated position at <paramref name="elapsed"/>.
        /// This is exposed mostly for test.
        /// </summary>
        /// <param name="elapsed">The time elapsed since the start.</param>
        /// <param name="position">The current position.</param>
        /// <returns>True until arrived at end position.</returns>
        internal bool TryGetPosition(TimeSpan elapsed, out POINT position)
        {
            if (this.status == Status.Done)
            {
                position = default(POINT);
                return false;
            }

            if (this.status == Status.NotStarted)
            {
                position = this.from;
                this.status = Status.Running;
            }
            else if (this.time.TotalMilliseconds - elapsed.TotalMilliseconds < 10)
            {
                position = this.to;
                this.status = Status.Done;
            }
            else
            {
                var s = elapsed.TotalMilliseconds / this.time.TotalMilliseconds;
                position = new POINT
                {
                    X = (int)(this.from.X + (s * (this.to.X - this.from.X))),
                    Y = (int)(this.from.Y + (s * (this.to.Y - this.from.Y))),
                };
            }

            return true;
        }
    }
}
