
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using UnityEngine;
using VRWorlds.Schemas.Browser.Common;
using static VRWorlds.Schemas.Browser.Common.Ping;
using VRWorlds.Schemas.Browser.Standards;

namespace Assets.Code.Lib3DOMImplementation
{
    class PingImpl : PingBase
    {
        public override Task<PingReturn> Ping(PingRequest request, ServerCallContext context)
        {
            var now = System.DateTimeOffset.Now;
            var ret = new DateTimeOffset();
            ret.Ticks = now.Ticks;
            ret.Offset = System.Convert.ToInt32(now.Offset.TotalMinutes);



            var pr = new PingReturn { Now = ret };

            //return new Task<PingReturn>(pr)

            return base.Ping(request, context);


        }
    }
}