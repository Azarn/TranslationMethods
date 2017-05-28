using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.ClassDescription {
    public class ExceptionTableDescription {
        public ushort StartPC;
        public ushort EndPC;
        public ushort HandlerPC;
        public ushort CatchType;

        private ExceptionTableDescription() { }

        public static ExceptionTableDescription ParseData(byte[] data, ref int pos) {
            ExceptionTableDescription res = new ExceptionTableDescription();
            res.StartPC = Utils.ReadUShort(data, ref pos);
            res.EndPC = Utils.ReadUShort(data, ref pos);
            res.HandlerPC = Utils.ReadUShort(data, ref pos);
            res.CatchType = Utils.ReadUShort(data, ref pos);
            return res;
        }

        public byte[] BuildData() {
            var res = new List<byte>();
            res.AddRange(Utils.WriteUShort(StartPC));
            res.AddRange(Utils.WriteUShort(StartPC));
            res.AddRange(Utils.WriteUShort(HandlerPC));
            res.AddRange(Utils.WriteUShort(CatchType));
            return res.ToArray();
        }
    }
}
