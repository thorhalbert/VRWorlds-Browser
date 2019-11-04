
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using UnityEngine;
using VRWorlds.Schemas.Browser.Common;
using static VRWorlds.Schemas.Browser.Common.Ping;
using VRWorlds.Schemas.Browser.Standards;
using System;

namespace Assets.Code.Lib3DOMImplementation
{
    class PingImpl : PingBase
    {
        private Guid _processId;
        public PingImpl(Guid processId)
        {
            _processId = processId;

        }
        public override Task<PingReturn> Ping(PingRequest request, ServerCallContext context)
        {
            //var now = System.DateTimeOffset.Now;
            //var ret = new DateTimeOffset();
            //ret.Ticks = now.Ticks;
            //ret.Offset = System.Convert.ToInt32(now.Offset.TotalMinutes);

            //var pr = new PingReturn { Now = ret };

            Debug.Log(_processId.ToString() + ": Got Ping");

            return Task.FromResult(new PingReturn());
        }
    }
}