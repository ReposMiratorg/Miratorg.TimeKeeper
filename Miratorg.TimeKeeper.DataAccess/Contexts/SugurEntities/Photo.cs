using System;
using System.Collections.Generic;

namespace Storage.Data.EntitySigur
{
    public class Photo
    {
        public int Id { get; set; }
        public int? Ts { get; set; }
        public byte[] PreviewRaster { get; set; }
        public byte[] HiresRaster { get; set; }
        public byte[] TvaDesc { get; set; }
        public int? TvaDescts { get; set; }
        public string TvaDesctype { get; set; }
        public string Extver { get; set; }
    }
}
