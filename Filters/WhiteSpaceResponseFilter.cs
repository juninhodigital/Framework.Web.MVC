using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.MVC
{
    public class WhiteSpaceResponseFilter : Stream
    {
        #region| Fields |

        private Stream shrink;
        private Func<string, string> filter; 

        #endregion

        #region| Constructor | 

        public WhiteSpaceResponseFilter(Stream shrink, Func<string, string> filter)
        {
            this.shrink = shrink;
            this.filter = filter;
        }

        #endregion

        #region| Properties |

        public override bool CanRead { get { return true; } }
        public override bool CanSeek { get { return true; } }
        public override bool CanWrite { get { return true; } }
        public override void Flush() { shrink.Flush(); }
        public override long Length { get { return 0; } }
        public override long Position { get; set; }

        #endregion

        #region| Methods |

        public override int Read(byte[] buffer, int offset, int count)
        {
            return shrink.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return shrink.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            shrink.SetLength(value);
        }

        public override void Close()
        {
            shrink.Close();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            // capture the data and convert to string 
            byte[] data = new byte[count];
            Buffer.BlockCopy(buffer, offset, data, 0, count);
            string s = Encoding.Default.GetString(buffer);

            // filter the string
            s = filter(s);

            // write the data to stream 
            byte[] outdata = Encoding.Default.GetBytes(s);
            shrink.Write(outdata, 0, outdata.GetLength(0));
        } 

        #endregion
    }

}
