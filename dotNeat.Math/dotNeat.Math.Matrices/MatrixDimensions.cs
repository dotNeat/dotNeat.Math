namespace dotNeat.Math.Matrices
{
    using System.Text;

    public struct MatrixDimensions
    {
        public MatrixDimensions(ulong rows, ulong columns)
        {
            this.Rows = rows;
            this.Columns = columns;
        }
        public ulong Rows { get; init; }
        public ulong Columns { get; init; }

        private string? _description = null;
        public override string ToString()
        {
            if (_description == null)
            {
                StringBuilder sb = new StringBuilder();

                if (this.Rows == 0 && this.Columns == 0)
                {
                    sb.Append("Empty Matrix ");
                }
                else if (this.Rows == 1 && this.Columns > 1)
                {
                    sb.Append("Row Vector ");
                }
                else if (this.Rows > 1 && this.Columns == 1)
                {
                    sb.Append("Column Vector ");
                }
                else if (this.Rows == this.Columns)
                {
                    sb.Append("Square Matrix ");
                }
                else
                {
                    sb.Append("Rectangular Matrix ");
                }

                sb.Append($"{this.Rows}x{this.Columns} [RxC]");

                _description = sb.ToString();
            }

            return _description;
        }
    }
}
